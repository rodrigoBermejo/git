import { useEffect, useContext, useState } from 'react';
import { useRouter } from 'next/router';
import { GlobalContext } from '../../context/GlobalContext';
import Loading from '../../components/common/Loading';

const withAuth = (WrappedComponent) => {
  return (props) => {
    const router = useRouter();
    const { token, setToken, setUser, setTokenExpiration } = useContext(GlobalContext);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
      const storedToken = localStorage.getItem('token');
      const storedUser = localStorage.getItem('user');
      const storedTokenExpiration = localStorage.getItem('tokenExpiration');

      if (storedToken && storedUser && storedTokenExpiration) {
        setToken(storedToken);
        setUser(JSON.parse(storedUser));
        setTokenExpiration(storedTokenExpiration);
      }

      if (!storedToken) {
        router.push('/login');
      } else {
        setLoading(false);
      }
    }, [router.pathname]);

    if (loading) {
      return <Loading />;
    }

    return <WrappedComponent {...props} />;
  };
};

export default withAuth;