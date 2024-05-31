import React, { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLocation, useNavigate } from "react-router-dom";
import { MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import StyledButton from "../../components/controls/Button";
import StyledForm from "../../components/controls/Form";
import { CreateSportEventFormInput } from "./abstract";
import enumApi from "@/api/enumApi";
import { CreatePastSportEventSchema, CreateFutureSportEventSchema } from "./schemas";
import sportEventApi from "@/api/sportEventApi";

const CreateSportEventForm: React.FC = () =>
{
    const navigate = useNavigate();
    const location = useLocation();
    const queryClient = useQueryClient();

    // Extract query parameter
    const searchParams = new URLSearchParams( location.search );
    const eventType = searchParams.get( 'eventType' );

    // Determine which schema to use based on the query parameter
    const isPastEvent = eventType === 'achievement';
    const validationSchema = isPastEvent ? CreatePastSportEventSchema : CreateFutureSportEventSchema;

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateSportEventFormInput>( {
        resolver: yupResolver( validationSchema ),
    } );

    const [ selectedCountry, setSelectedCountry ] = useState( 'BGR' );
    const [ selectedSport, setSelectedSport ] = useState( 'Football' );
    const [ selectedEventType, setSelectedEventType] = useState('Race');

    // Queries
    const countriesQuery = useQuery( { queryKey: [ 'getCountries' ], queryFn: enumApi.getCountries } );
    const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );
    const eventTypesQuery = useQuery( { queryKey: [ 'getEventTypes' ], queryFn: enumApi.getEventTypes } );

    // Mutations
    const mutation = useMutation( {
        mutationFn: sportEventApi.createSportEvent,
        onSuccess: () =>
        {
            alert( "Thank you! The event you created needs to be confirmed by admin and you'll receive a notification shortly" );
            navigate( `/dashboard` );
            // TODO: Invalidate and refetch
            queryClient.invalidateQueries( { queryKey: [ 'createSportEvent' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<CreateSportEventFormInput> = async ( data ) => mutation.mutate( data );

    return (
        <>
            { !mutation.isError && !mutation.isPending && !countriesQuery.isPending && !sportsQuery.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>{ isPastEvent ? 'Add a past sport event' : 'Add a future sport event' }</h1>

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
                        { ...register( "eventDate" ) }
                        type="date"
                        error={ !!errors.eventDate }
                        helperText={ "Please enter a past date" }
                        
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

                    <StyledButton type="submit">Submit</StyledButton>
                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( countriesQuery.isPending || sportsQuery.isPending || mutation.isPending || eventTypesQuery.isPending ) && <h3>Loading Spinner...</h3> }
        </>
    );
};

export default CreateSportEventForm;
