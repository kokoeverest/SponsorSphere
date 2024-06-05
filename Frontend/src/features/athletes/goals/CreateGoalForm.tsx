import React, { useEffect, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { Link, useNavigate } from "react-router-dom";
import { Alert, CircularProgress, MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { PAGE_SIZE as pageSize } from "@/common/constants";
import StyledButton from "../../../components/controls/Button";
import StyledForm from "../../../components/controls/Form";
import { CreateGoalFormInput } from "./abstract";
import goalApi from "@/api/goalApi";
import sportEventApi from "@/api/sportEventApi";
import enumApi from "@/api/enumApi";
import { SportEventDto } from "@/types/sportEvent";
import CreateGoalSchema from "./schemas";

const CreateGoalForm: React.FC = () =>
{
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateGoalFormInput>( {
        resolver: yupResolver( CreateGoalSchema ),
    } );

    const [ selectedSportEventId, setSelectedSportEventId ] = useState( '' );
    const [ selectedSport, setSelectedSport ] = useState( 'Football' );
    const [ queryParams, setQueryParams ] = useState( '?sport=Football' );

    // TODO: Pagination
    useEffect( () =>
    {
        setQueryParams( `?sport=${ selectedSport }&pageNumber=1&pageSize=${ pageSize }` );
    }, [ selectedSport ] );

    // Queries
    const sportsEventsQuery = useQuery(
        { queryKey: [ "getUnFinishedSportEvents", queryParams ], queryFn: () => sportEventApi.getUnFinishedSportEvents( queryParams ), } );

    const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );

    // Mutations
    const mutation = useMutation( {
        mutationFn: goalApi.createGoal,
        onSuccess: () =>
        {
            <Alert severity='success' variant='filled'>Successful! Thank you!</Alert>;
            navigate( `/dashboard` );
            queryClient.invalidateQueries( { queryKey: [ 'createGoal' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<CreateGoalFormInput> = async ( data ) => mutation.mutate( data );

    return (
        <>
            { !mutation.isError && !mutation.isPending && !sportsEventsQuery.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>Add your goal</h1>

                    <div>
                        <Link to={ '/achievements/sportEvents/create?eventType=goal' }>*If you don't find the sport event in the list,
                            click here to create it first*</Link>
                    </div>
                    <TextField
                        select
                        label="Select sport"
                        helperText={ "The sport of your achievement" }
                        value={ selectedSport }
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
                        { ...register( 'amountNeeded' ) }
                        label="Goal amount needed"
                        type='number'
                        placeholder="Goal amount needed"
                        error={ !!errors.amountNeeded }
                        helperText={ "How much do you need?" }
                    />
                    <br />
                    <StyledButton type="submit">Submit</StyledButton>
                    
                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( sportsEventsQuery.isPending || mutation.isPending ) && <CircularProgress /> }
        </>
    );
};

export default CreateGoalForm;
