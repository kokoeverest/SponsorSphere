import React, { useEffect, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { Link, useNavigate } from "react-router-dom";
import { MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import StyledButton from "../../../components/controls/Button";
import StyledForm from "../../../components/controls/Form";
import { CreateAchievementFormInput } from "./abstract";
import achievementApi from "@/api/achievementApi";
import sportEventApi from "@/api/sportEventApi";
import enumApi from "@/api/enumApi";
import CreateAchievementSchema from "./schemas";
import { SportEventDto } from "@/types/sportEvent";
import CreateSportEvent from "@/features/sportEvents/CreateSportEvent";

const CreateAchievementForm: React.FC = () =>
{
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateAchievementFormInput>( {
        resolver: yupResolver( CreateAchievementSchema ),
    } );

    const [ selectedSportEventId, setSelectedSportEventId ] = useState( '' );
    const [ selectedSport, setSelectedSport ] = useState( '' );
    const [ queryParams, setQueryParams ] = useState( '?sport=Football' );

    useEffect( () =>
    {
        setQueryParams( `?sport=${ selectedSport }` );
    }, [ selectedSport ] );

    // Queries
    const sportsEventsQuery = useQuery(
        { queryKey: [ "getFinishedSportEvents", queryParams ], queryFn: () => sportEventApi.getFinishedSportEvents( queryParams ), } );

    const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );

    // Mutations
    const mutation = useMutation( {
        mutationFn: achievementApi.createAchievement,
        onSuccess: () =>
        {
            alert( "Successful! Thank you!" );
            navigate( `/dashboard` );
            // TODO: Invalidate and refetch
            queryClient.invalidateQueries( { queryKey: [ 'createAchievement' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<CreateAchievementFormInput> = async ( data ) => mutation.mutate( data );

    return (
        <>
            { !mutation.isError && !mutation.isPending && !sportsEventsQuery.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>Add your achievement</h1>

                    <TextField
                        select
                        label="Select sport"
                        helperText={ "The sport of your achievement" }
                        value={ selectedSport + "&pageNumber=1&pageSize=10" }
                        onChange={ ( event ) => setSelectedSport( event.target.value ) }
                    >
                        { sportsQuery.data?.map( ( sport ) => (
                            <MenuItem key={ sport } value={ sport }>
                                { sport }
                            </MenuItem>
                        ) ) }
                    </TextField>

                    <TextField
                        { ...register( "sportEventId" ) }
                        select
                        label="Select sport event"
                        error={ !!errors.sportEventId }
                        helperText={ errors.sportEventId?.message }
                        value={ selectedSportEventId }
                        onChange={ ( event ) => setSelectedSportEventId( event.target.value ) }
                    >
                        { sportsEventsQuery.data?.map( ( sportEvent: SportEventDto ) => (
                            <MenuItem key={ sportEvent.id } value={ sportEvent.id }>
                                { sportEvent.name }
                            </MenuItem>
                        ) ) }
                    </TextField>

                    <TextField
                        { ...register( 'placeFinished' ) }
                        label="Place finished"
                        type="text"
                        placeholder="Place finished"
                        error={ !!errors.placeFinished }
                        helperText={ "If your event was a race" }
                    />

                    <TextField
                        { ...register( 'description' ) }
                        label="Description"
                        type="text"
                        placeholder="Description"
                        error={ !!errors.description }
                        helperText={ "Describe your achievement" }
                    />
                    <br />
                    <StyledButton type="submit">Submit</StyledButton>
                    <br />
            <div>
                <Link to={'/achievements/sportEvents/create'}>*If you don't find the sport event in the list,
                        click here to create it first*</Link>
            </div>
                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( sportsEventsQuery.isPending || mutation.isPending ) && <h3>Loading Spinner...</h3> }
        </>
    );
};

export default CreateAchievementForm;
