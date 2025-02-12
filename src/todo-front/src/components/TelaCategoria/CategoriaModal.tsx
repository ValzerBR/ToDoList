import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { Fieldset } from "primereact/fieldset";
import { InputText } from "primereact/inputtext";
import { Panel } from "primereact/panel";
import React, { useState } from "react";
import InputField from "../InputField";

interface CategoriaModalProps {
    visible: boolean,
    onHide: () => void
}

const CategoriaModal = ({visible, onHide} : CategoriaModalProps)  => {
const [categoria, setCategoria] = useState<string>('');

  const footerStyle = {
    padding: "5px",
    height: "60px", // Reduz a altura do footer
    display: "flex",
    justifyContent: "flex-end",
    alignItems: "center",
};

  const footer = (
      <Fieldset className="flex justify-end gap-2" style={footerStyle}>
          <Button label="Cancelar" icon="pi pi-times" onClick={onHide}className="p-button-text" />
          <Button label="Salvar" icon="pi pi-check" onClick={() => console.log("Salvar")} />
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
        header="Nova Categoria"
        visible={visible}
        resizable={false}
        style={{
            width: "30vw",
            maxHeight: "none",
            borderRadius: "10px", // Borda arredondada
            display: "flex",
            flexDirection: "column",
        }}
        draggable={false}
        onHide={onHide}
        footer={<div style={footerStyle}>{footer}</div>}
        modal
    >
        <Panel header="Adicionar Categoria">
            <InputField label="" value={categoria} onChange={setCategoria} placeholder="" />
        </Panel>
    </Dialog>
  )
}

export default CategoriaModal;