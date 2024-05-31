import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';

const AthleteList: React.FC = () => {
    const [athletes, setAthletes] = useState<AthleteDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [pageNumber, setPageNumber] = useState('1')
    const [ queryParams, setQueryParams ] = useState( '?sport=Football' );

    useEffect( () =>
    {
        setQueryParams( `?pageNumber=${ pageNumber }&pageSize=10` );
    }, [ pageNumber ] );

    useEffect(() => {
        const fetchData = async () => {
            try {
                const result = await athleteApi.getAthletes(queryParams);
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