import React, { useState } from 'react';
import { Dialog } from 'primereact/dialog';
import { Button } from 'primereact/button';
import { MultiSelect } from 'primereact/multiselect';

interface FiltroModalProps {
  visible: boolean;
  onClose: () => void;
  onApply: (filters: { status?: number[]; dataInicial?: Date; dataFinal?: Date }) => void;
}

interface FiltroTarefa {
  status?: number[];
  dataInicial?: Date | null;
  dataFinal?: Date | null;
}

const FiltroModal: React.FC<FiltroModalProps> = ({ visible, onClose, onApply }) => {
  const [statusSelecionados, setStatusSelecionados] = useState<number[]>([]);

  const statusOptions = [
    { label: 'Pendente', value: 1 },
    { label: 'Em Andamento', value: 2 },
    { label: 'Concluída', value: 3 },
    { label: 'Cancelada', value: 4 },
    { label: 'Vencida', value: 5 },
  ];

  // Função para aplicar o filtro de datas
  const handleApply = () => {
    const filtros: FiltroTarefa = {
      status: statusSelecionados
    };

    onApply(filtros);
  };

  const footer = (
    <div className="flex justify-end gap-2">
      <Button label="Cancelar" icon="pi pi-times" className="p-button-text" onClick={onClose} />
      <Button label="Aplicar" icon="pi pi-check" className="p-button" onClick={handleApply} />
    </div>
  );

  return (
    <Dialog
      visible={visible}
      onHide={onClose}
      header="Filtrar Tarefas"
      footer={footer}
      modal
      style={{ width: '30vw' }}
    >
      <div className="p-fluid">
        <div className="field">
          <label htmlFor="status">Status</label>
          <MultiSelect
            id="status"
            options={statusOptions}
            value={statusSelecionados}
            onChange={(e) => setStatusSelecionados(e.value)}
            placeholder="Selecione o(s) status"
            className="w-full"
          />
        </div>
      </div>
    </Dialog>
  );
};

export default FiltroModal;
