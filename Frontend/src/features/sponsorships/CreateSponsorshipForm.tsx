import React, { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLocation, useNavigate } from "react-router-dom";
import { Alert, CircularProgress, MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import StyledButton from "../../components/controls/Button";
import StyledForm from "../../components/controls/Form";
import { CreateSponsorshipFormInput } from "./abstract";
import enumApi from "@/api/enumApi";
import CreateSponsorshipSchema from "./schemas";
import sponsorshipApi from "@/api/sponsorshipApi";


const CreateSponsorshipForm: React.FC = () =>
{
    const location = useLocation();
    const { athlete, sponsor } = location.state || {};
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const [ selectedLevel, setSelectedLevel ] = useState( 'Monthly' );

    if ( !athlete || !sponsor )
    {
        return <div>Missing athlete or sponsor information.</div>;
    }
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateSponsorshipFormInput>( {
        resolver: yupResolver( CreateSponsorshipSchema ),
    } );

    const sponsorshipLevelsQuery = useQuery( { queryKey: [ "getLevels" ], queryFn: enumApi.getLevels } );

    // Mutations
    const mutation = useMutation( {
        mutationFn: sponsorshipApi.createSponsorship,
        onSuccess: () =>
        {
            <Alert severity='success' variant='filled'>Successful! Thank you!</Alert>;
            navigate( `/feed` );
            queryClient.invalidateQueries( { queryKey: [ 'createSponsorship' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<CreateSponsorshipFormInput> = async ( data ) => mutation.mutate( data );

    return (
        <>
            { !mutation.isError && !mutation.isPending && !sponsorshipLevelsQuery.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>Want to become a sposor?</h1>

                    <input hidden { ...register( "athleteId" ) } defaultValue={ athlete.id } />
                    <input hidden { ...register( 'sponsorId' ) } defaultValue={ sponsor.id } />

                    <TextField
                        { ...register( 'level' ) }
                        select
                        label='Select sponsorship level'
                        value={ selectedLevel }
                        onChange={ ( event ) => setSelectedLevel( event.target.value ) }
                    >
                        { sponsorshipLevelsQuery.data?.map( ( level ) => (
                            <MenuItem key={ level } value={ level }>
                                { level }
                            </MenuItem>
                        ) ) }
                    </TextField>

                    <TextField
                        { ...register( "amount" ) }
                        type='number'
                        label="Enter amount in EUR"
                        error={ !!errors.amount }
                        helperText={ "The amount of the sponsorship" }
                    />

                    <br />
                    <StyledButton type="submit">Submit</StyledButton>

                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( sponsorshipLevelsQuery.isPending || mutation.isPending ) && <CircularProgress /> }
        </>
    );
};

export default CreateSponsorshipForm;
