import { Usuario } from "../models/Usuario";
import { UsuarioService } from "../services/UsuarioService";


export const UsuarioController = {
    async cadastrarTarefa(usuario: Usuario) {
      try {
        const response = await UsuarioService.cadastrar(usuario);
        return response;
      } catch (error) {
        console.error("Erro ao cadastrar usuario:", error);
        throw error;
      }
    },

    async login(email: any){
        try {
            const response = await UsuarioService.login(email);
            return response;
        } catch(error){
        }
    },

    async detail(id: number){
      try {
          const response = await UsuarioService.detail(id);
          return response;
      } catch(error){
      }
  },

  };