import React from "react";
import { Button } from "@mui/material";
import './Button.css'

interface ButtonProps {
    setIsLoggedIn?: React.Dispatch<React.SetStateAction<boolean>>;
    onClick?: () => void;
    name?: string;
    className?: string;
    type?: string;
}

const StyledButton: React.FC<ButtonProps> = (props) => (
    <Button variant="contained" {...props} >{props.name}</Button>
);

export default StyledButton;