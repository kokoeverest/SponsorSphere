import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import LogoutButton from './controls/LogoutButton';
import AuthContext from '@/context/AuthContext';
import Dashboard from './Dashboard';

const Header: React.FC = () =>
{
  const authData = useContext( AuthContext );

  const navigate = useNavigate();

  const handleTitleClick = () =>
  {
    navigate( '/' );
  };

  return (
    <div className="header">
      <h1 className='headerTitle' onClick={ handleTitleClick }>SponsorSphere</h1>
      { authData.isLogged
        && <div className="container-buttons">
          <Dashboard />
          <LogoutButton></LogoutButton>
        </div>
      }
    </div>
  );
};

export default Header;