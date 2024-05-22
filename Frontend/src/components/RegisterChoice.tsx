// src/components/RegisterChoice.tsx
import React from 'react';
import { useNavigate } from 'react-router-dom';

const RegisterChoice: React.FC = () => {
    const navigate = useNavigate();

    const handleRegisterAsAthlete = () => {
        navigate('/register/athlete');
    };

    const handleRegisterAsCompany = () => {
        navigate('/register/company');
    };

    const handleRegisterAsIndividual = () => {
        navigate('/register/individual');
    };

    return (
        <div className="register-choice">
            <h2>Please select the correct profile you want to create:</h2>
            <div className="container-buttons">
                <button onClick={handleRegisterAsAthlete}>I am an Athlete</button>
                <button onClick={handleRegisterAsCompany}>I represent a Company Sponsor</button>
                <button onClick={handleRegisterAsIndividual}>I am an Individual Sponsor</button>
            </div>
        </div>
    );
};

export default RegisterChoice;