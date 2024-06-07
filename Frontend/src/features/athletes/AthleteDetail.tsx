import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';
import CircularProgress from '@mui/material/CircularProgress';
import { Alert, Avatar, Box } from '@mui/material';
import pictureApi from '@/api/pictureApi';

const AthleteDetail: React.FC = () =>
{
  const { id } = useParams<{ id: string; }>();
  const [ athlete, setAthlete ] = useState<AthleteDto | null>( null );
  const [ loading, setLoading ] = useState<boolean>( true );
  const [ error, setError ] = useState<string | null>( null );
  const [ profilePicture, setProfilePicture ] = useState<string | null>( null );

  useEffect( () =>
  {
    const fetchData = async () =>
    {
      try
      {
        const result = await athleteApi.getAthleteById( id! );
        setAthlete( result );
        console.log( result?.pictureId );
        if ( result?.pictureId )
        {
          const picture = await pictureApi.getPictureById( result.pictureId );
          setProfilePicture( picture );
        }
      } catch ( error )
      {
        setError( "Failed to fetch athlete details" );
        console.error( error );
      } finally
      {
        setLoading( false );
      }
    };

    fetchData();
  }, [ id ] );

  if ( loading ) return <CircularProgress />;
  if ( error ) return <Alert severity='error' variant='filled'>{ error }</Alert>;

  if ( !athlete ) return <p>No athlete found</p>;

  return (
    <div>
      <h1>{ athlete.name } { athlete.lastName }</h1>
      { profilePicture ? (
        <img
          src={ `data:image/jpeg;base64,${ profilePicture }` }
          width={ 100 }
          height={ 100 }
          style={ { objectFit: 'cover' } }
          alt="Profile"
        />
      ) : (
        <Avatar sx={ { width: 100, height: 100 } }>{ athlete.name.slice( 0, 1 ) }{ athlete.lastName.slice( 0, 1 ) }</Avatar>
      ) }
      <p>Age: { athlete.age }</p>
      <p>Sport: { athlete.sport }</p>
      <p>Email: { athlete.email }</p>
      <h3>Achievements</h3>
      <ul>
        { athlete.achievements.map( ( achievement, index ) => (
          <li key={ index }>{ achievement.sport } { achievement.placeFinished } { achievement.description }</li>
        ) ) }
      </ul>
      <h3>Blog Posts</h3>
      <ul>
        { athlete.blogPosts.map( ( blogPost, index ) => (
          <li key={ index }>
            <p>Posted: { new Date( blogPost.created ).toLocaleDateString() }</p>
            <p>{ blogPost.content.slice( 0, 50 ) }</p>
            { blogPost.pictures.length > 0 && (
              <img src={ `data:image/jpeg;base64,${ profilePicture }` } alt="Blog post" />
            ) }
          </li>
        ) ) }
      </ul>
      <Box>
        <p>Registered on: { new Date( athlete.created ).toLocaleDateString() }</p>
        <p>
          Website: { athlete.website }
          Facebook: { athlete.faceBookLink }
          Instagram: { athlete.instagramLink }
          Strava: { athlete.stravaLink }
          Twitter: { athlete.twitterLink }
        </p>
      </Box>
    </div>
  );
};

export default AthleteDetail;
