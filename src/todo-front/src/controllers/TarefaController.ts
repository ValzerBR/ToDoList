import { Tarefa } from "../models/Tarefa";
import { TarefaService } from "../services/TarefaService";


export const TarefaController = {
    async cadastrar(tarefa: Tarefa) {
      try {
        const response = await TarefaService.cadastrar(tarefa);
        return response;
      } catch (error) {
        console.error("Erro ao cadastrar a tarefa.:", error);
        throw error;
      }
    }, 

    async atualizar(tarefa: Tarefa) {
        try {
          const response = await TarefaService.update(tarefa);
          return response;
        } catch (error) {
          console.error("Erro ao atualizar a tarefa.:", error);
          throw error;
        }
    }, 

    async detail(id: number) {
        try {
          const response = await TarefaService.detail(id);
          return response;
        } catch (error) {
          console.error("Erro ao detalhar a tarefa.:", error);
          throw error;
        }
    }, 

    async delete(id: number) {
        try {
          const response = await TarefaService.delete(id);
          return response;
        } catch (error) {
          console.error("Erro ao deletar a tarefa.:", error);
          throw error;
        }
    }, 

    async search(titulo: string, usuarioId: number) {
        try {
          const response = await TarefaService.search(titulo, usuarioId);
          return response;
        } catch (error) {
          console.error("Erro ao deletar a tarefa.:", error);
          throw error;
        }
    }
}