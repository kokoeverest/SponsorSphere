import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';
import { Badge } from '@mui/material';
import sportEventApi from '@/api/sportEventApi';

const count: number = await sportEventApi.getPendingSportEventsCount();

const PendingSportEvents: React.FC = () =>
{
    
    const navigate = useNavigate();
    const pendingSportEventsHandler = () =>
    {
        // navigate( '/sportEvents/pending' );
    };
    return (
        <Badge badgeContent={count} color='error'>
        <StyledButton onClick={ pendingSportEventsHandler } sx={ { backgroundColor: 'var(--backGroundOrange)' } }>Pending Sport Events</StyledButton>

        </Badge>
    );
};

export default PendingSportEvents;