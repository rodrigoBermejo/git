import React, { useState, useContext } from 'react';
import { Container, Typography, Box } from '@mui/material';
import { useRouter } from 'next/router';
import { GlobalContext } from '../context/GlobalContext';
import { login } from '../api/userApi';
import LoginForm from '../components/auth/LoginForm';

const LoginPage = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const router = useRouter();
  const { setUser, setToken, setTokenExpiration } = useContext(GlobalContext);

  const handleLogin = async (email, password) => {
    setLoading(true);
    setError('');
    try {
      const data = await login(email, password);
      setUser(data.user);
      setToken(data.token);
      setTokenExpiration(data.expiration);
      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(data.user));
      localStorage.setItem('tokenExpiration', data.expiration);
      router.push('/');
    } catch (error) {
      console.error('Error logging in:', error);
      setError('Error logging in. Please check your credentials.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm">
      <Box sx={{ mt: 8 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Login
        </Typography>
        {error && (
          <Typography variant="body1" color="error" gutterBottom>
            {error}
          </Typography>
        )}
        <LoginForm onLogin={handleLogin} loading={loading} />
      </Box>
    </Container>
  );
};

export default LoginPage;