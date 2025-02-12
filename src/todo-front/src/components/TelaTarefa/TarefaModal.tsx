import { Dialog, DialogProps } from 'primereact/dialog';
import React, { useEffect } from 'react';
import { Button } from 'primereact/button';
import { Panel } from 'primereact/panel';
import { InputText } from 'primereact/inputtext';
import { useState } from "react";
import { Calendar } from "primereact/calendar";
import { MultiSelect } from "primereact/multiselect";
import { Fieldset } from "primereact/fieldset";
import { TarefaController } from '../../controllers/TarefaController';
import { useAuth } from '../../context/auth';
        
interface TarefaModalProps {
  visible: boolean;
  onClose: () => void;
  idDaTarefa: number | null; // Recebe o id da tarefa
  edit: boolean;
  onSave: () => void;
}

const TarefaModal = ({
  visible,
  onClose,
  edit,
  idDaTarefa,
  onSave
}: TarefaModalProps) => {

const [title, setTitulo] = useState('');
const [description, setDescricao] = useState('');
const [dataDeVencimento, setDataVencimento] = useState<Date | null>(null);
const [opcoesSelecionadas, setOpcoesSelecionadas] = useState<number[]>([]);
const [editingTarefaId, setEditingTarefaId] = useState<boolean | null>(null); // Estado para armazenar o ID da tarefa em edição
const { usuario } = useAuth();
const [tarefaId, setTarefaId] = useState<number>();
const [error, setError] = useState({
  titulo: false,
  descricao: false
});


const handleClose = () => {
  setTitulo('');
  setDescricao('');
  setDataVencimento(null);
  setOpcoesSelecionadas([]);
  onClose();
  setEditingTarefaId(false);
};

  const opcoes = [
      { label: "Pendente", value: 1 },
      { label: "Em Andamento", value: 2 },
      { label: "Concluída", value: 3 },
      { label: "Cancelada", value: 4 },
      { label: "Vencida", value: 5 },
  ];

useEffect(() => {
  if(visible && idDaTarefa)
  TarefaController.detail(idDaTarefa)
  .then((response) =>{
    setTitulo(response.Titulo);
    setDescricao(response.Descricao);
    setDataVencimento(converterParaData(response.DataDeVencimento));
    setOpcoesSelecionadas([response.Status]);
  })
}, [visible, idDaTarefa]);

function converterParaData(dataString: string): Date {
  const [dia, mes, ano] = dataString.split('/').map(Number);
  return new Date(ano, mes - 1, dia); // Ajustando o mês (JS usa base 0)
}

const save = () => {
  const newError = {
    titulo: !title || title.trim() === '',
    descricao: !description || description.trim() === ''
  };
  
  setError(newError);

  if (Object.values(newError).some(err => err)) {
    return;
  }

  if(editingTarefaId || edit){
    const formData = {
      id: idDaTarefa,
      Titulo: title,
      Descricao: description,
      Status: opcoesSelecionadas[0],
      DataDeEncerramento: null,
      DataDeVencimento: dataDeVencimento,
      UsuarioId: usuario.id,
      CategoriasId: []
    }

    TarefaController.atualizar(formData)
     .then((response) => {
      setTarefaId(response.Id);
      setEditingTarefaId(true);
      onSave();
    });
  }
  else{
    const formData = {
      Titulo: title,
      Descricao: description,
      Status: opcoesSelecionadas[0],
      DataDeEncerramento: null,
      DataDeVencimento: dataDeVencimento,
      UsuarioId: usuario.id,
      CategoriasId: []
    }

    console.log(opcoesSelecionadas[0])
    TarefaController.cadastrar(formData)
     .then((response) => {
      setTarefaId(response.Id);
      setEditingTarefaId(true);
      onClose();
      onSave();
    });
  }
};

  const footerStyle = {
    padding: "5px",
    height: "60px", // Reduz a altura do footer
    display: "flex",
    justifyContent: "flex-end",
    alignItems: "center",
};

  const footer = (
      <Fieldset className="flex justify-end gap-2" style={footerStyle}>
          <Button label="Cancelar" icon="pi pi-times" onClick={() => {handleClose(); setEditingTarefaId(false)}} className="p-button-text" />
          <Button label="Salvar" icon="pi pi-check" onClick={() => {save(); }} />
      </Fieldset>
  );

  const fieldsetStyle = {
    width: "100%",
    minHeight: "40px", // Reduz a altura do Fieldset
    padding: "5px", // Reduz o espaçamento interno
    display: "flex",
    alignItems: "center",
    marginBottom: "5px"
};

  return (
    <Dialog
    header="Nova Tarefa"
    blockScroll={true}
    visible={visible}
    resizable={false}
    style={{
      width: "40vw",
      maxHeight: "none",
      borderRadius: "10px", // Borda arredondada
      display: "flex",
      flexDirection: "column",
    }}
    draggable={false}
    onHide={handleClose}
    footer={<div style={footerStyle}>{footer}</div>}
    modal
    >
    <Panel header="">
        {/* Linha 1: Título e Descrição */}
        <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Fieldset legend="Título" style={fieldsetStyle}>
                <InputText id="titulo" 
                value={title} 
                className={`w-full ${error.titulo ? 'p-invalid' : ''}`}
                onChange={(e) => {
                  setTitulo(e.target.value);
                if (error.titulo) setError(prev => ({...prev, titulo: false}));
                }}
                placeholder='Título'
                />
            </Fieldset>
            <Fieldset legend="Descrição" style={fieldsetStyle}>
                <InputText id="descricao"
                 value={description} 
                 type='string' 
                 maxLength={50}
                 className={`w-full ${error.descricao ? 'p-invalid' : ''}`}
                 onChange={(e) => {
                  setDescricao(e.target.value);
                if (error.descricao) setError(prev => ({...prev, descricao: false}));
                }}
                 placeholder='Descrição'
                 required={true}
                 />
            </Fieldset>
        </div>

        {/* Linha 2: Data de Vencimento e Opções */}
        <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Fieldset legend="Data de Vencimento" style={fieldsetStyle}>
                <Calendar id="data" 
                value={dataDeVencimento} 
                showIcon 
                dateFormat="dd/mm/yy"
                onChange={(e) => {
                  setDataVencimento(e.target.value);
                }}
                 />
            </Fieldset>
            <Fieldset legend="Definir Status" style={fieldsetStyle}>
              <div className="flex items-center gap-2">
                <MultiSelect
                  id="selecionados"
                  options={opcoes}
                  value={opcoesSelecionadas}
                  placeholder="Pesquise e selecione"
                  className="w-full"
                  maxSelectedLabels={1}
                  onChange={e => setOpcoesSelecionadas(e.value.length > 1 ? [e.value[1]] : e.value)}
                />
              </div>
            </Fieldset>
        </div>
    </Panel>
</Dialog>
  );
};

export default TarefaModal;