import React from 'react';
import { Card } from 'primereact/card';
import { Button } from 'primereact/button';
import { Toolbar } from 'primereact/toolbar';
import Status from '../../models/Status';
import { Tarefa } from '../../models/Tarefa';

interface TaskCardProps {
  tarefa: Tarefa;
  onEdit: (id: number) => void;
  onDelete?: (id: number) => void;
}

const TaskCard = ({ tarefa, onEdit, onDelete}: TaskCardProps) => {
  return (
    <Card style={{width: "335px"}} className="shadow-lg p-4 border-2 border-gray-300 rounded-lg bg-white">
      <h3 className="text-xl font-extrabold text-gray-800">{tarefa.Titulo}</h3>

      <p className="text-sm text-gray-600">{tarefa.Descricao}</p>

      <p className="text-sm text-gray-500 mt-2">Vence em: {String(tarefa.DataDeVencimento) || 'Data não definida'}</p>

      <span className={`inline-block px-3 py-1 text-sm font-semibold mt-2 rounded-lg ${getStatusColor(tarefa.Status)}`}>
        {getStatusName(tarefa.Status)}
      </span>

      <div className="mt-4 flex gap-2">
        <Button label="Editar" icon="pi pi-pencil" onClick={() => {onEdit(tarefa.Id); }} className="p-button-sm p-button-outlined"/>
        <Button label="Excluir" icon="pi pi-trash" onClick={() => {onDelete(tarefa.Id);}} className="p-button-sm p-button-danger" />
      </div>
    </Card>
  );
};

// Função para definir a cor do status dinamicamente
const getStatusColor = (status: number) => {
  switch (status) {
    case 1: return 'bg-yellow-100 text-yellow-800';
    case 2: return 'bg-blue-100 text-blue-800';
    case 3: return 'bg-green-100 text-green-800';
    case 4: return 'bg-red-100 text-red-800';
    case 5: return 'bg-gray-300 text-gray-700';
    default: return 'bg-gray-100 text-gray-800';
  }
};

const getStatusName = (status: number) => {
  switch (status) {
    case 1: return 'Pendente';
    case 2: return 'Em Andamento';
    case 3: return 'Concluída';
    case 4: return 'Cancelada';
    case 5: return 'Vencida';
    default: return 'Pendente';
  }
};

export default TaskCard;
