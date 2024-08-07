import * as React from 'react';
import Dialog, { DialogProps } from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import AuthContext from '@/context/AuthContext';
import { useContext, useEffect, useRef, useState } from 'react';
import { AchievementDto as AchievementDto } from '@/types/achievement';
import StyledButton from '@/components/controls/Button';
import { AthleteDto } from '@/types/athlete';
import sportEventApi from '@/api/sportEventApi';
import { SportEventDto } from '@/types/sportEvent';
import { Alert, Box, CircularProgress, Container, Divider } from '@mui/material';
import achievementApi from '@/api/achievementApi';
import { urlBuilder } from '@/common/helpers';
import { useNavigate } from 'react-router-dom';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import StyledText from '@/components/controls/Typography';

interface AchievementDetailProps
{
    achievement: AchievementDto | null;
    athlete: AthleteDto | null;
    open: boolean;
    onClose: () => void;
}

const AchievementDetail: React.FC<AchievementDetailProps> = ( {
    achievement: achievement, athlete: athlete, open, onClose } ) =>
{
    const { id, role, userType } = useContext( AuthContext );
    const [ scroll ] = useState<DialogProps[ 'scroll' ]>( 'paper' );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ sportEvent, setSportEvent ] = useState<SportEventDto | null>( null );
    const navigate = useNavigate();
    const queryClient = useQueryClient();


    const descriptionElementRef = useRef<HTMLElement>( null );

    const fetchSportEvent = async ( sportEventId: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const result = await sportEventApi.getSportEventById( sportEventId );
            setSportEvent( result );
        } catch ( error )
        {
            setError( "Failed to fetch sport events" );
            console.error( error );
        } finally
        {
            setLoading( false );
        }
    };

    useEffect( () =>
    {
        if ( achievement )
        {
            fetchSportEvent( achievement.sportEventId );
        }
    }, [ achievement ] );

    useEffect( () =>
    {
        if ( open && descriptionElementRef.current )
        {
            descriptionElementRef.current.focus();
        }
    }, [ open ] );

    const mutation = useMutation( {
        mutationFn: ( { sportEventId, userId }: { sportEventId: number, userId: number; } ) =>
            achievementApi.deleteAchievement( sportEventId, userId ),
        onSuccess: () =>
        {
            onClose();
            queryClient.invalidateQueries( { queryKey: [ `getAthletes` ] } );
            navigate( urlBuilder( id!, role!, userType! ) );
        },
    } );

    const onDeleteHandler = async ( sportEventId: number, userId: number ) => 
    {
        if ( sportEvent )
        {
            mutation.mutate( { sportEventId, userId } );
        }
    };

    if ( !achievement ) return null;
    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <Dialog
            open={ open }
            onClose={ onClose }
            scroll={ scroll }
            aria-labelledby="scroll-dialog-title"
            aria-describedby="scroll-dialog-description"
            sx={ { lineHeight: 2 } }
        >
            <DialogTitle id="scroll-dialog-title"
                sx={ {
                    width: '60rem',
                    maxWidth: '80%'
                } }
            >
                <StyledText>
                    { athlete?.name }'s achievement from { new Date( sportEvent!.eventDate ).toLocaleDateString() }:
                </StyledText>
            </DialogTitle>
            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >

                    <Divider flexItem>
                        <StyledText>{ sportEvent?.name } in { sportEvent?.country }</StyledText>
                    </Divider>

                    <Divider flexItem>
                        <StyledText>
                            Sport of the event: { sportEvent?.sport }
                        </StyledText>
                    </Divider>

                    {
                        achievement.placeFinished &&
                        <Divider flexItem>
                            <StyledText>Finished: <StyledText variant='h4' sx={ { color: 'green', display: 'inline' } }>{ achievement.placeFinished }</StyledText> place</StyledText>
                        </Divider>
                    }

                    { achievement.description &&
                        <Container>
                            <Box sx={ {
                                border: '2px solid var(--backGroundOrange)',
                                borderRadius: '10px',
                                maxWidth: '100%',
                                p: 2,
                                m: 2
                            } }>
                                <StyledText sx={ { justifySelf: 'center' } }>Description:</StyledText>

                                <StyledText variant='body1'
                                    sx={ {
                                        flexWrap: 'inherit',
                                        whiteSpace: 'pretty',
                                        wordWrap: 'break-word',
                                        width: '100%',
                                        overflow: 'hidden'
                                    } } >
                                    { achievement.description }
                                </StyledText>
                            </Box>
                        </Container>
                    }
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <div className='container-buttons'>
                    <StyledButton onClick={ onClose }>Close</StyledButton>
                    { Number( id ) === achievement.athleteId && <StyledButton onClick={ () => onDeleteHandler( sportEvent!.id, Number( id! ) ) }>Delete achievement</StyledButton> }
                </div>
            </DialogActions>
        </Dialog>
    );
};

export default AchievementDetail;
