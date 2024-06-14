import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';
import CircularProgress from '@mui/material/CircularProgress';
import { Alert, Box, Link, List, ListItem, ListItemButton, Stack, SvgIcon, Typography } from '@mui/material';
import pictureApi from '@/api/pictureApi';
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import WebhookIcon from '@mui/icons-material/Webhook';
import EmailIcon from '@mui/icons-material/Email';
import TwitterIcon from '@mui/icons-material/Twitter';
import BlogPostDetail from '../blogPosts/BlogPostDetail';
import AchievementDetail from './achievements/AchievementDetail';
import { BlogPostDto } from '@/types/blogPost';
import { UserDto } from '@/types/user';
import { AchievementDto } from '@/types/achievement';
import { GoalDto } from '@/types/goal';
import GoalDetail from './goals/GoalDetail';
import StyledBox from '@/components/controls/Box';
import StyledText from '@/components/controls/Typography';
import ProfilePicture from '@/components/controls/ProfilePicture';
import StyledButton from '@/components/controls/Button';
import AuthContext from '@/context/AuthContext';
import { SponsorDto } from '@/types/sponsor';
import sponsorCompanyApi from '@/api/sponsorCompanyApi';
import { SponsorshipDto } from '@/types/sponsorship';
import sponsorshipApi from '@/api/sponsorshipApi';

