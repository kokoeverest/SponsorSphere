import React from 'react';
import StyledButton from '@/components/controls/Button';
import { Badge } from '@mui/material';
import sportEventApi from '@/api/sportEventApi';

let count: number = 0;

try
{
    count = await sportEventApi.getPendingSportEventsCount();
} catch ( error )
{
    console.error( error );
}

const PendingSportEvents: React.FC = () =>
{

    
    return (
        <Badge badgeContent={ count } max={ 9 } color='error'>
            <StyledButton sx={ { backgroundColor: 'var(--backGroundOrange)' } }>Pending Sport Events</StyledButton>
        </Badge>
    );
};

export default PendingSportEvents;