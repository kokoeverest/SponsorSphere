import React from "react";
import { Button } from "@mui/material";
import './Button.css'

interface ButtonProps {
    setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
    onClick: React.FC;
    name: string;
}

const StyledButton: React.FC<ButtonProps> = (props) => (
    <Button variant="contained" className="loginButton" onClick={props.onClick}>{props.name}</Button>
);

export default StyledButton;