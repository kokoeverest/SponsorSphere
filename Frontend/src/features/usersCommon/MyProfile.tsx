import StyledButton from '@/components/controls/Button';
import AuthContext from '@/context/AuthContext';
import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { urlBuilder } from '@/common/helpers';

const MyProfile: React.FC = () =>
{
    const navigate = useNavigate();
    const { id, role, userType } = useContext( AuthContext );

    const myProfileClickHandler = () =>
    {
        if ( role != 'Admin' )
        {
            navigate( urlBuilder( id!, role!, userType! ) );
            console.log( role );
        }
        else
        {
            navigate( '/feed' );
        }
    };

    return (
        <StyledButton
            onClick={ myProfileClickHandler }
            sx={ {
                color: 'black',
                backgroundColor: 'var(--backGroundOrange)'
            } }
        >
            My Profile
        </StyledButton>
    );
};

export default MyProfile;