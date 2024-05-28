import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useForm, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { useMutation, useQueryClient } from '@tanstack/react-query';

import StyledButton from '../../components/controls/Button';
import { TextField } from '@mui/material';
import StyledForm from '../../components/controls/Form';
import { loginSchema } from './schemas';
import { LoginFormInput } from './abstract';
import loginApi from '@/api/loginApi';

const LoginForm: React.FC = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const {
        register,
        handleSubmit,
        formState: { errors },
        reset
    } = useForm<LoginFormInput>({
        resolver: yupResolver(loginSchema),
    });

    const [error, setError] = useState<string | null>(null);

    const mutation = useMutation({
        mutationFn: loginApi.login,
        onSuccess: (token) => {
            localStorage.setItem('token', token);
            reset();
            navigate('/dashboard');

            // refresh the header so the login button will disappear
            queryClient.invalidateQueries({ queryKey: ['login'] });
        },
        onError: (error: any) => {
            setError(error.response?.data?.message || 'Invalid email or password!');
        },
    });
    const onSubmitHandler: SubmitHandler<LoginFormInput> = async (data) => {
        setError(null);
        mutation.mutate(data);
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
