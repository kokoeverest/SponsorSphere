import React, { useEffect, useState } from 'react';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import { List, ListItem, CircularProgress, Alert } from '@mui/material';
import { PAGE_SIZE as pageSize } from '@/common/constants';
import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';
import StyledPagination from '@/components/controls/Pagination';

const AthleteList: React.FC = () =>
{
    const [ athletes, setAthletes ] = useState<AthleteDto[]>( [] );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ pageNumber, setPageNumber ] = useState<number>( 1 );
    const [ totalPages, setTotalPages ] = useState<number>( 1 );

    const fetchAthletes = async ( page: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const queryParams = `?pageNumber=${ page }&pageSize=${ pageSize }&sport=Football`;
            const result: AthleteDto[] = await athleteApi.getAthletes( queryParams );
            setAthletes( result );
        } catch ( error )
        {
            setError( "Failed to fetch athletes" );
            console.error( error );
        } finally
        {
            setLoading( false );
        }
    };

    const fetchAthleteCount = async () =>
    {
        try
        {
            const count: number = await athleteApi.getAthletesCount();
            setTotalPages( Math.ceil( count / pageSize ) );
        } catch ( error )
        {
            setError( "Failed to fetch athlete count" );
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchAthletes( pageNumber );
        fetchAthleteCount();
    }, [ pageNumber ] );

    const handlePageChange = ( _: React.ChangeEvent<unknown>, value: number ) =>
    {
        setPageNumber( value );
    };

    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <>
            { athletes.map( athlete => (
                <List key={ athlete.id } dense={ true }>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar>
                                <ArrowForwardIcon />
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemButton href={ `/athletes/${ athlete.id }` }>
                            { athlete.name } { athlete.lastName }, Age: { athlete.age }, Sport: { athlete.sport }, Country: { athlete.country }
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
