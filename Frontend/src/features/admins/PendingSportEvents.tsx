import React from 'react';
import StyledButton from '@/components/controls/Button';
import { Badge } from '@mui/material';
import { usePendingSportEvents } from '@/context/PendingSportEventsContext';

const PendingSportEvents: React.FC = () =>
{
    const { count } = usePendingSportEvents();

    return (
        <Badge badgeContent={ count } max={ 99 } color='error'>
            <StyledButton sx={ { backgroundColor: 'var(--backGroundGrey)' } }>
                Pending Sport Events
            </StyledButton>
        </Badge>
    );
};

export default PendingSportEvents;
