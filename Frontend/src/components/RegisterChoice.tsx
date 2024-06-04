import React from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from './controls/Button';
import { Box } from '@mui/material';

const RegisterChoice: React.FC = () => {
    const navigate = useNavigate();

    const handleRegisterAsAthlete = () => {
        navigate('/register/athlete');
    };

    const handleRegisterAsCompany = () => {
        navigate('/register/company');
    };

    const handleRegisterAsIndividual = () => {
        navigate('/register/individual');
    };

    return (
        <Box className="register-choice">
            <h2>Please select the correct profile you want to create:</h2>
            <div className="container-buttons">
                <StyledButton onClick={handleRegisterAsAthlete}>I am an Athlete</StyledButton>
                <StyledButton onClick={handleRegisterAsCompany}>I represent a Company Sponsor</StyledButton>
                <StyledButton onClick={handleRegisterAsIndividual}>I am an Individual Sponsor</StyledButton>
            </div>
        </Box>
    );
};

export default RegisterChoice;