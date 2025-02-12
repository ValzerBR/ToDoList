import axios from "axios";
import { Tarefa } from "../models/Tarefa";

const url = "https://todo-todolist-app3-latest.onrender.com/Tarefa/"

export const TarefaService = {
    async cadastrar(tarefa: Tarefa) {
        try {
            const response = await axios.post(`${url}Create`, tarefa);
            return response.data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            } else {
                console.error("Erro desconhecido:", error);
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    },

    async update(tarefa: Tarefa) {
        try {
            const response = await axios.put(`${url}Update`, tarefa);
            return response.data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            } else {
                console.error("Erro desconhecido:", error);
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    },

    async detail(id: number){
        try {
            const response = await axios.get(`${url}Detail/${id}`);
            return response.data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                throw new Error("Ocorreu um erro ao buscar a tarefa.");
            } else {
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    },

    async delete(id: number){
        try {
            const response = await axios.delete(`${url}Delete?ids=${id}`);
            return response.data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                throw new Error("Ocorreu um erro ao deletar a tarefa.");
            } else {
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    }
}
