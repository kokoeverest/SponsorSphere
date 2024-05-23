import React from "react";
import { Button, ButtonProps } from "@mui/material";
import './Button.css'

const StyledButton: React.FC<ButtonProps> = ({children, ...rest}) => (
    <Button variant="contained" {...rest} >{children}</Button>
);

export default StyledButton;