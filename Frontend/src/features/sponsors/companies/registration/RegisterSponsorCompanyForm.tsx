import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { MenuItem, TextField } from '@mui/material';
import StyledButton from '../../../../components/controls/Button';
import StyledForm from '../../../../components/controls/Form';

const RegisterCompany: React.FC = () => {
    const [name, setName] = useState('');
    const [iban, setIban] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
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

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await axios.post('https://localhost:7026/users/sponsors/companies/register', {
                Name: name,
                Iban: iban,
                Email: email,
                Password: password,
                PhoneNumber: phoneNumber,
                Country: selectedCountry,
            }, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            alert("You registered successfully!");
            navigate('/');
        } catch (err) {
            console.error('Registration failed', err);
            alert(err);
        }
    };

    return (
        <StyledForm onSubmit={ handleSubmit }>
            <h1>Register as Sponsor</h1>

            <TextField
                required
                label='Company name'
                type="text"
                name="Name"
                placeholder="Company name"
                value={ name }
                onChange={ (e) => setName(e.target.value) }
            />

            <TextField
                required
                label='IBAN'
                type="text"
                name="Iban"
                placeholder="IBAN"
                value={ iban }
                onChange={ (e) => setIban(e.target.value) }
            />

            <TextField
                required
                label='Email'
                type="email"
                name="Email"
                placeholder="Enter a valid email"
                value={ email }
                onChange={ (e) => setEmail(e.target.value) }
            />

            <TextField
                required
                label='Password'
                type="password"
                name="Password"
                placeholder="Enter strong password"
                value={ password }
                onChange={ (e) => setPassword(e.target.value) }
            />

            <TextField
                required
                label='Phone number'
                type="tel"
                name="PhoneNumber"
                value={ phoneNumber }
                onChange={ (e) => setPhoneNumber(e.target.value) }
            />

            <TextField
                required
                select
                label="Select country"
                name="Country"
                value={ selectedCountry }
                onChange={ (e) => setSelectedCountry(e.target.value) }
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

export default RegisterCompany;
