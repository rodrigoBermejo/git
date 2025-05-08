import React from 'react';
import { Container, Typography, Box, Button } from '@mui/material';
import { useRouter } from 'next/router';

function Custom404() {
  const router = useRouter();

  const handleGoBack = () => {
    if (router.asPath !== '/') {
      router.back();
    } else {
      router.push('/');
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 8, textAlign: 'center' }}>
        <Typography variant="h4" component="h1" gutterBottom>
          404 - Page Not Found
        </Typography>
        <Typography variant="body1" gutterBottom>
          The page you are looking for does not exist.
        </Typography>
        <Button variant="contained" color="primary" onClick={handleGoBack} sx={{ mt: 2 }}>
          Go Back
        </Button>
      </Box>
    </Container>
  );
}

export default Custom404;