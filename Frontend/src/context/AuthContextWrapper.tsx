import { useState } from "react";
import AuthContext from "./AuthContext";
import { UserInfoResponse } from "@/api/userApi";

interface AuthContextWrapperProps {
    children: React.ReactNode;
}

const AuthContextWrapper: React.FC<AuthContextWrapperProps> = ({children}) => {
    const isLoggedLocalStorage = localStorage.getItem('isLogged') === "true";
    const userNameLocalStorage = localStorage.getItem('userName');
    const roleLocalStorage = localStorage.getItem('role');

    const [isLogged, setLogged] = useState(isLoggedLocalStorage);
    const [userName, setUserName] = useState(userNameLocalStorage);
    const [role, setRole] = useState(roleLocalStorage);

    const login = (userData: UserInfoResponse) => {
        if(!isLogged) {
            localStorage.setItem("isLogged", "true");
            localStorage.setItem("userName", userData.userName);
            localStorage.setItem("role", userData.role);

            setLogged(true);
            setUserName(userData.userName);
            setRole(userData.role);
        }
    };

    const logout = () => {
        localStorage.clear();

        setLogged(false);
        setUserName(null);
        setRole(null);
    };

    return <AuthContext.Provider value={{ isLogged, userName, role, login, logout }}>
        {children}
    </AuthContext.Provider>
};

export default AuthContextWrapper;