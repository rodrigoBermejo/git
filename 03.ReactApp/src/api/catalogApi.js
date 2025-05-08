import api from './axiosConfig';

export const getCatalogData = async (catalog) => {
  const response = await api.get(`/${catalog}`);
  return response.data;
};

export const deleteCatalogItem = async (catalog, id) => {
  await api.delete(`/${catalog}/${id}`);
};

export const addCatalogItem = async (catalog, item) => {
  const response = await api.post(`/${catalog}`, item);
  return response.data;
};

export const updateCatalogItem = async (catalog, item) => {
  const response = await api.put(`/${catalog}`, item);
  return response.data;
};