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
import { Alert, CircularProgress, Typography } from '@mui/material';
import achievementApi from '@/api/achievementApi';
import { urlBuilder } from '@/common/helpers';
import { useNavigate } from 'react-router-dom';
import { useQueryClient } from '@tanstack/react-query';

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

    const onDeleteHandler = async () => 
    {
        if ( sportEvent )
        {
            await achievementApi.deleteAchievement( sportEvent.id, Number( id ) );
            onClose();
            queryClient.invalidateQueries( { queryKey: [ `deleteAchievement` ] } );
            navigate( urlBuilder(id!, role!, userType!) );
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
        >
            <DialogTitle id="scroll-dialog-title">{ athlete?.name }'s achievement from { new Date( sportEvent!.eventDate ).toLocaleDateString() }:</DialogTitle>
            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >
                    <Typography>Sport of the event: {sportEvent?.sport}</Typography>
                    <Typography>{sportEvent?.name} in {sportEvent?.country}</Typography>
                    { achievement.placeFinished && <Typography>Finished: { achievement.placeFinished } place</Typography> }
                    
                    { achievement.description && <Typography>Description: {achievement.description}</Typography> }
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <div className='container-buttons'>
                    <StyledButton onClick={ onClose }>Close</StyledButton>
                    { Number( id ) === achievement.athleteId && <StyledButton onClick={ onDeleteHandler }>Delete achievement</StyledButton> }
                </div>
            </DialogActions>
        </Dialog>
    );
};

export default AchievementDetail;
