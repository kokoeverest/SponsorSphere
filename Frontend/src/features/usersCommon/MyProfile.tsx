import StyledButton from '@/components/controls/Button';
import AuthContext from '@/context/AuthContext';
import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';

const MyProfile: React.FC = () =>
{
    const { id, role } = useContext( AuthContext );
    const navigate = useNavigate();

    const myProfileClickHandler = () =>
    {
        if ( role != 'Admin' )
        {
            navigate( `/${ role?.toLowerCase() }s/${ id }` );
        }
        else
        {
            navigate( '/feed' );
        }
    };

    return (
        <StyledButton onClick={ myProfileClickHandler } sx={ { color: 'black', backgroundColor: 'var(--backGroundOrange)' } }>My Profile</StyledButton>
    );
};

export default MyProfile;