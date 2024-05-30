import React, { useContext } from "react";
import StyledButton from "./Button";
import AuthContext from "@/context/AuthContext";
import { api } from "@/api/api";
import { LOGOUT_URL } from "@/common/constants";

const LogoutButton: React.FC = () => {
    const { logout } = useContext(AuthContext);

    const logoutHandler = () => {
        // TODO: hit endpoint in the api to invalidate the cookie.
        api.delete(LOGOUT_URL);
        logout();
    }

    return (
        <StyledButton onClick={logoutHandler}>Log out</StyledButton>
    );
};

export default LogoutButton;