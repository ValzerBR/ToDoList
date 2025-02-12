import { InputText } from 'primereact/inputtext';
import React from 'react';

interface InputFieldProps {
    label: string,
    value: string,
    onChange: (value: string) => void,
    placeholder?: string,
    type?: string,
};


const InputField = ({label, value, onChange, placeholder, type = 'text'}: InputFieldProps) => {
    return(
        <div>
            <label className="block text-gray-700 mb-1">{label}</label>
            <InputText 
                value={value} 
                onChange={(e) => onChange(e.target.value)} 
                placeholder={placeholder} 
                className="w-full" 
                type={type}
            />
        </div>
    );
};

export default InputField;