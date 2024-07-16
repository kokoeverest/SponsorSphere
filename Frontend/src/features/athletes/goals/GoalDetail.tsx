import * as React from 'react';
import Dialog, { DialogProps } from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import AuthContext from '@/context/AuthContext';
import { useContext, useEffect, useRef, useState } from 'react';
import { GoalDto as GoalDto } from '@/types/goal';
import StyledButton from '@/components/controls/Button';
import { AthleteDto } from '@/types/athlete';
import sportEventApi from '@/api/sportEventApi';
import { SportEventDto } from '@/types/sportEvent';
import { Alert, Box, CircularProgress, Divider, Slider } from '@mui/material';
import goalApi from '@/api/goalApi';
import { useNavigate } from 'react-router-dom';
import { urlBuilder } from '@/common/helpers';
import StyledText from '@/components/controls/Typography';
import { useQueryClient } from '@tanstack/react-query';

interface GoalDetailProps
{
    goal: GoalDto | null;
    athlete: AthleteDto | null;
    open: boolean;
    onClose: () => void;
}

const GoalDetail: React.FC<GoalDetailProps> = ( {
    goal: goal, athlete: athlete, open, onClose } ) =>
{
    const { id, role, userType } = useContext( AuthContext );
    const [ scroll ] = useState<DialogProps[ 'scroll' ]>( 'paper' );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ sportEvent, setSportEvent ] = useState<SportEventDto | null>( null );
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    let totalSponsorshipsAmount = 0;
    athlete?.sponsorships.forEach( sponsorship => totalSponsorshipsAmount += sponsorship.amount );

    let notEnoughMoney = 0;
    notEnoughMoney = goal?.amountNeeded ? ( goal.amountNeeded - totalSponsorshipsAmount ) : ( - totalSponsorshipsAmount );

    const marks = [
        {
            value: 0,
            label: '0',
        },
        {
            value: goal?.amountNeeded ? goal.amountNeeded : 100,
            label: `${ goal?.amountNeeded }`,
        },
    ];

    function valuetext ( value: number )
    {
        return `${ value }`;
    }

    const descriptionElementRef = useRef<HTMLElement>( null );

    const fetchSportEvent = async ( sportEventId: number ) =>
    {
        setLoading( true );
        setError( null );
        try
        {
            const fetchedSportEvent = await sportEventApi.getSportEventById( sportEventId );
            setSportEvent( fetchedSportEvent );
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
        if ( goal )
        {
            fetchSportEvent( goal.sportEventId );
        }
    }, [ goal ] );

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
            await goalApi.deleteGoal( sportEvent.id, Number( id ) );
            onClose();
            navigate( urlBuilder( id!, role!, userType! ) );
            queryClient.invalidateQueries( { queryKey: [ 'deleteGoal' ] } );
        }
    };

    if ( !goal ) return null;
    if ( !sportEvent ) return null;
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
            <DialogTitle
                id="scroll-dialog-title"
            >
                <StyledText>
                    { athlete?.name } dreams to impress the world on <br />
                    { new Date( sportEvent!.eventDate ).toLocaleDateString() }:
                </StyledText>
            </DialogTitle>

            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >
                    <StyledText variant='h6'>{ sportEvent?.name } in { sportEvent?.country }</StyledText>
                    <Divider />
                    <StyledText variant='h6'>Sport of the event: { sportEvent?.sport }</StyledText>
                    <Divider />
                    <StyledText variant='h6'>So far { athlete?.name } has <StyledText variant='h5' sx={ { color: 'green', display: 'inline' } }>{ totalSponsorshipsAmount } </StyledText> euro </StyledText>
                    <Divider />
                    { notEnoughMoney > 0
                        ? <StyledText variant='h6'>{ athlete?.name } needs <StyledText variant='h5' sx={ { color: 'red', display: 'inline' } }>{ notEnoughMoney }</StyledText> euro more to be able to participate!</StyledText>
                        : <StyledText variant='h6'> { athlete?.name } has received the needed money for this goal!</StyledText>
                    }

                    <Box sx={
                        {
                            marginTop: 6,
                            border: '2px solid var(--backGroundOrange)',
                            borderRadius: '10px',
                            maxWidth: '100%',
                            p: 2
                        }
                    }
                    >
                        <Slider
                            min={ 0 }
                            max={ goal.amountNeeded }
                            defaultValue={ totalSponsorshipsAmount }
                            aria-label="Always visible"
                            getAriaValueText={ valuetext }
                            valueLabelDisplay="on"
                            marks={ marks }
                            sx={ { color: notEnoughMoney > 0 ? 'red' : 'green', maxWidth: '95%' } }
                        />
                    </Box>
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <div className='container-buttons'>
                    <StyledButton onClick={ onClose }>Close</StyledButton>
                    { Number( id ) === goal.athleteId
                        && <StyledButton onClick={ onDeleteHandler }>Delete goal</StyledButton> }
                </div>
            </DialogActions>
        </Dialog>
    );
};

export default GoalDetail;
