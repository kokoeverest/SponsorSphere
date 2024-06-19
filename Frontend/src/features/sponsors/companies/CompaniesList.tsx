import React, { useEffect, useState } from 'react';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import { List, ListItem, CircularProgress, Alert } from '@mui/material';
import { PAGE_SIZE as pageSize } from '@/common/constants';
import sponsorCompanyApi from '@/api/sponsorCompanyApi';
import { SponsorCompanyDto } from '../../../types/sponsorCompany';
import StyledPagination from '@/components/controls/Pagination';
import StyledText from '@/components/controls/Typography';
import { fetchPicture } from '@/common/helpers';

interface SponsorCompanyWithPicture extends SponsorCompanyDto
{
    pictureContent?: string;
}

const AthleteList: React.FC = () =>
{
    const [ sponsorCompanies, setSponsorCompanies ] = useState<SponsorCompanyWithPicture[]>( [] );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ pageNumber, setPageNumber ] = useState<number>( 1 );
    const [ totalPages, setTotalPages ] = useState<number>( 1 );

    const fetchCompanies = async ( page: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const queryParams = `?pageNumber=${ page }&pageSize=${ pageSize }&sport=Football`;
            const result: SponsorCompanyDto[] = await sponsorCompanyApi.getSponsorCompanies( queryParams );

            const sponsorCompaliesWithPictures = await Promise.all( result.map( async ( sponsorCompany ) =>
            {
                if ( sponsorCompany.pictureId )
                {
                    const pictureContent = await fetchPicture( sponsorCompany.pictureId );
                    return { ...sponsorCompany, pictureContent };
                }
                return sponsorCompany;
            } ) );

            setSponsorCompanies( sponsorCompaliesWithPictures );
        }
        catch ( error )
        {
            setError( "Failed to fetch companies" );
            console.error( error );
        }
        finally
        {
            setLoading( false );
        }
    };

    const fetchCompaniesCount = async () =>
    {
        try
        {
            const count: number = await sponsorCompanyApi.getCompaniesCount();
            setTotalPages( Math.ceil( count / pageSize ) );
        }
        catch ( error )
        {
            setError( "Failed to fetch companies count" );
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchCompanies( pageNumber );
        fetchCompaniesCount();
    }, [ pageNumber ] );

    const handlePageChange = ( _: React.ChangeEvent<unknown>, value: number ) =>
    {
        setPageNumber( value );
    };

    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <>
            { sponsorCompanies.map( sponsorCompany => (
                <List key={ sponsorCompany.id } dense={ true }>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar src={ `data:image/jpeg;base64,${ sponsorCompany.pictureContent }` }>
                                { !sponsorCompany.pictureContent && <ArrowForwardIcon /> }
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemButton href={ `/sponsors/companies/${ sponsorCompany.id }` }>
                            <StyledText>{ sponsorCompany.name }, Country: { sponsorCompany.country }</StyledText>
                        </ListItemButton>
                    </ListItem>
                </List>
            ) ) }
            <StyledPagination
                pageNumber={ pageNumber }
                pageCount={ totalPages }
                onPageChange={ handlePageChange }
            />
        </>
    );
};

export default AthleteList;
