// src/components/WelcomePage.tsx
import React from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from './controls/Button';

const WelcomePage: React.FC = () => {
    const navigate = useNavigate();

    const handleRegisterClick = () => {
        navigate('/register');
    };

    const handleExploreClick = () => {
        navigate('/athletes');
    };

    return (
        <div className="welcome-page">
            <h1>Welcome to SponsorSphere</h1>
            <p>Your one-stop platform for athlete sponsorship.</p>
            <div className="container-buttons">
                <StyledButton onClick={handleRegisterClick} name="Register"></StyledButton>
                <StyledButton onClick={handleExploreClick} name="Explore"></StyledButton>
            </div>
        </div>
    );
};

export default WelcomePage;