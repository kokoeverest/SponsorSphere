import React, { useContext } from "react";
import StyledButton from "./Button";
import AuthContext from "@/context/AuthContext";

const LogoutButton: React.FC = () => {
    const { logout } = useContext(AuthContext);

    const logoutHandler = () => {
        logout();
        // TODO: hit endpoint in the api to invalidate the cookie.
    }

    return (
        <StyledButton onClick={logoutHandler}>Log out</StyledButton>
    );
};

export default LogoutButton;