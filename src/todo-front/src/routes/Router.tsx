// src/routes/Router.tsx
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import React from 'react';
import LoginView from '../views/LoginView';
import CadastroView from '../views/CadastroView';
import TarefaView from '../views/TarefaView';
import { AuthProvider } from '../context/auth';
import ProtectedRoute from './ProtectedRoute';  // Importando o componente de rota protegida

const AppRouter = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginView />} />
        <Route path="/cadastro" element={<CadastroView />} />
        <Route
          path="/tarefa"
          element={
            <ProtectedRoute
              element={<TarefaView />}  // Página que será renderizada se o usuário estiver logado
            />
          }
        />
      </Routes>
    </Router>
  );
};

export default AppRouter;
