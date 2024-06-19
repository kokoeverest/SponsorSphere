import React, { useEffect, useState } from 'react';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ListItemButton from '@mui/material/ListItemButton';
import { List, ListItem, CircularProgress, Alert } from '@mui/material';
import { PAGE_SIZE as pageSize } from '@/common/constants';
import athleteApi from '@/api/athleteApi';
import { fetchPicture } from '@/common/helpers'; // Assuming you have a pictureApi to fetch pictures
import { AthleteDto } from '../../types/athlete';
import StyledPagination from '@/components/controls/Pagination';
import StyledText from '@/components/controls/Typography';

interface AthleteWithPicture extends AthleteDto
{
    pictureContent?: string;
}

const AthleteList: React.FC = () =>
{
    const [ athletes, setAthletes ] = useState<AthleteWithPicture[]>( [] );
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
            const queryParams = `?pageNumber=${ page }&pageSize=${ pageSize }`;
            const result: AthleteDto[] = await athleteApi.getAthletes( queryParams );

            // Fetch pictures for athletes with pictureId
            const athletesWithPictures = await Promise.all( result.map( async ( athlete ) =>
            {
                if ( athlete.pictureId )
                {
                    const pictureContent = await fetchPicture( athlete.pictureId );
                    return { ...athlete, pictureContent };
                }
                return athlete;
            } ) );

            setAthletes( athletesWithPictures );
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
    if ( error ) return <Alert severity='error' variant='filled'>{ error }</Alert>;

    return (
        <>
            { athletes.map( athlete => (
                <List key={ athlete.id } dense={ true }>
                    <ListItem>
                        <ListItemAvatar>
                            <Avatar src={ `data:image/jpeg;base64,${ athlete.pictureContent }` }>
                                { !athlete.pictureContent && <ArrowForwardIcon /> }
                            </Avatar>
                        </ListItemAvatar>
                        <ListItemButton href={ `/athletes/${ athlete.id }` }>
                            <StyledText>{ athlete.name } { athlete.lastName }, Age: { athlete.age }, Sport: { athlete.sport }, Country: { athlete.country }</StyledText>
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
