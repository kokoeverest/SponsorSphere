import React, { useContext, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import athleteApi from '@/api/athleteApi';
import { AthleteDto } from '../../types/athlete';
import CircularProgress from '@mui/material/CircularProgress';
import { Alert, Box, Divider, Link, List, ListItem, ListItemButton, Stack } from '@mui/material';
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
import StravaIcon from '../../assets/StravaIcon';
import { UpdateUserProfile } from '../usersCommon/UpdateUserProfile';

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
    <StyledBox sx={ { backgroundColor: 'whitesmoke' } }>
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
        <Divider />

        <Divider flexItem><StyledText>General info</StyledText></Divider>

        <StyledBox>
          <StyledText>SponsorSphere member since: <strong>{ new Date( athlete.created ).toLocaleDateString() }</strong></StyledText>
          <Divider hidden />
          <StyledText>Age: <strong>{ athlete.age }</strong></StyledText>
          <Divider hidden />
          <StyledText >Sport: <strong>{ athlete.sport }</strong></StyledText>
          <Divider hidden />
        </StyledBox>

        <Divider flexItem><StyledText>Sponsorships</StyledText></Divider>
        <StyledBox>
          { athlete.sponsorships && athlete.sponsorships.length > 0 ? (

            <List>
              <ListItemButton>
                <StyledText>{ athlete.name } has { athlete.sponsorships.length } sponsors</StyledText>

              </ListItemButton>
            </List>


          ) : (
            <StyledText>{ athlete.name } has no sponsors yet</StyledText>
          ) }
        </StyledBox>

        <Divider flexItem> <StyledText>Goals</StyledText> </Divider>
        <StyledBox>
          { athlete.goals && athlete.goals.length > 0 ? (
            <List>
              { athlete.goals.map( ( goal, index ) => (
                <ListItem key={ index }>
                  <ListItemButton onClick={ () => handleGoalClick( goal, athlete ) }>
                    <StyledText>{ `Sport: ${ goal.sport }` }</StyledText>
                  </ListItemButton>
                </ListItem>
              ) ) }
            </List>
          ) : (
            <StyledText>{ athlete.name } has no current goals</StyledText>
          ) }
        </StyledBox>


        <Divider flexItem> <StyledText>Achievements</StyledText></Divider>
        <StyledBox>
          <Stack>
            { athlete.achievements && athlete.achievements.length > 0 ? (
              <List>
                { athlete.achievements.map( ( achievement, index ) => (
                  <ListItem key={ index }>
                    <ListItemButton onClick={ () => handleAchievementClick( achievement, athlete ) }>
                      <StyledText>Sport: { achievement.sport }

                        { achievement.placeFinished && <StyledText>{ `Finished:  ${ achievement.placeFinished } place` }</StyledText> }
                        { achievement.description && <StyledText>{ `Description:  ${ achievement.description.slice( 0, 15 ) + "..." }` }</StyledText> }
                      </StyledText>
                    </ListItemButton>
                  </ListItem>
                ) ) }
              </List>
            ) : (
              <StyledText>{ athlete.name } hasn't added any achievements yet</StyledText>
            ) }
          </Stack>
        </StyledBox>

        <Divider flexItem><StyledText>Blog Posts</StyledText></Divider>
        <StyledBox>
          <Stack>
            { athlete?.blogPosts && athlete.blogPosts.length > 0 ? (
              athlete.blogPosts.map( ( blogPost, index ) => (
                <List>
                  <ListItem key={ index }>
                    <ListItemButton onClick={ () => handleBlogPostClick( blogPost, athlete ) }>
                      <StyledText>
                        { `Posted on: ${ new Date( blogPost.created ).toLocaleDateString() }
                        ${ blogPost.content.slice( 0, 15 ) + "..." }` }
                      </StyledText>
                    </ListItemButton>
                  </ListItem>
                </List>
              ) ) ) : (
              <StyledText>{ athlete?.name } hasn't shared anything yet</StyledText>
            ) }
          </Stack>
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

        <Divider flexItem><StyledText>Contacts and social media</StyledText></Divider>
        <Box>
          <Stack direction="row" spacing={ 8 } justifyContent="center">
            { athlete.website && (
              <Link href={ athlete.website } target="_blank" rel="noopener noreferrer">
                <WebhookIcon />
              </Link>
            ) }
            { athlete.faceBookLink && (
              <Link href={ athlete.faceBookLink } target="_blank" rel="noopener noreferrer">
                <FacebookIcon />
              </Link>
            ) }
            { athlete.instagramLink && (
              <Link href={ athlete.instagramLink } target="_blank" rel="noopener noreferrer">
                <InstagramIcon />
              </Link>
            ) }
            { athlete.stravaLink && (
              <Link href={ `${ athlete.stravaLink }` } target="_blank" rel="noopener noreferrer">
                <StravaIcon />
              </Link>
            ) }
            { athlete.twitterLink && (
              <Link href={ athlete.twitterLink } target="_blank" rel="noopener noreferrer">
                <TwitterIcon />
              </Link>
            ) }
            { athlete.email && (
              <Link href={ `mailto:${ athlete.email }` } target="_blank" rel="noopener noreferrer">
                <EmailIcon />
              </Link>
            ) }
          </Stack>
        </Box>
        
        <Divider flexItem />
        { athleteId == id && <UpdateUserProfile /> }

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
