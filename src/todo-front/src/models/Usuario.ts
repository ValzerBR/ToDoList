import { Tarefa } from "./Tarefa";

// src/models/Usuario.ts
export class Usuario {
  id?: number; // Opcional
  nome: string;
  email: string;
  senha: string;
  
  constructor(nome: string, email: string,senha: string, id?: number) {
      this.nome = nome;
      this.email = email;
      this.senha = senha;
      if (id !== undefined) {
          this.id = id; // Define apenas se fornecido
      }
  }
}