import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
import { Container, Typography, Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { getAllUsers, deleteUser } from '../../../api/userApi';

const roleMapping = {
  1: 'Admin',
  2: 'Sales',
  3: 'Support',
  4: 'Manager',
  5: 'User'
};

const UserList = () => {
  const router = useRouter();
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setLoading(true);
    try {
      const result = await getAllUsers();
      setUsers(result);
    } catch (error) {
      console.error('Error fetching users:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    try {
      await deleteUser(id);
      fetchData();
    } catch (error) {
      console.error('Error deleting user:', error);
    }
  };

  const handleAdd = () => {
    router.push('/catalogs/users/add');
  };

  const handleEdit = (id) => {
    router.push(`/catalogs/users/edit/${id}`);
  };

  const handleBack = () => {
    router.back();
  };

  return (
    <Container>
      <Typography variant="h4" component="h1" gutterBottom>
        Users
      </Typography>
      <Button variant="contained" color="primary" onClick={handleAdd} sx={{ mb: 2 }}>
        Add New User
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
                <TableCell>Username</TableCell>
                <TableCell>Display Name</TableCell>
                <TableCell>Email</TableCell>
                <TableCell>Role</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {users.map((user) => (
                <TableRow key={user.idUser}>
                  <TableCell>
                    <IconButton color="primary" onClick={() => handleEdit(user.idUser)} sx={{ mr: 1 }}>
                      <EditIcon />
                    </IconButton>
                    <IconButton color="error" onClick={() => handleDelete(user.idUser)}>
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                  <TableCell>{user.userName}</TableCell>
                  <TableCell>{user.userDisplayName}</TableCell>
                  <TableCell>{user.email}</TableCell>
                  <TableCell>{roleMapping[user.role]}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </Container>
  );
};

export default UserList;