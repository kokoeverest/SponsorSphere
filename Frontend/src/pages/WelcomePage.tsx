import React from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from '../components/controls/Button';
import { Box } from '@mui/material';
import { useAuth } from '@/hooks/useAuth';

const WelcomePage: React.FC = () => {

    const { isAuthenticated } = useAuth();
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
            <Box className="container-buttons">
                { !isAuthenticated 
                ? <StyledButton onClick={handleRegisterClick}>Register</StyledButton> 
                : <StyledButton onClick={handleExploreClick}>Explore</StyledButton>}
            </Box>
        </Box>
    );
};

export default WelcomePage;