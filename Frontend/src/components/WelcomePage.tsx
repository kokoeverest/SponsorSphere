// src/components/WelcomePage.tsx
import React from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from './controls/Button';
import { Box } from '@mui/material';

const WelcomePage: React.FC = () => {
    const navigate = useNavigate();

    const handleRegisterClick = () => {
        navigate('/register');
    };

    const handleExploreClick = () => {
        navigate('/athletes');
    };

    return (
        <Box className="welcome-page">
            <h1>Welcome to SponsorSphere</h1>
            <p>Your one-stop platform for athlete sponsorship.</p>
            <div className="container-buttons">
                <StyledButton onClick={handleRegisterClick}>Register</StyledButton>
                <StyledButton onClick={handleExploreClick}>Explore</StyledButton>
            </div>
        </Box>
    );
};

export default WelcomePage;