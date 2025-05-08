import React from 'react';
import { Container, Typography, Box } from '@mui/material';

const AboutPage = () => {
  return (
    <Container maxWidth="md">
      <Box sx={{ mt: 8 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          About Us
        </Typography>
        <Typography variant="body1" gutterBottom>
          Welcome to our CRM application. This application is designed to help you manage your customer relationships effectively.
        </Typography>
        <Typography variant="body1" gutterBottom>
          <strong>Copyright &copy; 2023 SmartBiz CRM. All rights reserved.</strong>
        </Typography>
        <Typography variant="body1" gutterBottom>
          Unauthorized use and/or duplication of this material without express and written permission from this site’s author and/or owner is strictly prohibited.
        </Typography>
      </Box>
    </Container>
  );
};

export default AboutPage;