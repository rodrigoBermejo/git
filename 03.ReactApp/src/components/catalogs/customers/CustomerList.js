import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
import { Container, Typography, Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { getAllCustomers, deleteCustomer } from '../../../api/customerApi';

const stateMapping = {
  1: 'Active',
  0: 'Inactive'
};

const CustomerList = () => {
  const router = useRouter();
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setLoading(true);
    try {
      const result = await getAllCustomers();
      setCustomers(result);
    } catch (error) {
      console.error('Error fetching customers:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteCustomer(id);
      fetchData();
    } catch (error) {
      console.error('Error deleting customer:', error);
    }
  };

  const handleAdd = () => {
    router.push('/catalogs/customers/add');
  };

  const handleEdit = (id) => {
    router.push(`/catalogs/customers/edit/${id}`);
  };

  const handleBack = () => {
    router.back();
  };

  return (
    <Container>
      <Typography variant="h4" component="h1" gutterBottom>
        Customers
      </Typography>
      <Button variant="contained" color="primary" onClick={handleAdd} sx={{ mb: 2 }}>
        Add New Customer
      </Button>
      <Button variant="outlined" color="secondary" onClick={handleBack} sx={{ mb: 2, ml: 2 }}>
        Back
      </Button>
      {loading ? (
        <Typography>Loading...</Typography>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Actions</TableCell>
                <TableCell>Name</TableCell>
                <TableCell>Email</TableCell>
                <TableCell>Phone</TableCell>
                <TableCell>Address</TableCell>
                <TableCell>State</TableCell>
                <TableCell>Notes</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {customers.map((customer) => (
                <TableRow key={customer.idCustomer}>
                  <TableCell>
                    <IconButton color="primary" onClick={() => handleEdit(customer.idCustomer)} sx={{ mr: 1 }}>
                      <EditIcon />
                    </IconButton>
                    <IconButton color="error" onClick={() => handleDelete(customer.idCustomer)}>
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                  <TableCell>{customer.name}</TableCell>
                  <TableCell>{customer.email}</TableCell>
                  <TableCell>{customer.phone}</TableCell>
                  <TableCell>{customer.address}</TableCell>
                  <TableCell>{stateMapping[customer.state]}</TableCell>
                  <TableCell>{customer.notes}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
};

export default CustomerList;