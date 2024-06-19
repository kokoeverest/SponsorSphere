import React, { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useNavigate } from "react-router-dom";
import { Alert, CircularProgress, MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

import StyledButton from "../../../components/controls/Button";
import StyledForm from "../../../components/controls/Form";
import { RegisterAthleteFormInput } from "./abstract";
import athleteApi from "@/api/athleteApi";
import enumApi from "@/api/enumApi";
import { registerAthleteSchema } from "@/features/athletes/schemas";

const RegisterAthleteForm: React.FC = () =>
{
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterAthleteFormInput>( {
    resolver: yupResolver( registerAthleteSchema ),
  } );

  const [ selectedCountry, setSelectedCountry ] = useState( 'BGR' );
  const [ selectedSport, setSelectedSport ] = useState( 'Basketball' );
  const [ errorMessage, setErrorMessage ] = useState<string | null>( null );

  // Queries
  const countriesQuery = useQuery( { queryKey: [ 'getCountries' ], queryFn: enumApi.getCountries } );
  const sportsQuery = useQuery( { queryKey: [ 'getSports' ], queryFn: enumApi.getSports } );

  // Mutations
  const mutation = useMutation( {
    mutationFn: athleteApi.registerAthlete,
    onSuccess: ( userId ) =>
    {
      <Alert severity='success' variant='filled'>You registered successfully!</Alert>;
      navigate( `/athletes/${ userId }` );
      queryClient.invalidateQueries( { queryKey: [ 'getAthletes' ] } );
    },
    onError: ( error: any ) =>
    {
      if ( error.response && error.response.data && error.response.data.message )
      {
        if ( error.response.data.message.includes( "is already taken" ) )
        {
          setErrorMessage( "This email is already registered. Please use another email." );
        } else
        {
          setErrorMessage( "An unexpected error occurred. Please try again." );
        }
      }
    }
  } );

  const onSubmitHandler: SubmitHandler<RegisterAthleteFormInput> = async ( data ) =>
  {
    setErrorMessage( null );
    mutation.mutate( data );
  };

  return (
    <>
      { !countriesQuery.isPending && !sportsQuery.isPending && (
        <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
          <h1>Register as Athlete</h1>


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
            { ...register( "password" ) }
            label="Password"
            type="password"
            placeholder="Enter strong password"
            error={ !!errors.password }
            helperText={ errors.password?.message }
          />

          <TextField
            { ...register( "birthDate" ) }
            type="date"
            error={ !!errors.birthDate }
            helperText={ errors.birthDate?.message }
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

          { errorMessage && (

            <Alert
              severity="error"
              sx={ {
                position: 'fixed',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                zIndex: 100
              } }>{ errorMessage }</Alert>

          ) }

          <StyledButton type="submit">Register</StyledButton>
        </StyledForm>
      ) }

      { ( countriesQuery.isPending || sportsQuery.isPending || mutation.isPending ) && <CircularProgress /> }
    </>
  );
};

export default RegisterAthleteForm;
