import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import axios from 'axios';
import StyledButton from '../../components/controls/Button';
import { TextField } from '@mui/material';
import StyledForm from '../../components/controls/Form';

interface LoginFormInput {
    email: string;
    password: string;
}

const loginSchema = yup.object().shape({
    email: yup.string().email().required(),
    password: yup.string().min(8).max(32).required(),
});

const LoginForm: React.FC = () => {
    const { register, handleSubmit, formState: { errors }, reset } = useForm<LoginFormInput>({
        resolver: yupResolver(loginSchema),
    });

    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const onSubmitHandler: SubmitHandler<LoginFormInput> = async (data) => {
        try {
            const response = await axios.post('https://localhost:7026/login', data, {
                headers: {
                    'Content-Type': 'application/json',
                },
            });
            const token = response.data.token;
            localStorage.setItem('token', token); // Store the token in local storage
            navigate('/'); // Redirect to the home page or any protected route
            reset(); // Reset the form after successful submission
        } catch (err) {
            setError('Invalid email or password');
        }
    };

    return (
        <StyledForm onSubmit={ handleSubmit(onSubmitHandler) }>
            { error && <p className="error">{ error }</p> }
            <h1>Login</h1>
            <TextField
                { ...register('email') }
                required
                label='Email'
                type="email"
                error={ !!errors.email }
                helperText={ errors.email ? errors.email.message : '' }
            />
            <TextField
                { ...register('password') }
                required
                label='Password'
                type="password"
                error={ !!errors.password }
                helperText={ errors.password ? errors.password.message : '' }
            />
            <StyledButton type="submit">Login</StyledButton>
        </StyledForm>
    );
};

export default LoginForm;
