import React, { createContext, useState } from 'react';

export const GlobalContext = createContext();

export const GlobalProvider = ({ children }) => {
  const [searchQuery, setSearchQuery] = useState('');
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [tokenExpiration, setTokenExpiration] = useState(null);
  const [drawerOpen, setDrawerOpen] = useState(false);

  return (
    <GlobalContext.Provider value={{ searchQuery, setSearchQuery, user, setUser, token, setToken, tokenExpiration, setTokenExpiration, drawerOpen, setDrawerOpen }}>
      {children}
    </GlobalContext.Provider>
  );
};