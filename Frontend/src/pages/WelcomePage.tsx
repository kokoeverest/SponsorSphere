import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from '../components/controls/Button';
import { Box } from '@mui/material';
import AuthContext from '@/context/AuthContext';

const WelcomePage: React.FC = () =>
{
    const { isLogged } = useContext( AuthContext );
    // const { isAuthenticated } = useAuth();
    const navigate = useNavigate();

    const handleRegisterClick = () =>
    {
        navigate( '/register' );
    };

    const handleLoginClick = () =>
    {
        navigate( '/login' );
    };


    const handleExploreClick = () =>
    {
        navigate( '/feed' );
    };

    return (
        <Box className="welcome-page">
            <h1>Welcome to SponsorSphere</h1>
            <p>Your one-stop platform for athlete sponsorship.</p>
                { isLogged
                    ? <StyledButton onClick={ handleExploreClick }>Explore</StyledButton>
                    : <div className='container-buttons'>
                        <StyledButton onClick={ handleRegisterClick }>Register</StyledButton>
                        <StyledButton onClick={ handleLoginClick }>Login</StyledButton>
                    </div>

                }
        </Box>
    );
};

export default WelcomePage;