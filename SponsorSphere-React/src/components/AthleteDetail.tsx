// src/components/AthleteDetail.tsx
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getAthleteById } from '../api/api';
import { AthleteDto } from '../types/athlete';

const AthleteDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [athlete, setAthlete] = useState<AthleteDto | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await getAthleteById(id!);
        setAthlete(result);
      } catch (error) {
        setError("Failed to fetch athlete details");
        console.error(error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  if (!athlete) return <p>No athlete found</p>;

  return (
    <div>
      <h1>{athlete.name} {athlete.lastName}</h1>
      <p>Age: {athlete.age}</p>
      <p>Sport: {athlete.sport}</p>
      <p>Email: {athlete.email}</p>
      <h3>Achievements</h3>
      <ul>
        {athlete.achievements.map((achievement, index) => (
          <li key={index}> {achievement.sport} {achievement.placeFinished} {achievement.description}</li>
        ))}
      </ul>
      <h3>Blog Posts</h3>
      <ul>
        {athlete.blogPosts.map((blogPost, index) => (
          <li key={index}>
            <p>Posted: {new Date(blogPost.created).toLocaleDateString()}</p>
            <p>{blogPost.content}</p>
            <img src={'data:image/jpeg;base64,${blogPost.pictures[0].content}'} />
          </li>
        ))}
      </ul>
      <p>Registered on: {new Date(athlete.created).toLocaleDateString()}</p>
      <p>Website: {athlete.website}</p>
      <p>Facebook: {athlete.faceBookLink}</p>
      <p>Instagram: {athlete.instagramLink}</p>
      <p>Strava: {athlete.stravaLink}</p>
      <p>Twitter: {athlete.twitterLink}</p>
    </div>
  );
};

export default AthleteDetail;
