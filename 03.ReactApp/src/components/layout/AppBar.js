import React, { useState, useContext } from 'react';
import { AppBar as MuiAppBar, Toolbar, Typography, IconButton, Menu, MenuItem, Avatar, TextField, InputAdornment } from '@mui/material';
import { Language as LanguageIcon, Menu as MenuIcon, Search as SearchIcon } from '@mui/icons-material';
import { useRouter } from 'next/router';
import { GlobalContext } from '../../context/GlobalContext';

function AppBar() {
  const [anchorEl, setAnchorEl] = useState(null);
  const [languageAnchorEl, setLanguageAnchorEl] = useState(null);
  const { searchQuery, setSearchQuery,
    setUser,
    setToken, setTokenExpiration,
    setDrawerOpen } = useContext(GlobalContext);
  const router = useRouter();

  const handleProfileMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleLanguageMenuOpen = (event) => {
    setLanguageAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    setLanguageAnchorEl(null);
  };

  const handleSignOut = () => {
    setUser(null);
    setToken(null);
    setTokenExpiration(null);
    router.push('/login');
    handleMenuClose();
  };

  const handleProfile = () => {
    router.push('/profile');
    handleMenuClose();
  };

  const handleSearchChange = (event) => {
    setSearchQuery(event.target.value);
  };

  const handleDrawerToggle = () => {
    setDrawerOpen((prev) => !prev);
  };

  return (
    <MuiAppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
      <Toolbar>
        <IconButton
          edge="start"
          color="inherit"
          aria-label="open drawer"
          onClick={handleDrawerToggle}
        >
          <MenuIcon />
        </IconButton>
        <Typography variant="h6" noWrap sx={{ flexGrow: 1 }}>
          SmartBiz CRM
        </Typography>
        <TextField
          value={searchQuery}
          onChange={handleSearchChange}
          placeholder="Search..."
          variant="outlined"
          size="small"
          sx={{ backgroundColor: 'white', borderRadius: 1, mr: 2 }}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon />
              </InputAdornment>
            ),
          }}
        />
        <IconButton color="inherit" onClick={handleLanguageMenuOpen}>
          <LanguageIcon />
        </IconButton>
        <Menu
          anchorEl={languageAnchorEl}
          anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
          keepMounted
          transformOrigin={{ vertical: 'top', horizontal: 'right' }}
          open={Boolean(languageAnchorEl)}
          onClose={handleMenuClose}
        >
          <MenuItem onClick={handleMenuClose}>English</MenuItem>
          <MenuItem onClick={handleMenuClose}>Spanish</MenuItem>
        </Menu>
        <IconButton color="inherit" onClick={handleProfileMenuOpen}>
          <Avatar />
        </IconButton>
        <Menu
          anchorEl={anchorEl}
          anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
          keepMounted
          transformOrigin={{ vertical: 'top', horizontal: 'right' }}
          open={Boolean(anchorEl)}
          onClose={handleMenuClose}
        >
          <MenuItem onClick={handleProfile}>Profile</MenuItem>
          <MenuItem onClick={handleSignOut}>Sign Out</MenuItem>
        </Menu>
      </Toolbar>
    </MuiAppBar>
  );
}

export default AppBar;