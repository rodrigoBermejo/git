import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  typography: {
    fontFamily: 'Segoe UI, Arial, sans-serif',
  },
  palette: {
    primary: {
      main: '#37474f', // Darker shade of blue-gray
    },
    secondary: {
      main: '#546e7a', // Muted blue-gray
    },
    background: {
      default: '#eceff1', // Light gray
      paper: '#ffffff', // White for paper elements
    },
    text: {
      primary: '#263238', // Darker gray for primary text
      secondary: '#546e7a', // Muted gray for secondary text
    },
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
        },
      },
    },
  },
  appName: 'SmartBiz CRM', // Optional: Add app name to theme
});

export default theme;