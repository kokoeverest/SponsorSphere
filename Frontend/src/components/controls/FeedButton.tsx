import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';


const FeedButton: React.FC = () =>
{
    const navigate = useNavigate();

    const feedHandler = () =>
    {
        navigate( '/feed' );
    }
    return (
        <StyledButton onClick={ feedHandler } sx={ { color: 'black', backgroundColor: 'var(--backGroundGrey)' } }>Explore</StyledButton>
    );
};

export default FeedButton;