import { ReactNode, useState } from "react";
import AuthContext from "./AuthContext";
import { UserInfoResponse } from "@/api/userApi";

const AuthContextWrapper: React.FC<ReactNode> = (children) => {
    const isLoggedLocalStorage = localStorage.getItem('isLogged') === "true";
    const userNameLocalStorage = localStorage.getItem('userName');
    const roleLocalStorage = localStorage.getItem('role');

    const [isLogged, setLogged] = useState(isLoggedLocalStorage);
    const [userName, setUserName] = useState(userNameLocalStorage);
    const [role, setRole] = useState(userNameLocalStorage);

    const login = (userData: UserInfoResponse) => {
        setLogged(true);
        setUserName(userData.userName);
        setRole(userData.role);
    };

    const logout = () => {};

    return <AuthContext.Provider value={{ isLogged, login, logout }}>
        {children}
    </AuthContext.Provider>
};

export default AuthContextWrapper;