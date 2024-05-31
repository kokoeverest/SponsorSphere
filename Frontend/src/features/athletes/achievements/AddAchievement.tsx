import React from 'react'
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';


const AddAchievement: React.FC = () => {
  const navigate = useNavigate();

  const addAchievementHandler = () =>
  {
    // TODO: hit endpoint in the api to invalidate the cookie.
    
    navigate( '/achievements/create' );
  }
  return (
    <>
    
    <StyledButton onClick={addAchievementHandler}> Add achievement</StyledButton>
    </>
  );
};

export default AddAchievement;