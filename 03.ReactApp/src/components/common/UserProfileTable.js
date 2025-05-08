import React from 'react';
import { Table, TableBody, TableCell, TableHead, TableRow } from '@mui/material';

const UserProfileTable = ({ user }) => (
  <Table sx={{ minWidth: 650 }} aria-label="user profile table">
    <TableHead>
      <TableRow>
        <TableCell>Field</TableCell>
        <TableCell>Value</TableCell>
      </TableRow>
    </TableHead>
    <TableBody>
      <TableRow>
        <TableCell><strong>Username</strong></TableCell>
        <TableCell>{user.userName}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell><strong>Display Name</strong></TableCell>
        <TableCell>{user.userDisplayName}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell><strong>Email</strong></TableCell>
        <TableCell>{user.email}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell><strong>Role</strong></TableCell>
        <TableCell>{user.role}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell><strong>Created</strong></TableCell>
        <TableCell>{new Date(user.created).toLocaleString()}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell><strong>Updated</strong></TableCell>
        <TableCell>{user.updated ? new Date(user.updated).toLocaleString() : 'N/A'}</TableCell>
      </TableRow>
    </TableBody>
  </Table>
);

export default UserProfileTable;