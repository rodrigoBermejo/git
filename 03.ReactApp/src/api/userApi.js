import api from './axiosConfig';
import GlobalContext from '../context/GlobalContext';

export const login = async (username, password) => {
  const response = await api.post('/user/login', { username, password });
  return response.data;
};

export const getCurrentUser = async () => {
  const response = await api.get('/user/me');
  return response.data;
};

export const getAllUsers = async () => {
  const response = await api.get('/user');
  return response.data;
};

export const getUserById = async (id) => {
  const response = await api.get(`/user/${id}`);
  return response.data;
};

export const addUser = async (user, createdBy) => {
  const response = await api.post('/user', user);
  return response.status;
};

export const updateUser = async (user, updatedBy) => {
  const response = await api.put('/user', user);
  return response.status;
};

export const deleteUser = async (id) => {
  const response = await api.delete(`/user/${id}`);
  return response.status;
};