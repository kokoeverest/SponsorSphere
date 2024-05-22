// src/components/Header.tsx
import React from 'react';
import { useNavigate } from 'react-router-dom';

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
      {!isLoggedIn && <button className="loginButton" onClick={handleLoginClick}>Login</button>}
    </div>
  );
};

export default Header;