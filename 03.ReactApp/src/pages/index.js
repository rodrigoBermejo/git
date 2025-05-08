import React from 'react';
import { Typography } from '@mui/material';
import withAuth from '../components/auth/withAuth';


function HomePage() {
  return (
    <>
      <Typography variant="h4" component="h1" gutterBottom>
        Welcome to SmartBiz CRM
      </Typography>
      <Typography variant="body1" gutterBottom>
        This is the home page of your CRM.
      </Typography>
    </>
  );
}

export default withAuth(HomePage);