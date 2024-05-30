import React from 'react';
import StyledButton from '@/components/controls/Button';
import sportEventApi from '@/api/sportEventApi';

const CreateSportEvent: React.FC = () =>
    {
        const createSportEventHandler  = () => {
            sportEventApi.createSportEvent()
        };
    return (
        <StyledButton onClick={createSportEventHandler}>Create new sport event</StyledButton>
    );
};

export default CreateSportEvent;