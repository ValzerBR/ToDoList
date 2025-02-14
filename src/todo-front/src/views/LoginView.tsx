import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import LoginCard from '../components/TelaLogin/LoginCard';
import { UsuarioController } from '../controllers/UsuarioController';
import { Usuario } from '../models/Usuario';
import { useAuth } from '../context/auth';

const LoginView = () => {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { isLogged, usuario } = useAuth();

  const handleLogin = () => {
    if (!email) {
      setError('Preencha todos os campos!');
      return;
    }
    setError("");
    var request = UsuarioController.login(email, senha)
      .then((response: any) => {
        if(response){
          const user = new Usuario(response.Nome, response.Email, response.Senha, response.Id);
          isLogged(user);
          navigate('/tarefa')
        }else{
          setError("Usuário não existe!");
        }
      })
      .catch((error) => setError(error));
  };


  const handleCadastro = () => {
    navigate('/cadastro');
  };

  return (
      <LoginCard 
        email={email} 
        senha={senha}
        setEmail={setEmail} 
        setSenha={setSenha}
        handleLogin={handleLogin} 
        handleCadastro={handleCadastro} 
        error={error} 
      />
  );
};

export default LoginView;
