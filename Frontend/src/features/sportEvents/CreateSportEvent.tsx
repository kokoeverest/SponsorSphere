import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';

const CreateSportEvent: React.FC = () =>
    {
        const navigate = useNavigate();
        const createSportEventHandler  = () => {
            navigate('/achievements/sportEvents/create')
        };
    return (
        <StyledButton onClick={createSportEventHandler}>Create sport event</StyledButton>
    );
};

export default CreateSportEvent;