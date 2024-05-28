import React from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from './controls/Button';
import LogoutButton from './controls/LogoutButton';

interface HeaderProps {
  isLoggedIn: boolean;
  setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
}

const Header: React.FC<HeaderProps> = ({ isLoggedIn }) => {
  const navigate = useNavigate();

  const handleTitleClick = () => {
    navigate('/');
  };
  
  const handleLoginClick = () => {
    navigate('/login');
  };

  return (
    <div className="header">
      <h1 className='headerTitle' onClick={handleTitleClick}>SponsorSphere</h1>
      { isLoggedIn && <LogoutButton></LogoutButton> || <StyledButton onClick={handleLoginClick}>Login</StyledButton>}
    </div>
  );
};

export default Header;