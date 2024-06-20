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
import { Alert, CircularProgress, Slider, Typography } from '@mui/material';
import StyledBox from '@/components/controls/Box';
import goalApi from '@/api/goalApi';
import { useNavigate } from 'react-router-dom';
import { urlBuilder } from '@/common/helpers';

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
            <DialogTitle id="scroll-dialog-title">{ athlete?.name } dreams to impress the world on <br />
                { new Date( sportEvent!.eventDate ).toLocaleDateString() }:</DialogTitle>
            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >
                    <Typography>Sport of the event: { sportEvent?.sport }</Typography>
                    <Typography>{ sportEvent?.name } in { sportEvent?.country }</Typography>
                    <Typography>So far { athlete?.name } has { totalSponsorshipsAmount } euro </Typography>

                    { notEnoughMoney > 0
                        ? <Typography>{ athlete?.name } needs { notEnoughMoney } euro more to be able to participate!</Typography>
                        : <Typography> { athlete?.name } has received the needed money for this goal!</Typography> }

                    <StyledBox sx={ { marginTop: 6 } }>
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
                    </StyledBox>

                    <Typography>Current sponsors: { }</Typography>
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
