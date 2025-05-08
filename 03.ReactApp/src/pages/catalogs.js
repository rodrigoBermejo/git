import React from 'react';
import { Container, Grid, Typography } from '@mui/material';
import { useRouter } from 'next/router';
import CatalogCard from '../components/catalog/CatalogCard';

const catalogs = [
  { name: 'Contacts', path: 'contacts' },
  { name: 'Customers', path: 'customers' },
  { name: 'Users', path: 'users' },
  { name: 'Orders', path: 'orders' },
  { name: 'Invoices', path: 'invoices' },
  { name: 'Products', path: 'products' },
  { name: 'Activities', path: 'activities' },
  { name: 'Support Cases', path: 'support-cases' },
  { name: 'Sales Opportunities', path: 'sales-opportunities' },
];

const CatalogsPage = () => {
  const router = useRouter();

  const handleCatalogClick = (path) => {
    router.push(`/catalogs/${path}`);
  };

  return (
    <Container>
      <Typography variant="h4" component="h1" gutterBottom>
        Catalogs
      </Typography>
      <Grid container spacing={3}>
        {catalogs.map((catalog) => (
          <CatalogCard key={catalog.path} catalog={catalog} onClick={handleCatalogClick} />
        ))}
      </Grid>
    </Container>
  );
};

export default CatalogsPage;