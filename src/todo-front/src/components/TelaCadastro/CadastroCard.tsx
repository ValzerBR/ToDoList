import React from "react";
import InputField from "../InputField";
import { Card } from "primereact/card";
import { Button } from "primereact/button";

interface CadastroCardProps {
    nome: string,
    email: string,
    setEmail: (value: string) => void,
    setNome: (value: string) => void,
    handleCadastrar: () => void,
    error?: string
};

const CadastroCard = ({nome, email, setEmail, setNome, handleCadastrar, error}: CadastroCardProps) => {
    return (
        <div className="flex align-items-center justify-content-center min-h-screen">
          <div className="surface-card p-4 shadow-2 border-round w-full lg:w-6">
            <div className="flex flex-column md:flex-row align-items-center gap-3">
              <div className="flex-1">
                <div className="flex flex-column gap-2">
                <InputField label="Nome" value={nome} onChange={setNome}  placeholder="Digite seu nome"/>
                <InputField label="E-mail" value={email} onChange={setEmail} placeholder="Digite seu e-mail" />
                {error && <p className="text-red-500 text-sm">{error}</p>}
                <Button label="Cadastrar" icon="pi pi-sign-in" className="w-full" onClick={handleCadastrar} />
                </div>
              </div>
            </div>
          </div>
        </div>
      );
    };

export default CadastroCard;
