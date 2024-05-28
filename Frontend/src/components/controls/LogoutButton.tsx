import React from "react";
import useLogout from "@/hooks/useLogout";
import StyledButton from "./Button";

const LogoutButton: React.FC = () => {
    const logout = useLogout();

    return (
        <StyledButton onClick={logout}>Log out</StyledButton>
    );
};

export default LogoutButton;