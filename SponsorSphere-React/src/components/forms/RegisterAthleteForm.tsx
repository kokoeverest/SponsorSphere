
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const RegisterAthlete: React.FC = () => {
    const [name, setName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [countries, setCountries] = useState<string[]>([]);
    const [sports, setSports] = useState<string[]>([]);
    const [selectedCountry, setSelectedCountry] = useState('');
    const [selectedSport, setSelectedSport] = useState('');

    const navigate = useNavigate();

    useEffect(() => {
        const fetchEnums = async () => {
            try {
                const countriesResponse = await axios.get('https://localhost:7026/enums/countries');
                const sportsResponse = await axios.get('https://localhost:7026/enums/sports');
                setCountries(countriesResponse.data);
                setSports(sportsResponse.data);
                setSelectedCountry(countriesResponse.data[0]); // Set default selected value
                setSelectedSport(sportsResponse.data[0]);     // Set default selected value
            } catch (error) {
                console.error('Failed to fetch enum values', error);
            }
        };

        fetchEnums();
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7026/users/athletes/register', {
                Name: name,
                LastName: lastName,
                Email: email,
                Password: password,
                BirthDate: birthDate,
                PhoneNumber: phoneNumber,
                Country: selectedCountry,
                Sport: selectedSport
            });
            alert("You registered successfully!")
            navigate('/');
        } catch (err) {
            console.error('Registration failed', err);
            alert(err)
        }
    };

    return (
        <div className="login-form">
            <h2>Register as Athlete</h2>
            <form name="registerAthleteForm" method="post" onSubmit={handleSubmit}>
                <div>
                    <input
                        required
                        type="text"
                        name="Name"
                        placeholder="First name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>
                <div>
                    <input
                        required
                        type="text"
                        name="LastName"
                        placeholder="Last name"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                    />
                </div>
                <div>
                    <input
                        required
                        type="email"
                        name="Email"
                        id="Email"
                        placeholder="Enter a valid email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>
                <div>
                    <input
                        required
                        type="password"
                        name="Password"
                        id="Password"
                        placeholder="Enter strong password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <div>
                    <input
                        type="date"
                        name="BirthDate"
                        id="BirthDate"
                        value={birthDate}
                        onChange={(e) => setBirthDate(e.target.value)}
                    />
                </div>
                <div>
                    <input
                        type="tel"
                        name="PhoneNumber"
                        placeholder="Enter phone number"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                    />
                </div>
                <div>
                    <select
                        required
                        name="Country"
                        id="countrySelect"
                        value={selectedCountry}
                        onChange={(e) => setSelectedCountry(e.target.value)}
                    >
                        {countries.map(country => (
                            <option key={country} value={country}>{country}</option>
                        ))}
                    </select>
                </div>
                <div>
                    <select
                        required
                        name="Sport"
                        id="sportSelect"
                        value={selectedSport}
                        onChange={(e) => setSelectedSport(e.target.value)}
                    >
                        {sports.map(sport => (
                            <option key={sport} value={sport}>{sport}</option>
                        ))}
                    </select>
                </div>
                <div>
                    <button type='submit'>Register</button>
                </div>
            </form>
        </div>
    );
};

export default RegisterAthlete;
