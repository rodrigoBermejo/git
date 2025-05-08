import React from 'react';
import { ListItem, ListItemText } from '@mui/material';
import { useRouter } from 'next/router';

const NavigationItem = ({ path, label }) => {
  const router = useRouter();

  const handleNavigation = () => {
    router.push(path);
  };

  return (
    <ListItem button onClick={handleNavigation}>
      <ListItemText primary={label} />
    </ListItem>
  );
};

export default NavigationItem;