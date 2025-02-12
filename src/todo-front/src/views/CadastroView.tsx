import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import LoginCard from '../components/TelaLogin/LoginCard';
import CadastroCard from '../components/TelaCadastro/CadastroCard';
import { UsuarioService } from '../services/UsuarioService';
import { Usuario } from '../models/Usuario';
import { useAuth } from '../context/auth';

const CadastroView = () => {
  const [email, setEmail] = useState('');
  const [nome, setNome] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const {usuario, isLogged} = useAuth();

  const handleCadastro = async () => {
        if(!nome || !email) {
          setError("Não foi possível cadastrar o usuário.");
        }
        else{
          const usuario = new Usuario(nome, email);
          var usuarioReq = await UsuarioService.cadastrar(usuario)
          .then((response) => {
            const user = new Usuario(response.Nome, response.Email, response.Id);
            isLogged(user);
            navigate('/tarefa');
          })
          .catch((error) => {
            setError("Não foi possível cadastrar o usuário.");
          });
        }
};

  return (
      <CadastroCard 
        email={email} 
        setEmail={setEmail}
        nome={nome}
        setNome={setNome}
        handleCadastrar={handleCadastro}
        error={error} 
      />
  );
};

export default CadastroView;