const AthleteDetail: React.FC = () =>
{
  const navigate = useNavigate();
  const { id, role } = useContext( AuthContext );
  const { id: athleteId } = useParams<{ id: string; }>();
  const [ athlete, setAthlete ] = useState<AthleteDto | null>( null );
  const [ sponsor, setSponsor ] = useState<SponsorDto | null>( null );
  const [ loading, setLoading ] = useState<boolean>( true );
  const [ error, setError ] = useState<string | null>( null );
  const [ profilePicture, setProfilePicture ] = useState<string | null>( null );
  const [ existingSponsorship, setExistingSponsorship ] = useState<SponsorshipDto | null>( null );

  const [ selectedBlogPost, setSelectedBlogPost ] = useState<BlogPostDto | null>( null );
  const [ selectedAuthor, setSelectedAuthor ] = useState<UserDto | null>( null );
  const [ isBlogPostDetailOpen, setIsBlogPostDetailOpen ] = useState<boolean>( false );

  const [ selectedAchievement, setSelectedAchievement ] = useState<AchievementDto | null>( null );
  const [ selectedAthlete, setSelectedAthlete ] = useState<AthleteDto | null>( null );
  const [ isAchievementDetailOpen, setIsAchievementDetailOpen ] = useState<boolean>( false );

  const [ selectedGoal, setSelectedGoal ] = useState<GoalDto | null>( null );
  const [ isGoalDetailOpen, setIsGoalDetailOpen ] = useState<boolean>( false );

  useEffect( () =>
  {
    const fetchAthleteData = async () =>
    {
      try
      {
        const result = await athleteApi.getAthleteById( athleteId! );
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

    fetchAthleteData();
  }, [ athleteId ] );

  useEffect( () =>
  {
    const fetchSponsorData = async () =>
    {
      try
      {
        const result = await sponsorCompanyApi.getSponsorCompanyById( id! );
        setSponsor( result );

      } catch ( error )
      {
        setError( "Failed to fetch sponsor details" );
      } finally
      {
        setLoading( false );
      }
    };

    if ( role === 'Sponsor' )
    {
      fetchSponsorData();
    }
  }, [ id ] );

  useEffect( () =>
  {
    const fetchSponsorshipData = async () =>
    {
      try
      {
        if ( athlete && sponsor )
        {
          const result = await sponsorshipApi.getSponsorship( athlete?.id, sponsor.id );
          setExistingSponsorship( result );
        }
      } catch ( error )
      {
        setExistingSponsorship( null );
      } finally
      {
        setLoading( false );
      }

    };

    fetchSponsorshipData();

  }, );

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

  const handleGoalClick = ( goal: GoalDto, athlete: AthleteDto ) =>
  {
    setSelectedGoal( goal );
    setSelectedAthlete( athlete );
    setIsGoalDetailOpen( true );
  };

  const handleCloseGoalDetail = () =>
  {
    setIsGoalDetailOpen( false );
    setSelectedGoal( null );
  };

  return (
    <StyledBox>
      <Stack alignItems="center" spacing={ 2 }>
        <h1>{ athlete.name } { athlete.lastName }</h1>

        { profilePicture ? (
          <ProfilePicture
            alt="Profile"
            src={ `data:image/jpeg;base64,${ profilePicture }` }
          />
        ) : (
          <ProfilePicture
            alt='Avatar'>
            { athlete.name.slice( 0, 1 ) }{ athlete.lastName.slice( 0, 1 ) }
          </ProfilePicture>
        ) }


        <StyledText>SponsorSphere member since: <strong>{ new Date( athlete.created ).toLocaleDateString() }</strong></StyledText>
        <StyledText>Age: <strong>{ athlete.age }</strong></StyledText>
        <StyledText >Sport: <strong>{ athlete.sport }</strong></StyledText>

        <StyledBox>
          <StyledText>Sponsorships:</StyledText><br />
          { athlete.sponsorships && athlete.sponsorships.length > 0 ? (
          
            <List>
                <ListItemButton>
                <StyledText variant='h6'>{ athlete.name } has { athlete.sponsorships.length } sponsors</StyledText>

                </ListItemButton>
            </List>
          
          
          ) : (
            <StyledText>{ athlete.name } has no sponsors yet</StyledText>
          ) }
        </StyledBox>

        <StyledBox>
          <StyledText>Goals:</StyledText>
          { athlete.goals && athlete.goals.length > 0 ? (
            <List>
              { athlete.goals.map( ( goal, index ) => (
                <ListItem key={ index }>
                  <ListItemButton onClick={ () => handleGoalClick( goal, athlete ) }>
                    <StyledText>Sport: { goal.sport }</StyledText>
                  </ListItemButton>
                </ListItem>
              ) ) }
            </List>
          ) : (
            <StyledText>{ athlete.name } has no current goals</StyledText>
          ) }
        </StyledBox>

        { role === 'Sponsor' && !existingSponsorship
          ? <StyledButton onClick={ () => navigate( `/sponsorships/create`, { state: { athlete, sponsor } } ) }>Become a sponsor</StyledButton>
          : ( role === 'Sponsor' &&
            <StyledBox className='container-buttons'>
              <StyledButton>Edit sponsorship</StyledButton>
              <StyledButton>Cancel sponsorship</StyledButton>
            </StyledBox>
          )
        }

        <StyledBox>
          <Stack>
            <StyledText>Achievements:</StyledText>
            { athlete.achievements.map( ( achievement, index ) => (
              <List>
                <ListItem key={ index }>
                  <ListItemButton onClick={ () => handleAchievementClick( achievement, athlete ) }>
                    <StyledText>Sport: { achievement.sport } </StyledText>
                    { achievement.placeFinished && <Typography>Finished: <strong>{ achievement.placeFinished }</strong></Typography> }
                    { achievement.description && <Typography>Description: <strong>{ achievement.description.slice( 0, 15 ) + "..." }</strong></Typography> }
                  </ListItemButton>
                </ListItem>
              </List>
            ) ) }
          </Stack>
        </StyledBox>

        <StyledBox>
          <Stack>
            <StyledText>Blog Posts:</StyledText>
            { athlete.blogPosts.map( ( blogPost, index ) => (
              <List>
                <ListItem key={ index }>
                  <ListItemButton onClick={ () => handleBlogPostClick( blogPost, athlete ) }>

                    <Typography>Posted: { new Date( blogPost.created ).toLocaleDateString() }
                      <strong>{ blogPost.content.slice( 0, 15 ) + "..." }</strong></Typography>
                  </ListItemButton>
                </ListItem>
              </List>
            ) ) }
          </Stack>
        </StyledBox>

        <p>TODO: Fix the links</p>
        <Box>
          <Stack direction="row" spacing={ 8 } justifyContent="center" >
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
              <SvgIcon >
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
            <Link href={ athlete.email } target="_blank" rel="noopener noreferrer">
              <EmailIcon />
            </Link>
          </Stack>
        </Box>
      </Stack>

      <GoalDetail
        goal={ selectedGoal }
        athlete={ selectedAthlete }
        open={ isGoalDetailOpen }
        onClose={ handleCloseGoalDetail }
      />

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
    </StyledBox>
  );
};

export default AthleteDetail;
