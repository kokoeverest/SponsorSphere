import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';


const AddGoal: React.FC = () =>
{
    const navigate = useNavigate();

    const addGoalHandler = () =>
    {
        navigate( '/goals/create' );
    };
    return (
        <>

            <StyledButton onClick={ addGoalHandler }> Add goal</StyledButton>
        </>
    );
};

export default AddGoal;