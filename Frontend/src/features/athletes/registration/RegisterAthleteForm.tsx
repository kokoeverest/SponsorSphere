import React, { useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useNavigate } from "react-router-dom";
import { MenuItem, TextField } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";

import StyledButton from "../../../components/controls/Button";
import StyledForm from "../../../components/controls/Form";
import { RegisterAthleteFormInput } from "./abstract";
import athleteApi from "@/api/athleteApi";
import enumApi from "@/api/enumApi";
import { registerAthleteSchema } from "@/features/athletes/schemas";

const RegisterAthleteForm: React.FC = () => {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterAthleteFormInput>({
    resolver: yupResolver(registerAthleteSchema),
  });

  const [selectedCountry, setSelectedCountry] = useState('ABW');
  const [selectedSport, setSelectedSport] = useState('Basketball');

  // Queries
  const countriesQuery = useQuery({ queryKey: ['getCountries'], queryFn: enumApi.getCountries });
  const sportsQuery = useQuery({ queryKey: ['getSports'], queryFn: enumApi.getSports });

  // Mutations
  const mutation = useMutation({
    mutationFn: athleteApi.register,
    onSuccess: (userId) => {
      alert("You registered successfully!");
      navigate(`/athletes/${userId}`);
      // TODO: Invalidate and refetch
      //queryClient.invalidateQueries({ queryKey: ['getAthletesQuery'] })
    },
  });

  const onSubmitHandler: SubmitHandler<RegisterAthleteFormInput> = async (data) => mutation.mutate(data);

  return (
    <>
      {!mutation.isError && !mutation.isPending && !countriesQuery.isPending && !sportsQuery.isPending && (
        <StyledForm onSubmit={handleSubmit(onSubmitHandler)}>
          <h1>Register as Athlete</h1>

          <TextField
            {...register("name")}
            label="First name"
            type="text"
            placeholder="First name"
            error={!!errors.name}
            helperText={errors.name?.message}
          />

          <TextField
            {...register("lastName")}
            label="Last name"
            type="text"
            placeholder="Last name"
            error={!!errors.lastName}
            helperText={errors.lastName?.message}
          />

          <TextField
            {...register("email")}
            label="Email"
            type="email"
            placeholder="Enter a valid email"
            error={!!errors.email}
            helperText={errors.email?.message}
          />

          <TextField
            {...register("password")}
            label="Password"
            type="password"
            placeholder="Enter strong password"
            error={!!errors.password}
            helperText={errors.password?.message}
          />

          <TextField
            {...register("birthDate")}
            type="date"
            error={!!errors.birthDate}
            helperText={errors.birthDate?.message}
          />

          <TextField
            {...register("phoneNumber")}
            label="Phone number"
            type="tel"
            error={!!errors.phoneNumber}
            helperText={errors.phoneNumber?.message}
          />

          <TextField
            {...register("country")}
            select
            label="Select country"
            error={!!errors.country}
            helperText={errors.country?.message}
            value={selectedCountry}
            onChange={(event) => setSelectedCountry(event.target.value)}
          >
            {countriesQuery.data?.map((country) => (
              <MenuItem key={country} value={country}>
                {country}
              </MenuItem>
            ))}
          </TextField>

          <TextField
            {...register("sport")}
            select
            label="Select sport"
            error={!!errors.sport}
            helperText={errors.sport?.message}
            value={selectedSport}
            onChange={(event) => setSelectedSport(event.target.value)}
          >
            {sportsQuery.data?.map((sport) => (
              <MenuItem key={sport} value={sport}>
                {sport}
              </MenuItem>
            ))}
          </TextField>
          <br />

          <StyledButton type="submit">Register</StyledButton>
        </StyledForm>
      )}

      {mutation.isError && <h3>Error</h3>}
      {(countriesQuery.isPending || sportsQuery.isPending || mutation.isPending) && <h3>Loading Spinner...</h3>}
    </>
  );
};

export default RegisterAthleteForm;
