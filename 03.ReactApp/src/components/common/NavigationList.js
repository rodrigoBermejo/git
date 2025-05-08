import React from 'react';
import { List } from '@mui/material';
import NavigationItem from '../../components/common/NavigationItem';

const NavigationList = () => {
  return (
    <List>
      <NavigationItem path="/" label="Home" />
      <NavigationItem path="/about" label="About" />
      <NavigationItem path="/profile" label="Profile" />
      <NavigationItem path="/catalogs" label="Catalogs" />
    </List>
  );
};

export default NavigationList;