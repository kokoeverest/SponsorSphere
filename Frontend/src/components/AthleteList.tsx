// src/components/AthleteList.tsx
import React, { useEffect, useState } from 'react';
import { getAthletes } from '../api/api';
import { AthleteDto } from '../types/athlete';
import { Link } from 'react-router-dom';

const AthleteList: React.FC = () => {
    const [athletes, setAthletes] = useState<AthleteDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const result = await getAthletes();
                setAthletes(result);
            } catch (error) {
                setError("Failed to fetch athletes");
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>{error}</p>;

    return (
        <div>
            <h1>Athletes</h1>
            <ul>
                {athletes.map(athlete => (
                    <li key={athlete.id}>
                        <Link to={`/athletes/${athlete.id}`}>
                            Name: {athlete.name}, Age: {athlete.age}, Sport: {athlete.sport}, Country: {athlete.country}
                        </Link>
                        <ul>
                            {athlete.achievements.map((achievement, index) => (
                                <li key={index}>{achievement.sport}</li>
                            ))}
                        </ul>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default AthleteList;