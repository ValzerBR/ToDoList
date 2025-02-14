import axios from "axios";
import { Usuario } from "../models/Usuario";

const url = "http://localhost:8080/Usuario/"

export const UsuarioService = {
    async cadastrar(usuario: Usuario) {
        try {
            const response = await axios.post(`${url}Create`, usuario);
            return response.data;
        } catch (error) {
            // Trata o erro
            if (axios.isAxiosError(error)) {
                throw new Error("Ocorreu um erro ao cadastrar o usuário.");
            } else {
                console.error("Erro desconhecido:", error);
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    },

    async login(email: string, senha: string) {
        try {
            const response = await axios.get(`${url}GetByEmail?email=${email}`);
            const user = response.data;
            if (user && user.Senha === senha) {
                return user;
            } else {
                throw new Error("Email ou senha incorretos.");
            }
        } catch (error) {
            if (axios.isAxiosError(error)) {
                throw new Error("Ocorreu um erro ao buscar o usuário.");
            } else {
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
                throw new Error("Ocorreu um erro ao buscar o usuário.");
            } else {
                throw new Error("Ocorreu um erro inesperado.");
            }
        }
    }
};
