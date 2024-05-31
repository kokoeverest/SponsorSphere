import React, { useContext } from "react";
import StyledButton from "./Button";
import AuthContext from "@/context/AuthContext";
import { api } from "@/api/api";
import { LOGOUT_URL } from "@/common/constants";
import { useNavigate } from "react-router-dom";

const LogoutButton: React.FC = () => {
    const { logout } = useContext(AuthContext);
    const navigate = useNavigate();

    const logoutHandler = () => {
        // TODO: hit endpoint in the api to invalidate the cookie.
        api.delete(LOGOUT_URL);
        logout();
        navigate('/');
    }

    return (
        <StyledButton onClick={logoutHandler}>Log out</StyledButton>
    );
};

export default LogoutButton;