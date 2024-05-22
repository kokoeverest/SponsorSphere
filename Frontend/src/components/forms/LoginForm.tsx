// src/components/LoginForm.tsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import '../../styles/LoginForm.css';
import StyledButton from '../controls/Button';

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7026/login', { email, password });
            const token = response.data.token;
            localStorage.setItem('token', token); // Store the token in local storage
            navigate('/'); // Redirect to the home page or any protected route
        } catch (err) {
            setError('Invalid email or password');
        }
    };

    return (
        <div className="login-form">
            {/* <h2>Login</h2> */}
            {error && <p className="error">{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <StyledButton type="submit" name='Login'></StyledButton>
            </form>
        </div>
    );
};

export default LoginForm;