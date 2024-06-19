import React, { useEffect, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useLocation, useNavigate } from "react-router-dom";
import { CircularProgress, Grid, MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import enumApi from "@/api/enumApi";
import StyledButton from "../../../components/controls/Button";
import StyledForm from "../../../components/controls/Form";
import { UpdateSponsorIndividualProfileFormInput } from "./abstract";
import { UpdateSponsorIndividualProfileSchema } from "./schemas";
import sponsorIndividualApi from "@/api/sponsorIndividualApi";
import UploadPictureButton from "@/components/controls/UploadPictureButton";
import ProfilePicture from "@/components/controls/ProfilePicture";
import { formatDate } from "@/common/helpers";
import { SponsorIndividualDto } from "@/types/sponsorIndividual";
import pictureApi from "@/api/pictureApi";
import { GetPictureDto } from "@/types/picture";

const UpdateSponsorIndividualProfileForm: React.FC = () =>
{
    const navigate = useNavigate();
    const location = useLocation();
    const queryClient = useQueryClient();
    const user = location.state as SponsorIndividualDto;
    const profilePage = `/sponsors/${ user.id }`;

    const [ selectedCountry, setSelectedCountry ] = useState( user.country );
    const [ profilePicture, setProfilePicture ] = useState<File | GetPictureDto | null>( null );

    const countriesQuery = useQuery( { queryKey: [ 'getCountries' ], queryFn: enumApi.getCountries } );
    const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );

    const mutation = useMutation( {
        mutationFn: sponsorIndividualApi.updateSponsorIndividual,
        onSuccess: () =>
        {
            navigate( profilePage );
            queryClient.invalidateQueries( { queryKey: [ 'getSponsorIndividuals' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<UpdateSponsorIndividualProfileFormInput> = async ( data ) =>
    {
        const updatedData = { ...data, pictureId: profilePicture instanceof File ? profilePicture : undefined };
        mutation.mutate( updatedData );
    };

    const onCancelHandler = () => 
    {
        navigate( profilePage );
    };


    const onDeleteHandler = async () =>
    {
        setProfilePicture( null );
    };

    const handlePictureUpload = ( file: File ) =>
    {
        setProfilePicture( file );
    };

    const { register, handleSubmit, formState: { errors }, setValue } = useForm<UpdateSponsorIndividualProfileFormInput>( {
        resolver: yupResolver( UpdateSponsorIndividualProfileSchema ),
    } );

    useEffect( () =>
    {
        if ( user )
        {
            setValue( "id", user.id );
            setValue( "name", user.name );
            setValue( "country", user.country );
            setValue( "phoneNumber", user.phoneNumber );
            setValue( "email", user.email );
            setValue( "pictureId", profilePicture );
            setValue( "website", user.website );
            setValue( "faceBookLink", user.faceBookLink );
            setValue( "instagramLink", user.instagramLink );
            setValue( "stravaLink", user.stravaLink );
            setValue( "twitterLink", user.twitterLink );
            setValue( "lastName", user.lastName );

            if ( user.pictureId && user.pictureId !== 0 )
            {
                pictureApi.getPictureById( user.pictureId ).then( ( pictureUrl ) =>
                {
                    setProfilePicture( pictureUrl );
                } );
            }
        }
    }, [ user, setValue ] );

    return (
        <>
            { !mutation.isError && !mutation.isPending && !countriesQuery.isLoading && !sportsQuery.isLoading && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <h1>Update your profile</h1>
                    <input hidden type="date" { ...register( 'created' ) } defaultValue={ formatDate( user.created ) } />
                    <input hidden type="date" { ...register( 'birthDate' ) } defaultValue={ formatDate( user.birthDate ) } />

                    { !profilePicture
                        ? <UploadPictureButton onUpload={ handlePictureUpload } />
                        : <StyledButton onClick={ onDeleteHandler }>Remove uploaded picture</StyledButton> }

                    <Grid container spacing={ 2 } { ...register( 'pictureId' ) }>
                        <Grid item>
                            { profilePicture ? (
                                <ProfilePicture
                                    alt="Profile"
                                    src={ profilePicture instanceof File
                                        ? URL.createObjectURL( profilePicture )
                                        : `data:image/jpeg;base64,${ profilePicture.content }` }
                                />
                            ) : (
                                <ProfilePicture alt="Avatar">
                                    { user.name.slice( 0, 1 ) }{ user.lastName?.slice( 0, 1 ) }
                                </ProfilePicture>
                            ) }
                        </Grid>
                    </Grid>

                    <TextField
                        { ...register( "name" ) }
                        label="First name"
                        type="text"
                        placeholder="First name"
                        error={ !!errors.name }
                        helperText={ errors.name?.message }
                    />

                    <TextField
                        { ...register( "lastName" ) }
                        label="Last name"
                        type="text"
                        placeholder="Last name"
                        error={ !!errors.lastName }
                        helperText={ errors.lastName?.message }
                    />

                    <TextField
                        { ...register( "email" ) }
                        label="Email"
                        type="email"
                        placeholder="Enter a valid email"
                        error={ !!errors.email }
                        helperText={ errors.email?.message }
                    />

                    <TextField
                        { ...register( "phoneNumber" ) }
                        label="Phone number"
                        type="tel"
                        error={ !!errors.phoneNumber }
                        helperText={ errors.phoneNumber?.message }
                    />

                    <TextField
                        { ...register( "country" ) }
                        select
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
                        { ...register( "website" ) }
                        label="Website"
                        type="text"
                        placeholder="Enter a website associated with you"
                        error={ !!errors.website }
                        helperText={ errors.website?.message }
                    />

                    <TextField
                        { ...register( 'faceBookLink' ) }
                        label="Facebook link"
                        type="text"
                        placeholder="Link to your Facebook profile or page"
                        error={ !!errors.faceBookLink }
                        helperText={ errors.faceBookLink?.message }
                    />

                    <TextField
                        { ...register( 'instagramLink' ) }
                        label="Instagram link"
                        type="text"
                        placeholder="Link to your Instagram profile"
                        error={ !!errors.instagramLink }
                        helperText={ errors.instagramLink?.message }
                    />

                    <TextField
                        { ...register( 'stravaLink' ) }
                        label="Strava link"
                        type="text"
                        placeholder="Link to your Strava profile"
                        error={ !!errors.stravaLink }
                        helperText={ errors.stravaLink?.message }
                    />

                    <TextField
                        { ...register( 'twitterLink' ) }
                        label="Twitter link"
                        type="text"
                        placeholder="Link to your Twitter profile"
                        error={ !!errors.twitterLink }
                        helperText={ errors.twitterLink?.message }
                    />
                    <br />

                    <div className="container-buttons">
                        <StyledButton onClick={ onCancelHandler }>Cancel</StyledButton>
                        <StyledButton type="submit">Save</StyledButton>
                    </div>
                </StyledForm>
            ) }

            { mutation.isError && <h3>Error</h3> }
            { ( countriesQuery.isLoading || sportsQuery.isLoading || mutation.isPending ) && <CircularProgress /> }
        </>
    );
};

export default UpdateSponsorIndividualProfileForm;
