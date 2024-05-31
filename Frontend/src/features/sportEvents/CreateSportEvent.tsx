import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';

export const CreatePastSportEvent: React.FC = () =>
    {
        const navigate = useNavigate();
        const createSportEventHandler  = () => {
            navigate('/achievements/sportEvents/create?eventType=achievement')
        };
    return (
        <StyledButton onClick={createSportEventHandler}>Create sport event</StyledButton>
    );
};

export const CreateFutureSportEvent: React.FC = () =>
{
    const navigate = useNavigate();
    const createSportEventHandler = () =>
    {
        navigate( '/achievements/sportEvents/create?eventType=goal' );
    };
    return (
        <StyledButton onClick={ createSportEventHandler }>Create sport event</StyledButton>
    );
};