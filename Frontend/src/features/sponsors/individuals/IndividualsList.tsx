import React, { useEffect, useState } from 'react';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import { List, ListItem, CircularProgress, Alert } from '@mui/material';
import { PAGE_SIZE as pageSize } from '@/common/constants';
import { SponsorIndividualDto } from '../../../types/sponsorIndividual';
import StyledPagination from '@/components/controls/Pagination';
import sponsorIndividualApi from '@/api/sponsorIndividualApi';
import { fetchPicture } from '@/common/helpers';
import StyledText from '@/components/controls/Typography';

interface SponsorIndividualWithPicture extends SponsorIndividualDto
{
    pictureContent?: string;
}

const AthleteList: React.FC = () =>
{
    const [ sponsorIndividuals, setSponsorIndividuals ] = useState<SponsorIndividualWithPicture[]>( [] );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ pageNumber, setPageNumber ] = useState<number>( 1 );
    const [ totalPages, setTotalPages ] = useState<number>( 1 );

    const fetchIndividuals = async ( page: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const queryParams = `?pageNumber=${ page }&pageSize=${ pageSize }&sport=Football`;
            const result: SponsorIndividualDto[] = await sponsorIndividualApi.getSponsorIndividuals( queryParams );
            
            const sponsorIndividualsWithPictures = await Promise.all( result.map( async ( sponsorIndividual ) =>
            {
                if ( sponsorIndividual.pictureId )
                {
                    const pictureContent = await fetchPicture( sponsorIndividual.pictureId );
                    return { ...sponsorIndividual, pictureContent };
                }
                return sponsorIndividual;
            } ) );


            setSponsorIndividuals( sponsorIndividualsWithPictures );
        } 
        catch ( error )
        {
            setError( "Failed to fetch sponsors" );
            console.error( error );
        } 
        finally
        {
            setLoading( false );
        }
    };

    const fetchIndividualsCount = async () =>
    {
        try
        {
            const count: number = await sponsorIndividualApi.getIndividualsCount();
            setTotalPages( Math.ceil( count / pageSize ) );
        } 
        catch ( error )
        {
            setError( "Failed to fetch sponsors count" );
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchIndividuals( pageNumber );
        fetchIndividualsCount();
    }, [ pageNumber ] );

    const handlePageChange = ( _: React.ChangeEvent<unknown>, value: number ) =>
    {
        setPageNumber( value );
    };

    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <>
            { sponsorIndividuals.map( sponsorIndividual => (
                <List key={ sponsorIndividual.id } dense={ true }>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar src={ `data:image/jpeg;base64,${ sponsorIndividual.pictureContent }` }>
                                { !sponsorIndividual.pictureContent && <ArrowForwardIcon /> }
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemButton href={ `/sponsors/individuals/${ sponsorIndividual.id }` }>
                            <StyledText>{ sponsorIndividual.name }, Age: { sponsorIndividual.age }, Country: { sponsorIndividual.country }</StyledText>
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
