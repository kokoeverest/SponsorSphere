import React, { useEffect, useState } from 'react';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import { List, ListItem, CircularProgress, Alert } from '@mui/material';
import { PAGE_SIZE as pageSize } from '@/common/constants';
import StyledPagination from '@/components/controls/Pagination';
import { SportEventDto } from '@/types/sportEvent';
import sportEventApi from '@/api/sportEventApi';
import { useNavigate } from 'react-router-dom';

const PendingSportEventsList: React.FC = () =>
{
    const [ sportEvents, setSportEvents ] = useState<SportEventDto[]>( [] );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ pageNumber, setPageNumber ] = useState<number>( 1 );
    const [ totalPages, setTotalPages ] = useState<number>( 1 );
    const navigate = useNavigate();

    const fetchSportEvents = async ( page: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const queryParams = `?pageNumber=${ page }&pageSize=${ pageSize }`;
            const result: SportEventDto[] = await sportEventApi.getPendingSportEvents( queryParams );
            setSportEvents( result );
        } catch ( error )
        {
            setError( "Failed to fetch sport events" );
            console.error( error );
        } finally
        {
            setLoading( false );
        }
    };

    const fetchSportEventsCount = async () =>
    {
        try
        {
            const count: number = await sportEventApi.getPendingSportEventsCount();
            setTotalPages( Math.ceil( count / pageSize ) );
        } catch ( error )
        {
            setError( "Failed to fetch sport events count" );
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchSportEvents( pageNumber );
        fetchSportEventsCount();
    }, [ pageNumber ] );

    const handlePageChange = ( _: React.ChangeEvent<unknown>, value: number ) =>
    {
        setPageNumber( value );
    };

    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <>
            { sportEvents.map( sportEvent => (
                <List key={ sportEvent.id } dense={ true }>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <ArrowForwardIcon />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemButton onClick={ () => navigate( `/sportEvents/update`, { state: sportEvent } ) }>
                            { sportEvent.name }, Sport: { sportEvent.sport }, Country: { sportEvent.country }
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

export default PendingSportEventsList;
