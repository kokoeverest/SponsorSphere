import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from '../components/controls/Button';
import { Box, Divider } from '@mui/material';
import AuthContext from '@/context/AuthContext';
import StyledText from '@/components/controls/Typography';

const WelcomePage: React.FC = () =>
{
    const { isLogged } = useContext( AuthContext );
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
        <>
            <Box sx={ { p: 5 } }>
                <StyledText variant='h2'>Welcome to SponsorSphere</StyledText>
                <StyledText variant='h4'>Your one-stop platform for athlete sponsorship.</StyledText>
            </Box>
            <Box className="welcome-page">
                <Divider></Divider>
                { isLogged
                    ? <StyledButton onClick={ handleExploreClick }>Explore</StyledButton>
                    : <div className='container-buttons'>
                        <StyledButton onClick={ handleRegisterClick }>Register</StyledButton>
                        <StyledButton onClick={ handleLoginClick }>Login</StyledButton>
                    </div>
                }
            </Box>
        </>
    );
};

export default WelcomePage;