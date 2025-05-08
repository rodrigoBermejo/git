import api from './axiosConfig';

export const getAllCustomers = async () => {
  const response = await api.get('/customer');
  return response.data;
};

export const getCustomerById = async (id) => {
  const response = await api.get(`/customer/${id}`);
  return response.data;
};

export const addCustomer = async (customer, createdBy) => {
  customer.createdById = createdBy;
  customer.created = new Date().toISOString();
  const response = await api.post('/customer', customer);
  return response.status;
};

export const updateCustomer = async (customer, updatedBy) => {
  customer.updatedById = updatedBy;
  customer.updated = new Date().toISOString();
  const response = await api.put('/customer', customer);
  return response.status;
};

export const deleteCustomer = async (id) => {
  const response = await api.delete(`/customer/${id}`);
  return response.status;
};