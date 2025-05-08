import React, { useContext, useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import { Container, TextField, Button, Typography, Box, MenuItem, Alert } from '@mui/material';
import { addCustomer } from '../../../api/customerApi';
import { GlobalContext } from '../../../context/GlobalContext';
import Loading from '../../common/Loading';

const stateMapping = {
  1: 'Active',
  0: 'Inactive'
};

const AddCustomer = () => {
  const router = useRouter();
  const { user } = useContext(GlobalContext);
  const [formData, setFormData] = useState({});
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [isFormValid, setIsFormValid] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: name === 'state' ? Number(value) : value });
  };

  useEffect(() => {
    const isValid = formData.name && formData.email && formData.phone && formData.address && formData.state !== undefined;
    setIsFormValid(isValid);
  }, [formData]);

  const validateForm = () => {
    if (!formData.name || !formData.email || !formData.phone || !formData.address || formData.state === undefined) {
      setError('All fields are required.');
      return false;
    }
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(formData.email)) {
      setError('Invalid email format.');
      return false;
    }
    setError('');
    return true;
  };

  const handleAdd = async () => {
    if (!validateForm()) {
      return;
    }
    setLoading(true);
    try {
      const createdBy = user ? user.idUser : 'unknown';
      const status = await addCustomer({ ...formData, createdBy });
      if (status === 200) {
        setError('');
        router.push('/catalogs/customers');
      } else {
        setError('Error adding customer.');
      }
    } catch (error) {
      setError('Error adding customer: ' + error.message);
    } finally {
      setLoading(false);
    }
  };

  const handleBack = () => {
    router.back();
  };

  return (
    <Container maxWidth="sm">
      {loading ? <Loading /> :
        <Box sx={{ mt: 8 }}>
          <Typography variant="h4" component="h1" gutterBottom>
            Add New Customer
          </Typography>
          {error && <Alert severity="error">{error}</Alert>}
          <TextField
            label="Name"
            name="name"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.name || ''}
            onChange={handleChange}
            required
          />
          <TextField
            label="Email"
            name="email"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.email || ''}
            onChange={handleChange}
            required
          />
          <TextField
            label="Phone"
            name="phone"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.phone || ''}
            onChange={handleChange}
            required
          />
          <TextField
            label="Address"
            name="address"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.address || ''}
            onChange={handleChange}
            required
          />
          <TextField
            label="State"
            name="state"
            select
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.state || ''}
            onChange={handleChange}
            required
          >
            {Object.entries(stateMapping).map(([key, value]) => (
              <MenuItem key={key} value={key}>
                {value}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            label="Notes"
            name="notes"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.notes || ''}
            onChange={handleChange}
          />
          <Button variant="contained" color="primary" onClick={handleAdd} disabled={!isFormValid || loading} sx={{ mt: 2 }}>
            Add
          </Button>
          <Button variant="outlined" color="secondary" onClick={handleBack} sx={{ mt: 2, ml: 2 }}>
            Back
          </Button>
        </Box>
      }
    </Container>
  );
};

export default AddCustomer;