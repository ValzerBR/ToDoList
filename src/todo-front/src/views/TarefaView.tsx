import React, { useState, useEffect } from 'react';
import TarefaCard from '../components/TelaTarefa/TarefaCard'
import { Toolbar } from 'primereact/toolbar';
import { Button } from 'primereact/button';
import { IconField } from 'primereact/iconfield';
import { InputText } from 'primereact/inputtext';
import TarefaModal from '../components/TelaTarefa/TarefaModal';
import { useAuth } from '../context/auth';
import { useNavigate } from 'react-router-dom';
import { UsuarioController } from '../controllers/UsuarioController';
import { Tarefa } from '../models/Tarefa';
import { TarefaController } from '../controllers/TarefaController';

const TarefaView = () => {
  const { usuario, logout } = useAuth();
  const [tarefas, setTarefas] = useState<Tarefa[]>();
  const [idTarefa, setIdTarefa] = useState<number | null>();
  const [searchTerm, setSearchTerm] = useState<string>('');

  const atualizarTarefas = () => {
    if(usuario) {
      UsuarioController.detail(usuario.id)
      .then((response) => {
        setTarefas(response.Tarefas);
      })
    }
  }

  useEffect(() => {
    atualizarTarefas();
  }, [usuario])

  const navigate = useNavigate();
  const [visible, setVisible] = useState<boolean>(false);
  const [edit, setEditState] = useState<boolean>(false);

  //Se usuário for alterado, volta para a tela inicial.
  useEffect(() => {
    if (!usuario) {
      navigate('/');
    }
  }, [usuario, navigate]);

  const handleEdit = (id: number) => {
    setIdTarefa(id);
    setEditState(true);
    setVisible(true); 
  };

  const handleRemove = (id: number) => {
    TarefaController.delete(id)
      .then((response) => { 
        atualizarTarefas();
    })
  };

  const startContent = (
    <React.Fragment>
      <Button label='Criar tarefa' icon="pi pi-plus" className="mr-2" onClick={() => { setVisible(true); setIdTarefa(null); }} />
    </React.Fragment>
  );

  const search = () => {
    TarefaController.search(searchTerm, usuario.id)
    .then((response) => {
      setTarefas(response);
    })
  }

  const centerContent = (
    <IconField iconPosition="left">
      <InputText placeholder="Consultar" 
      value={searchTerm}
      onChange={(e) => setSearchTerm(e.target.value)}
      />
      <Button icon="pi pi-search" onClick={search} className="p-button p-button-outlined p-button-info" />
      <Button icon="pi pi-filter" onClick={search} className="p-button p-button-outlined p-button-info" />
    </IconField>
  );

  const endContent = (
    <Button icon ="pi pi-sign-out" className="p-button p-button-warning" onClick={() => {logout(); navigate('/')}}>
    </Button>
  )

  return (
    <div>
      <Toolbar
        start={startContent}
        center={centerContent}
        end={endContent}
      />
      <div className="min-h-screen bg-gray-100 p-6">
        <h1 className="text-2xl font-bold text-center mb-6">Minhas Tarefas</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {tarefas?.map((tarefa) => (
            <TarefaCard key={tarefa.Id} tarefa={tarefa} onEdit={handleEdit} onDelete={handleRemove}/>
          ))}
        </div>
      </div>
      <TarefaModal
        visible={visible}
        onClose={() => { setVisible(false); setEditState(false); }}
        idDaTarefa={idTarefa} // Passa o id da tarefa para o modal
        edit={edit}
        onSave={atualizarTarefas}
        // Passe os outros dados necessários aqui
      />
    </div>
  );
};

export default TarefaView;
