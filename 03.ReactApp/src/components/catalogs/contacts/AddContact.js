import React, { useContext, useState, useEffect } from 'react';
import { useRouter } from 'next/router';
import { Container, TextField, Button, Typography, Box, Alert, Autocomplete } from '@mui/material';
import { addContact } from '../../../api/contactApi';
import { getAllCustomers } from '../../../api/customerApi';
import { GlobalContext } from '../../../context/GlobalContext';
import Loading from '../../common/Loading';

const AddContact = () => {
  const router = useRouter();
  const { user } = useContext(GlobalContext);
  const [formData, setFormData] = useState({});
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [isFormValid, setIsFormValid] = useState(false);
  const [customers, setCustomers] = useState([]);

  useEffect(() => {
    fetchCustomers();
  }, []);

  const fetchCustomers = async () => {
    try {
      const result = await getAllCustomers();
      setCustomers(result);
    } catch (error) {
      console.error('Error fetching customers:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleCustomerChange = (event, value) => {
    setFormData({ ...formData, idCustomer: value ? value.idCustomer : '' });
  };

  useEffect(() => {
    const isValid = formData.name && formData.position && formData.email && formData.phone && formData.idCustomer;
    setIsFormValid(isValid);
  }, [formData]);

  const validateForm = () => {
    if (!formData.name || !formData.position || !formData.email || !formData.phone || !formData.idCustomer) {
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
      const created = new Date().toISOString();
      const createdBy = user ? user.userName : 'unknown';
      const status = await addContact({ ...formData, createdBy, created });
      if (status === 200) {
        setError('');
        router.push('/catalogs/contacts');
      } else {
        setError('Error adding contact.');
      }
    } catch (error) {
      setError('Error adding contact: ' + error.message);
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
            Add New Contact
          </Typography>
          {error && <Alert severity="error">{error}</Alert>}
          <Autocomplete
            options={customers}
            getOptionLabel={(option) => option.name}
            onChange={handleCustomerChange}
            renderInput={(params) => (
              <TextField
                {...params}
                label="Customer"
                variant="outlined"
                fullWidth
                margin="normal"
                required
              />
            )}
          />
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
            label="Position"
            name="position"
            variant="outlined"
            fullWidth
            margin="normal"
            value={formData.position || ''}
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

export default AddContact;