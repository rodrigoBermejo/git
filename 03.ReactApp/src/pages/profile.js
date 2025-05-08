import React from 'react';
import { Paper, Typography, TableContainer } from '@mui/material';
import UserProfileTable from '../components/common/UserProfileTable';

function ProfilePage() {
  const user = JSON.parse(localStorage.getItem('user'));

  return (
    <>
      <Typography variant="h4" component="h1" gutterBottom>
        User Profile
      </Typography>
      <TableContainer component={Paper}>
        <UserProfileTable user={user} />
      </TableContainer>
    </>
  );
}

export default ProfilePage;