// src/routes/ProtectedRoute.tsx
import React from 'react';
import { useAuth } from '../context/auth';
import { Navigate } from 'react-router-dom';

interface ProtectedRouteProps {
  element: React.ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ element }) => {
  const { usuario } = useAuth();

  if (!usuario) {
    //return <Navigate to="/" replace />;
  }

  return <>{element}</>;
};

export default ProtectedRoute;
