import Status from './Status'
import { Categoria } from './Categoria'

export class Tarefa {
  Id?: number;
  Titulo: string;
  Descricao: string;
  Status: number;
  DataDeEncerramento?: Date;
  DataDeVencimento: Date;
  UsuarioId: number;
  CategoriasId: number[];

  constructor(
    titulo: string,
    descricao: string,
    status: number,
    dataDeVencimento: Date,
    usuarioId: number,
    categoriasId: number[] = [],
    Id?: number
  ) {
    this.Titulo = titulo;
    this.Descricao = descricao;
    this.Status = status;
    this.DataDeVencimento = dataDeVencimento;
    this.UsuarioId = usuarioId;
    this.CategoriasId = categoriasId;
    if (Id !== undefined) {
      this.Id = Id; // Define apenas se fornecido
    }
  }
}