import { Button } from 'primereact/button';
import InputField from '../InputField';
import React from 'react';
import { Divider } from 'primereact/divider';

interface LoginCardProps {
  email: string;
  senha: string;
  setEmail: (value: string) => void;
  setSenha: (value: string) => void;
  handleLogin: () => void;
  handleCadastro: () => void;
  error?: string;
}

const LoginCard = ({ email, senha, setEmail, setSenha, handleLogin, handleCadastro, error }: LoginCardProps) => {
  return (
    <div className="flex align-items-center justify-content-center min-h-screen">
      <div className="surface-card p-4 shadow-2 border-round w-full lg:w-6">
        <div className="flex flex-column md:flex-row align-items-center gap-3">
          <div className="flex-1">
            <div className="flex flex-column gap-2">
              <InputField label="E-mail" value={email} onChange={setEmail} placeholder="Digite seu e-mail" />
              {error && <p className="text-red-500 text-sm">{error}</p>}
              <InputField label="Senha" value={senha} onChange={setSenha} placeholder="Digite sua senha" />
              <Button label="Entrar" icon="pi pi-user" className="w-full mt-2" onClick={handleLogin} />
            </div>
          </div>
  
          <Divider layout="vertical" className="hidden md:flex">
            <b>OU</b>
          </Divider>
          
          <div className="flex-1 flex align-items-center justify-content-center">
            <Button 
              label="Criar conta" 
              icon="pi pi-user-plus" 
              severity="success" 
              className="w-full" 
              onClick={handleCadastro}
            />
          </div>
        </div>
      </div>
    </div>
  );
};




export default LoginCard;
