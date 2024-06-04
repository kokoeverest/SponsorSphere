import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import StyledButton from './controls/Button';
import LogoutButton from './controls/LogoutButton';
import AuthContext from '@/context/AuthContext';

const Header: React.FC = () =>
{
  const authData = useContext( AuthContext );

  const navigate = useNavigate();

  const handleTitleClick = () =>
  {
    navigate( '/' );
  };

  const handleDashboardClick = () =>
  {
    navigate( '/dashboard' );
  };

  return (
    <div className="header">
      <h1 className='headerTitle' onClick={ handleTitleClick }>SponsorSphere</h1>
      { authData.isLogged
        && <div className="container-buttons">
          <StyledButton onClick={ handleDashboardClick }>Dashboard</StyledButton>
          <LogoutButton></LogoutButton>
        </div>
      }
    </div>
  );
};

export default Header;