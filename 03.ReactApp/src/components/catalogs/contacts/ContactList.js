import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
import { Container, Typography, Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { getAllContacts, deleteContact } from '../../../api/contactApi';

const ContactList = () => {
  const router = useRouter();
  const [contacts, setContacts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setLoading(true);
    try {
      const result = await getAllContacts();
      setContacts(result);
    } catch (error) {
      console.error('Error fetching contacts:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteContact(id);
      fetchData();
    } catch (error) {
      console.error('Error deleting contact:', error);
    }
  };

  const handleAdd = () => {
    router.push('/catalogs/contacts/add');
  };

  const handleEdit = (id) => {
    router.push(`/catalogs/contacts/edit/${id}`);
  };

  const handleBack = () => {
    router.back();
  };

  return (
    <Container>
      <Typography variant="h4" component="h1" gutterBottom>
        Contacts
      </Typography>
      <Button variant="contained" color="primary" onClick={handleAdd} sx={{ mb: 2 }}>
        Add New Contact
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
                <TableCell>Position</TableCell>
                <TableCell>Email</TableCell>
                <TableCell>Phone</TableCell>
                <TableCell>Notes</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {contacts.map((contact) => (
                <TableRow key={contact.idContact}>
                  <TableCell>
                    <IconButton color="primary" onClick={() => handleEdit(contact.idContact)} sx={{ mr: 1 }}>
                      <EditIcon />
                    </IconButton>
                    <IconButton color="error" onClick={() => handleDelete(contact.idContact)}>
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                  <TableCell>{contact.name}</TableCell>
                  <TableCell>{contact.position}</TableCell>
                  <TableCell>{contact.email}</TableCell>
                  <TableCell>{contact.phone}</TableCell>
                  <TableCell>{contact.notes}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
};

export default ContactList;