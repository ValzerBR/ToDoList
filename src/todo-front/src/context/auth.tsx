import React, { createContext, useState, useContext, ReactNode, useEffect } from 'react';
import { Usuario } from '../models/Usuario';

interface AuthContextType {
    usuario: Usuario | null;
    isLogged: (usuario: Usuario) => void;
    logout: () => void;
}

interface AuthProviderProps {
    children: ReactNode; 
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
    const [usuario, setUsuario] = useState<Usuario | null>(() => {
        const storedUser = localStorage.getItem("usuario");
        return storedUser ? JSON.parse(storedUser) : null;
    });

    useEffect(() => {
        if (usuario) {
            localStorage.setItem("usuario", JSON.stringify(usuario));
        } else {
            localStorage.removeItem("usuario");
        }
    }, [usuario]);

    const isLogged = (usuario: Usuario) => setUsuario(usuario);
    const logout = () => {
        setUsuario(null);
        localStorage.removeItem("usuario"); // Remover usuário ao deslogar
    };

    return (
        <AuthContext.Provider value={{ usuario, isLogged, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = (): AuthContextType => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};