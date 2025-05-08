import React, { useEffect, useContext, useState } from 'react';
import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import theme from '../theme';
import Layout from '../components/layout/Layout';
import Loading from '../components/common/Loading';
import { useRouter } from 'next/router';
import { GlobalProvider, GlobalContext } from '../context/GlobalContext';

function MyApp({ Component, pageProps }) {
  return (
    <GlobalProvider>
      <AppContent Component={Component} pageProps={pageProps} />
    </GlobalProvider>
  );
}

function AppContent({ Component, pageProps }) {
  const router = useRouter();
  const [loading, setLoading] = useState(true);
  const { token, setToken, setUser, setTokenExpiration } = useContext(GlobalContext);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    const storedUser = localStorage.getItem('user');
    const storedTokenExpiration = localStorage.getItem('tokenExpiration');

    if (storedToken && storedUser && storedTokenExpiration) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));
      setTokenExpiration(storedTokenExpiration);
    }

    if (!storedToken && router.pathname !== '/login' && router.pathname !== '/404') {
      router.push('/login');
    } else {
      setLoading(false);
    }
  }, [router.pathname]);

  if (loading) {
    return <Loading />;
  }

  const isLoginPage = router.pathname === '/login';
  const is404Page = router.pathname === '/404';

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      {isLoginPage || is404Page ? (
        <Component {...pageProps} />
      ) : (
        <Layout>
          <Component {...pageProps} />
        </Layout>
      )}
    </ThemeProvider>
  );
}

export default MyApp;