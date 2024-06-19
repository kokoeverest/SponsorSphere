import React, { useContext, useEffect, useState } from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';
import AuthContext from '@/context/AuthContext';
import { AthleteDto } from '@/types/athlete';
import { SponsorDto } from '@/types/sponsor';
import athleteApi from '@/api/athleteApi';
import sponsorCompanyApi from '@/api/sponsorCompanyApi';
import { SponsorIndividualDto } from '@/types/sponsorIndividual';
import { SponsorCompanyDto } from '@/types/sponsorCompany';
import { Box } from '@mui/material';

export const UpdateUserProfile: React.FC = () =>
{
    const { id, userType } = useContext( AuthContext );
    const [ athlete, setAthlete ] = useState<AthleteDto | null>( null );
    const [ sponsor, setSponsor ] = useState<SponsorIndividualDto | SponsorCompanyDto | SponsorDto | null>( null );

    const navigate = useNavigate();

    const fetchUser = async () =>
    {
        try
        {
            if ( userType === 'Athlete' && id )
            {
                setAthlete( await athleteApi.getAthleteById( id ) );
            }
            else if ( id && ( userType === 'SponsorCompany' || userType === 'SponsorIndividual' ) )
            {
                setSponsor( await sponsorCompanyApi.getSponsorCompanyById( id ) );
            }
        }
        catch ( error )
        {
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchUser();

    }, [ userType ] );

    return (
        <Box sx={ { p: 2 } }>
            <StyledButton
                sx={ { color: 'black', backgroundColor: 'var(--backGroundBlue)' } }
                onClick={ () => navigate( '/users/profile/update', { state: athlete ? athlete : sponsor } ) }
            >Edit profile</StyledButton>
        </Box>
    );
};