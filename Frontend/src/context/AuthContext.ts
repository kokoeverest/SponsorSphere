import { createContext } from "react";
import { AuthData } from "@/types/authData";

const AuthContext = createContext<AuthData>();

export default AuthContext;