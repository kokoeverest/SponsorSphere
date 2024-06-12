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
import { Alert, Box, CircularProgress, Slider, Typography } from '@mui/material';

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
    const { id } = useContext( AuthContext );
    const [ scroll ] = useState<DialogProps[ 'scroll' ]>( 'paper' );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ sportEvent, setSportEvent ] = useState<SportEventDto | null>( null );
    let totalSponsorshipsAmount = 0;
    athlete?.sponsorships.forEach( sponsorship => totalSponsorshipsAmount += sponsorship.amount );

    const marks = [
        {
            value: 0,
            label: '0',
        },
        {
            value: goal?.amountNeeded ? goal.amountNeeded : 100,
            label: `${goal?.amountNeeded}`,
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

    if ( !goal ) return null;
    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error } </Alert>;

    return (
        <Dialog
            open={ open }
            onClose={ onClose }
            scroll={ scroll }
            aria-labelledby="scroll-dialog-title"
            aria-describedby="scroll-dialog-description"
            sx={{lineHeight: 2}}
        >
            <DialogTitle id="scroll-dialog-title">{ athlete?.name } dreams to impress the world on <br/>
                { new Date( sportEvent!.eventDate ).toLocaleDateString() }:</DialogTitle>
            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >
                    <Typography>Sport of the event: { sportEvent?.sport }</Typography>
                    <Typography>{ sportEvent?.name } in { sportEvent?.country }</Typography>
                    { goal.amountNeeded && <Typography>{ athlete?.name } needs { goal.amountNeeded - totalSponsorshipsAmount } euro to be able to participate!</Typography> }
                    <Typography>So far { athlete?.name } has { totalSponsorshipsAmount } euro </Typography>
                    <Box sx={ { width: 'inherit', marginTop: 6 } }>
                        <Slider
                        disabled
                            min={ 0 }
                            max={ goal.amountNeeded }
                            defaultValue={ totalSponsorshipsAmount }
                            aria-label="Always visible"
                            getAriaValueText={valuetext}
                            valueLabelDisplay="on"
                            marks={marks}    
                        >

                        </Slider>
                    </Box>

                    <Typography>Current sponsors: { }</Typography>
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <div className='container-buttons'>
                    <StyledButton onClick={ onClose }>Close</StyledButton>
                    { Number( id ) === goal.athleteId && <StyledButton>Delete achievement</StyledButton> }
                </div>
            </DialogActions>
        </Dialog>
    );
};

export default GoalDetail;
