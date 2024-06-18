import { createContext } from "react";
import { AuthData } from "@/types/authData";

const defaultValue = { 
    isLogged: false, 
    id: null, 
    role: null, 
    userName: null, 
    userType: null,
    login: () => {}, 
    logout: () => {} 
};

const AuthContext = createContext<AuthData>(defaultValue);

export default AuthContext;