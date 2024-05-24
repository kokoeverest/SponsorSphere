import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { MenuItem, TextField } from '@mui/material';
import { useMutation } from '@tanstack/react-query';

import StyledButton from '../../../components/controls/Button';
import StyledForm from '../../../components/controls/Form';
import { RegisterAthleteFormInput } from './abstract';
import athleteApi from '@/api/athleteApi';

const registerAthleteSchema = yup.object().shape({
    name: yup.string().min(2).max(200).required('First name is required'),
    lastName: yup.string().min(2).max(200).required('Last name is required'),
    email: yup.string().email('Must be a valid email').required('Email is required'),
    password: yup.string().min(8).max(32).required('Password is required'),
    birthDate: yup.string().required('Birthdate is required').typeError('Invalid date format'),
    phoneNumber: yup.string().required('Phone number is required'),
    country: yup.string().required('Country is required'),
    sport: yup.string().required('Sport is required'),
});

const RegisterAthleteForm: React.FC = () => {
    const { register, handleSubmit, formState: { errors } } = useForm<RegisterAthleteFormInput>({
        resolver: yupResolver(registerAthleteSchema),
    });

    const [countries, setCountries] = useState<string[]>([]);
    const [sports, setSports] = useState<string[]>([]);

    const navigate = useNavigate();

    // Mutations
    const mutation = useMutation({
        mutationFn: athleteApi.register,
        onSuccess: (userId) => {
            alert("You registered successfully!");
            navigate(`/athletes/${userId}`);
        }
    });

    useEffect(() => {
        const fetchEnums = async () => {
            try {
                const countriesResponse = await axios.get('https://localhost:7026/enums/countries');
                const sportsResponse = await axios.get('https://localhost:7026/enums/sports');
                setCountries(countriesResponse.data);
                setSports(sportsResponse.data);
            } catch (error) {
                console.error('Failed to fetch enum values', error);
            }
        };

        fetchEnums();
    }, []);

    const onSubmitHandler: SubmitHandler<RegisterAthleteFormInput> = async (data) => {
        mutation.mutate(data);
    };

    return (
        <StyledForm onSubmit={ handleSubmit(onSubmitHandler) }>
            <h1>Register as Athlete</h1>

            <TextField
                { ...register('name') }
                label='First name'
                type="text"
                placeholder="First name"
                error={ !!errors.name }
                helperText={ errors.name?.message }
            />

            <TextField
                { ...register('lastName') }
                label='Last name'
                type="text"
                placeholder="Last name"
                error={ !!errors.lastName }
                helperText={ errors.lastName?.message }
            />

            <TextField
                { ...register('email') }
                label='Email'
                type="email"
                placeholder="Enter a valid email"
                error={ !!errors.email }
                helperText={ errors.email?.message }
            />

            <TextField
                { ...register('password') }
                label='Password'
                type="password"
                placeholder="Enter strong password"
                error={ !!errors.password }
                helperText={ errors.password?.message }
            />

            <TextField
                { ...register('birthDate') }
                type="date"
                error={ !!errors.birthDate }
                helperText={ errors.birthDate?.message }
            />

            <TextField
                { ...register('phoneNumber') }
                label='Phone number'
                type="tel"
                error={ !!errors.phoneNumber }
                helperText={ errors.phoneNumber?.message }
            />

            <TextField
                { ...register('country') }
                select
                label="Select country"
                error={ !!errors.country }
                helperText={ errors.country?.message }
            >
                { countries.map(country => (
                    <MenuItem key={ country } value={ country }>{ country }</MenuItem>
                )) }
            </TextField>

            <TextField
                { ...register('sport') }
                select
                label="Select sport"
                error={ !!errors.sport }
                helperText={ errors.sport?.message }
            >
                { sports.map(sport => (
                    <MenuItem key={ sport } value={ sport }>{ sport }</MenuItem>
                )) }
            </TextField><br />

            <StyledButton type='submit'>Register</StyledButton>
        </StyledForm>
    );
};

export default RegisterAthleteForm;
