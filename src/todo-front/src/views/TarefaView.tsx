import React, { useState, useEffect } from 'react';
import TarefaCard from '../components/TelaTarefa/TarefaCard';
import { Toolbar } from 'primereact/toolbar';
import { Button } from 'primereact/button';
import { IconField } from 'primereact/iconfield';
import { InputText } from 'primereact/inputtext';
import TarefaModal from '../components/TelaTarefa/TarefaModal';
import FiltroModal from '../components/TelaTarefa/FiltroModal';
import { useAuth } from '../context/auth';
import { useNavigate } from 'react-router-dom';
import { UsuarioController } from '../controllers/UsuarioController';
import { Tarefa } from '../models/Tarefa';
import { TarefaController } from '../controllers/TarefaController';

const TarefaView = () => {
  const { usuario, logout } = useAuth();
  const [tarefas, setTarefas] = useState<Tarefa[]>([]);
  const [idTarefa, setIdTarefa] = useState<number | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>('');
  const [filterVisible, setFilterVisible] = useState<boolean>(false);
  const [filters, setFilters] = useState({
    status: [] as number[],  // Garantindo que status sempre seja um array, mesmo vazio
  });

  const atualizarTarefas = (filters: any = {}) => {
    if (usuario) {
      UsuarioController.detail(usuario.id).then((response) => {
        let filteredTarefas = response.Tarefas;
  
        // Filtrando por Status
        if (filters.status.length > 0) {
          filteredTarefas = filteredTarefas.filter((tarefa: Tarefa) =>
            filters.status.includes(tarefa.Status)
          );
        }
  
        setTarefas(filteredTarefas);
      });
    }
  };
  

  useEffect(() => {
    atualizarTarefas(filters); // Atualiza as tarefas com os filtros
  }, [usuario, filters]);

  const navigate = useNavigate();
  const [visible, setVisible] = useState<boolean>(false);
  const [edit, setEditState] = useState<boolean>(false);

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
    TarefaController.delete(id).then(() => {
      atualizarTarefas(filters);
    });
  };

  const startContent = (
    <React.Fragment>
      <Button
        label="Criar tarefa"
        icon="pi pi-plus"
        className="mr-2"
        onClick={() => {
          setVisible(true);
          setIdTarefa(null);
        }}
      />
    </React.Fragment>
  );

  const search = () => {
    TarefaController.search(searchTerm, usuario.id).then((response) => {
      setTarefas(response);
    });
  };

  const centerContent = (
    <IconField iconPosition="left">
      <InputText
        placeholder="Consultar"
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
      />
      <Button
        icon="pi pi-search"
        onClick={search}
        className="p-button p-button-outlined p-button-info"
      />
      <Button
        icon="pi pi-filter"
        onClick={() => setFilterVisible(true)} // Abre o filtro
        className="p-button p-button-outlined p-button-info"
      />
    </IconField>
  );

  const endContent = (
    <Button
      icon="pi pi-sign-out"
      className="p-button p-button-warning"
      onClick={() => {
        logout();
        navigate('/');
      }}
    ></Button>
  );

  return (
    <div>
      <Toolbar start={startContent} center={centerContent} end={endContent} />
      <div className="min-h-screen bg-gray-100 p-6">
        <h1 className="text-2xl font-bold text-center mb-6">Minhas Tarefas</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {tarefas?.map((tarefa) => (
            <TarefaCard
              key={tarefa.Id}
              tarefa={tarefa}
              onEdit={handleEdit}
              onDelete={handleRemove}
            />
          ))}
        </div>
      </div>
      <TarefaModal
        visible={visible}
        onClose={() => {
          setVisible(false);
          setEditState(false);
        }}
        idDaTarefa={idTarefa}
        edit={edit}
        onSave={() => atualizarTarefas(filters)} // Atualiza as tarefas após salvar
      />
      <FiltroModal
        visible={filterVisible}
        onClose={() => setFilterVisible(false)}
        onApply={(newFilters) => {
          const updatedFilters = {
            status: newFilters.status || [], // Se não houver status, usa um array vazio
          };
          setFilters(updatedFilters); // Aplica os filtros corretamente
          setFilterVisible(false); // Fecha o filtro
        }}
      />
    </div>
  );
};

export default TarefaView;

