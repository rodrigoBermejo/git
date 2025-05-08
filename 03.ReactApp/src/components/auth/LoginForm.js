
import React, { useState } from 'react';
import { TextField, Button, Box, CircularProgress } from '@mui/material';

const LoginForm = ({ onLogin, loading }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleKeyPress = (event) => {
    if (event.key === 'Enter') {
      onLogin(email, password);
    }
  };

  return (
    <>
      <TextField
        label="Email"
        variant="outlined"
        fullWidth
        margin="normal"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        onKeyPress={handleKeyPress}
      />
      <TextField
        label="Password"
        type="password"
        variant="outlined"
        fullWidth
        margin="normal"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        onKeyPress={handleKeyPress}
      />
      <Box sx={{ position: 'relative', display: 'flex', justifyContent: 'center', mt: 2 }}>
        <Button
          variant="contained"
          color="primary"
          fullWidth
          onClick={() => onLogin(email, password)}
          disabled={loading}
        >
          Login
        </Button>
        {loading && (
          <CircularProgress
            size={24}
            sx={{
              position: 'absolute',
              top: '50%',
              left: '50%',
              marginTop: '-12px',
              marginLeft: '-12px',
            }}
          />
        )}
      </Box>
    </>
  );
};

export default LoginForm;