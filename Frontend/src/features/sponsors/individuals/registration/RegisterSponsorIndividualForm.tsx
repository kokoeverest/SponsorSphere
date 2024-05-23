import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { MenuItem, TextField } from '@mui/material';
import StyledButton from '../../../../components/controls/Button';
import StyledForm from '../../../../components/controls/Form';

interface RegisterIndividualFormInput {
    name: string;
    lastName: string;
    email: string;
    password: string;
    birthDate: string;
    phoneNumber: string;
    country: string;
}

const registerIndividualSchema = yup.object().shape({
    name: yup.string().min(2).max(200).required('First name is required'),
    lastName: yup.string().min(2).max(200).required('Last name is required'),
    email: yup.string().email('Must be a valid email').required('Email is required'),
    password: yup.string().min(8).max(32).required('Password is required'),
    birthDate: yup.string().required('Birthdate is required').typeError('Invalid date format'),
    phoneNumber: yup.string().required('Phone number is required'),
    country: yup.string().required('Country is required'),
});

const RegisterIndividual: React.FC = () => {
    const { register, handleSubmit, formState: { errors }, setValue } = useForm<RegisterIndividualFormInput>({
        resolver: yupResolver(registerIndividualSchema),
    });

    // const [name, setName] = useState('');
    // const [lastName, setLastName] = useState('');
    // const [email, setEmail] = useState('');
    // const [password, setPassword] = useState('');
    // const [birthDate, setBirthDate] = useState('');
    // const [phoneNumber, setPhoneNumber] = useState('');
    const [countries, setCountries] = useState<string[]>([]);
    const [selectedCountry, setSelectedCountry] = useState('');

    const navigate = useNavigate();

    useEffect(() => {
        const fetchEnums = async () => {
            try {
                const countriesResponse = await axios.get('https://localhost:7026/enums/countries');
                setCountries(countriesResponse.data);
                setSelectedCountry(countriesResponse.data[24]);
            } catch (error) {
                console.error('Failed to fetch enum values', error);
            }
        };

        fetchEnums();
    }, []);

    const onSubmitHandler: SubmitHandler<RegisterIndividualFormInput> = async (data) => {
        try {
            const response = await axios.post('https://localhost:7026/users/sponsors/individuals/register', data, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            const userId = response.data.id;
            alert("You registered successfully!");
            navigate(`/sponsors/individuals/${userId}`);
        } catch (err) {
            console.error('Registration failed', err);
            alert(err);
        }
    };

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
                // onChange={ handleCountryChange }
                error={ !!errors.country }
                helperText={ errors.country?.message }
            >
                { countries.map(country => (
                    <MenuItem key={ country } value={ country }>{ country }</MenuItem>
                )) }
            </TextField>

            <br />

            <StyledButton type='submit'>Register</StyledButton>
        </StyledForm>
    );
};

export default RegisterIndividual;
