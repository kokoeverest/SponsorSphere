import React, { useState } from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useNavigate } from 'react-router-dom';
import { MenuItem, TextField } from '@mui/material';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';

import { RegisterIndividualSchema } from '../schemas';
import { RegisterIndividualFormInput } from './abstract';
import StyledButton from '../../../../components/controls/Button';
import StyledForm from '../../../../components/controls/Form';
import enumApi from '@/api/enumApi';
import sponsorIndividualApi from '@/api/sponsorIndividualApi';


const RegisterIndividualForm: React.FC = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<RegisterIndividualFormInput>({
        resolver: yupResolver(RegisterIndividualSchema),
    });

    const [selectedCountry, setSelectedCountry] = useState('BGR');

    const countriesQuery = useQuery({ queryKey: ['getCountries'], queryFn: enumApi.getCountries });

    // Mutations
    const mutation = useMutation({
        mutationFn: sponsorIndividualApi.register,
        onSuccess: (userId) => {
            alert("You registered successfully!");
            navigate(`/sponsors/individuals/${userId}`);
            // TODO: Invalidate and refetch
            queryClient.invalidateQueries({ queryKey: ['getSponsorIndividuals'] });
        },
    });

    const onSubmitHandler: SubmitHandler<RegisterIndividualFormInput> = async (data) => mutation.mutate(data);

    return (
        <StyledForm onSubmit={ handleSubmit(onSubmitHandler) }>
            <h1>Register as Sponsor</h1>

            <TextField
                required
                { ...register('name') }
                label='First name'
                type="text"
                placeholder="First name"
                error={ !!errors.name }
                helperText={ errors.name?.message }
            />

            <TextField
                required
                { ...register('lastName') }
                label='Last name'
                type="text"
                placeholder="Last name"
                error={ !!errors.lastName }
                helperText={ errors.lastName?.message }
            />

            <TextField
                required
                { ...register('email') }
                label='Email'
                type="email"
                placeholder="Enter a valid email"
                error={ !!errors.email }
                helperText={ errors.email?.message }
            />

            <TextField
                required
                { ...register('password') }
                label='Password'
                type="password"
                placeholder="Enter strong password"
                error={ !!errors.password }
                helperText={ errors.password?.message }
            />

            <TextField
                required
                { ...register('birthDate') }
                type="date"
                error={ !!errors.birthDate }
                helperText={ errors.birthDate?.message }
            />

            <TextField
                required
                { ...register('phoneNumber') }
                label='Phone number'
                type="tel"
                error={ !!errors.phoneNumber }
                helperText={ errors.phoneNumber?.message }
            />

            <TextField
                required
                { ...register('country') }
                select
                label="Select country"
                value={ selectedCountry }
                onChange={ (event) => setSelectedCountry(event.target.value) }
                error={ !!errors.country }
                helperText={ errors.country?.message }
            >
                { countriesQuery.data?.map((country) => (
                    <MenuItem key={ country } value={ country }>{ country }</MenuItem>
                )) }
            </TextField>

            <br />

            <StyledButton type='submit'>Register</StyledButton>
        </StyledForm>
    );
};

export default RegisterIndividualForm;
