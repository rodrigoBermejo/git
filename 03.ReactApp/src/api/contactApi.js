import api from './axiosConfig';

export const getAllContacts = async () => {
  const response = await api.get('/contact');
  return response.data;
};

export const getContactById = async (id) => {
  const response = await api.get(`/contact/${id}`);
  return response.data;
};

export const addContact = async (contact, createdById) => {
  contact.createdById = createdById;
  contact.created = new Date().toISOString();
  const response = await api.post('/contact', contact);
  return response.status;
};

export const updateContact = async (contact, updatedById) => {
  contact.updatedById = updatedById;
  contact.updated = new Date().toISOString();
  const response = await api.put('/contact', contact);
  return response.status;
};

export const deleteContact = async (id) => {
  const response = await api.delete(`/contact/${id}`);
  return response.status;
};