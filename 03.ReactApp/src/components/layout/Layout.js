import React, { useContext } from 'react';
import { Typography, Container, Paper, CssBaseline, Box, Drawer, IconButton, Divider } from '@mui/material';
import AppBar from './AppBar';
import Sidebar from './Sidebar';
import { GlobalContext } from '../../context/GlobalContext';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';

const drawerWidth = 240;

const Layout = ({ children }) => {
  const { drawerOpen, setDrawerOpen } = useContext(GlobalContext);

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar />
      <Drawer
        variant="temporary"
        anchor="top"
        open={drawerOpen}
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            boxSizing: 'border-box',
          },
        }}
      >
        <Box sx={{ display: 'flex', alignItems: 'center', padding: 1 }}>
          <IconButton onClick={() => setDrawerOpen(false)}>
            <ChevronLeftIcon />
          </IconButton>
        </Box>
        <Divider />
        <Sidebar />
      </Drawer>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 3,
          transition: 'margin 0.3s',
          marginLeft: drawerOpen ? `${drawerWidth}px` : '0px',
          width: drawerOpen ? `calc(100% - ${drawerWidth}px)` : '100%',
          marginTop: '64px',
        }}
      >
        <Paper elevation={3} sx={{ padding: 2 }}>
          {children}
        </Paper>
      </Box>
      <Box
        component="footer"
        sx={{
          p: 2,
          mt: 'auto',
          backgroundColor: (theme) => theme.palette.primary.main,
          color: (theme) => theme.palette.primary.contrastText,
          width: '100%',
          position: 'fixed',
          bottom: 0,
          left: 0,
        }}
      >
        <Container maxWidth="md">
          <Typography variant="body1" align="center">
            SmartBiz CRM &copy; 2023. All rights reserved.
          </Typography>
        </Container>
      </Box>
    </Box>
  );
};

export default Layout;