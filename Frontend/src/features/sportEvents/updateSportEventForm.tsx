import React, { useEffect, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLocation, useNavigate } from "react-router-dom";
import { Alert, Box, CircularProgress, MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import StyledButton from "../../components/controls/Button";
import StyledForm from "../../components/controls/Form";
import { UpdateSportEventFormInput } from "./abstract";
import enumApi from "@/api/enumApi";
import { UpdateSportEventSchema } from "./schemas";
import sportEventApi from "@/api/sportEventApi";
import { SportEventDto } from "@/types/sportEvent";

const UpdateSportEventForm: React.FC = () =>
{
    const navigate = useNavigate();
    const location = useLocation();
    const queryClient = useQueryClient();
    const sportEvent = location.state as SportEventDto;

    const validationSchema = UpdateSportEventSchema;

    const {
        register,
        handleSubmit,
        formState: { errors },
        setValue,
    } = useForm<UpdateSportEventFormInput>( {
        resolver: yupResolver( validationSchema ),
    } );

    useEffect( () =>
    {
        if ( sportEvent )
        {
            setValue( "id", sportEvent.id );
            setValue( "name", sportEvent.name );
            setValue( "country", sportEvent.country );
            setValue( "eventDate", sportEvent.eventDate );
            setValue( "eventType", sportEvent.eventType );
            setValue( "sport", sportEvent.sport );
            setValue( "status", sportEvent.status );
        }
    }, [ sportEvent, setValue ] );

    const [ selectedCountry, setSelectedCountry ] = useState( sportEvent.country );
    const [ selectedSport, setSelectedSport ] = useState( sportEvent.sport );
    const [ selectedEventType, setSelectedEventType ] = useState( sportEvent.eventType );
    const [ selectedEventDate ] = useState( sportEvent.eventDate );

    // Queries
    const countriesQuery = useQuery( { queryKey: [ 'getCountries' ], queryFn: enumApi.getCountries } );
    const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );
    const eventTypesQuery = useQuery( { queryKey: [ 'getEventTypes' ], queryFn: enumApi.getEventTypes } );

    // Mutations
    const mutation = useMutation( {
        mutationFn: sportEventApi.updateSportEvent,
        onSuccess: () =>
        {
            <Alert severity='success' variant='filled'>
                Sport event updated successfully!
            </Alert>;
            navigate( `/dashboard` );
            queryClient.invalidateQueries( { queryKey: [ 'sportEvents' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<UpdateSportEventFormInput> = async ( data ) =>
    {
        const updatedData = { ...data, id: sportEvent.id, status: 'Approved' };
        mutation.mutate( updatedData );
    };

    const onDeleteHandler = async () =>
    {
        await sportEventApi.deleteSportEvent( sportEvent.id );
        queryClient.invalidateQueries( { queryKey: [ 'sportEvents/pending' ] } );
    };

    return (
        <>
            { !mutation.isError && !mutation.isPending && !countriesQuery.isPending && !sportsQuery.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>Update sport event</h1>

                    <TextField
                        { ...register( "name" ) }
                        label="Event name"
                        type="text"
                        placeholder="Event name"
                        error={ !!errors.name }
                        helperText={ errors.name?.message }
                    />

                    <TextField
                        { ...register( "country" ) }
                        select
                        label="Select country"
                        error={ !!errors.country }
                        helperText={ errors.country?.message }
                        value={ selectedCountry }
                        onChange={ ( event ) => setSelectedCountry( event.target.value ) }
                    >
                        { countriesQuery.data?.map( ( country ) => (
                            <MenuItem key={ country } value={ country }>
                                { country }
                            </MenuItem>
                        ) ) }
                    </TextField>

                    <TextField
                        type="text"
                        label="You can't change the date"
                        disabled
                        error={ !!errors.eventDate }
                        value={ selectedEventDate }
                    />

                    <TextField
                        { ...register( "eventType" ) }
                        select
                        label="Type of event"
                        error={ !!errors.eventType }
                        helperText={ errors.eventType?.message }
                        value={ selectedEventType }
                        onChange={ ( event ) => setSelectedEventType( event.target.value ) }
                    >
                        { eventTypesQuery.data?.map( ( eventType ) => (
                            <MenuItem key={ eventType } value={ eventType }>
                                { eventType }
                            </MenuItem>
                        ) ) }
                    </TextField>

                    <TextField
                        { ...register( "sport" ) }
                        select
                        label="Select sport"
                        error={ !!errors.sport }
                        helperText={ errors.sport?.message }
                        value={ selectedSport }
                        onChange={ ( event ) => setSelectedSport( event.target.value ) }
                    >
                        { sportsQuery.data?.map( ( sport ) => (
                            <MenuItem key={ sport } value={ sport }>
                                { sport }
                            </MenuItem>
                        ) ) }
                    </TextField>
                    <br />
                    <Box className='container-buttons'>
                        <StyledButton onClick={ onDeleteHandler }>Delete</StyledButton>
                        <StyledButton type="submit" >Approve</StyledButton>
                    </Box>
                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( countriesQuery.isPending || sportsQuery.isPending || mutation.isPending || eventTypesQuery.isPending ) && <CircularProgress /> }
        </>
    );
};

export default UpdateSportEventForm;