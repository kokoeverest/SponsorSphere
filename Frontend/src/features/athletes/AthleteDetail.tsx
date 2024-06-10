import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';
import CircularProgress from '@mui/material/CircularProgress';
import { Alert, Avatar, Box, Link, List, ListItem, ListItemAvatar, ListItemButton, Stack, SvgIcon, Typography } from '@mui/material';
import pictureApi from '@/api/pictureApi';
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import WebhookIcon from '@mui/icons-material/Webhook';
import TwitterIcon from '@mui/icons-material/Twitter';
import BlogPostDetail from '../blogPosts/BlogPostDetail';
import AchievementDetail from './achievements/AchievementDetail';
import { BlogPostDto } from '@/types/blogPost';
import { UserDto } from '@/types/user';
import { AchievementDto } from '@/types/achievement';

const AthleteDetail: React.FC = () =>
{
  const { id } = useParams<{ id: string; }>();
  const [ athlete, setAthlete ] = useState<AthleteDto | null>( null );
  const [ loading, setLoading ] = useState<boolean>( true );
  const [ error, setError ] = useState<string | null>( null );
  const [ profilePicture, setProfilePicture ] = useState<string | null>( null );

  const [ selectedBlogPost, setSelectedBlogPost ] = useState<BlogPostDto | null>( null );
  const [ selectedAuthor, setSelectedAuthor ] = useState<UserDto | null>( null );
  const [ isBlogPostDetailOpen, setIsBlogPostDetailOpen ] = useState<boolean>( false );

  const [ selectedAchievement, setSelectedAchievement ] = useState<AchievementDto | null>( null );
  const [ selectedAthlete, setSelectedAthlete ] = useState<AthleteDto | null>( null );
  const [ isAchievementDetailOpen, setIsAchievementDetailOpen ] = useState<boolean>( false );

  useEffect( () =>
  {
    const fetchData = async () =>
    {
      try
      {
        const result = await athleteApi.getAthleteById( id! );
        setAthlete( result );
        if ( result?.pictureId )
        {
          const picture = await pictureApi.getPictureById( result.pictureId );
          setProfilePicture( picture.content );
        }
      } catch ( error )
      {
        setError( "Failed to fetch athlete details" );
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

  const handleBlogPostClick = ( blogPost: BlogPostDto, author: UserDto ) =>
  {
    setSelectedBlogPost( blogPost );
    setSelectedAuthor( author );
    setIsBlogPostDetailOpen( true );
  };

  const handleCloseBlogPostDetail = () =>
  {
    setIsBlogPostDetailOpen( false );
    setSelectedBlogPost( null );
  };

  const handleAchievementClick = ( achievement: AchievementDto, athlete: AthleteDto ) =>
  {
    setSelectedAchievement( achievement );
    setSelectedAthlete( athlete );
    setIsAchievementDetailOpen( true );
  };

  const handleCloseAchievementDetail = () =>
  {
    setIsAchievementDetailOpen( false );
    setSelectedAchievement( null );
  };

  return (
    <Box
      my={ 4 }
      display="flex"
      alignItems="center"
      gap={ 4 }
      p={ 2 }
      sx={ { border: '2px solid grey' } }>
      <List>
        <h1>{ athlete.name } { athlete.lastName }</h1>
        <ListItem>
          <ListItemAvatar>
            { profilePicture ? (
              <img
                src={ `data:image/jpeg;base64,${ profilePicture }` }
                width={ 100 }
                height={ 100 }
                alt="Profile"
              />
            ) : (
              <Avatar sx={ { width: 100, height: 100 } }>{ athlete.name.slice( 0, 1 ) }{ athlete.lastName.slice( 0, 1 ) }</Avatar>
            ) }
          </ListItemAvatar>
        </ListItem>
        <Typography variant="h6">SponsorSphere member since: <strong>{ new Date( athlete.created ).toLocaleDateString() }</strong></Typography>
        <Typography variant="h6">Age: <strong>{ athlete.age }</strong></Typography>
        <Typography variant="h6">Sport: <strong>{ athlete.sport }</strong></Typography>

        <h3>Achievements</h3>
        { athlete.achievements.map( ( achievement, index ) => (
          <List>
            <ListItem key={ index }>
              <ListItemButton onClick={ () => handleAchievementClick( achievement, athlete ) }>
                <Typography>Sport: { achievement.sport }</Typography>
                { achievement.placeFinished && <Typography>Finished: <strong>{ achievement.placeFinished }</strong></Typography> }
                { achievement.description && <Typography>Description: <strong>{ achievement.description }</strong></Typography> }
              </ListItemButton>
            </ListItem>
          </List>
        ) ) }

        <h3>Blog Posts</h3>
        { athlete.blogPosts.map( ( blogPost, index ) => (
          <List>
            <ListItem key={ index }>
              <ListItemButton onClick={ () => handleBlogPostClick( blogPost, athlete ) }>

                <Typography>Posted: { new Date( blogPost.created ).toLocaleDateString() }</Typography><br />
                <Typography>{ blogPost.content.slice( 0, 50 ) }</Typography>
              </ListItemButton>
            </ListItem>
          </List>
        ) ) }
        <Stack direction="row" spacing={ 4 } >
          <Link href={ athlete.website } target="_blank" rel="noopener noreferrer">
            <WebhookIcon />
          </Link>
          <Link href={ athlete.faceBookLink } target="_blank" rel="noopener noreferrer">
            <FacebookIcon />
          </Link>
          <Link href={ athlete.instagramLink } target="_blank" rel="noopener noreferrer">
            <InstagramIcon />
          </Link>
          <Link href={ athlete.stravaLink } target="_blank" rel="noopener noreferrer">
            <SvgIcon>
              {/* credit: plus icon from https://iconduck.com/free-icons/strava */ }
              <svg
                viewBox="0 0 48 48"
                xmlns="http://www.w3.org/2000/svg">
                <g
                  fill="none"
                  stroke="#000"
                  strokeLinecap="round"
                  strokeLinejoin="round">
                  <path
                    d="m31.016 26.855-11.177-22.355-11.178 22.355" />
                  <path
                    d="m22.694 26.855 8.322 16.645 8.323-16.645" />
                </g>
              </svg>
            </SvgIcon>
          </Link>
          <Link href={ athlete.twitterLink } target="_blank" rel="noopener noreferrer">
            <TwitterIcon />
          </Link>
      <Typography variant="h6">Email: <strong>{ athlete.email }</strong></Typography>
        </Stack>
      </List>

      <BlogPostDetail
        blogPost={ selectedBlogPost }
        author={ selectedAuthor }
        open={ isBlogPostDetailOpen }
        onClose={ handleCloseBlogPostDetail }
      />
      <AchievementDetail
        achievement={ selectedAchievement }
        athlete={ selectedAthlete }
        open={ isAchievementDetailOpen }
        onClose={ handleCloseAchievementDetail } />
    </Box>
  );
};

export default AthleteDetail;
