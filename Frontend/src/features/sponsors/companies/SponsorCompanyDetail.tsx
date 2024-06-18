// import React, { useEffect, useState } from 'react';
// import { useParams } from 'react-router-dom';
// import CircularProgress from '@mui/material/CircularProgress';
// import { Alert, Box, Divider, Link, List, ListItem, ListItemButton, Stack, Typography } from '@mui/material';
// import pictureApi from '@/api/pictureApi';
// import FacebookIcon from '@mui/icons-material/Facebook';
// import InstagramIcon from '@mui/icons-material/Instagram';
// import WebhookIcon from '@mui/icons-material/Webhook';
// import EmailIcon from '@mui/icons-material/Email';
// import TwitterIcon from '@mui/icons-material/Twitter';
// import BlogPostDetail from '../../blogPosts/BlogPostDetail';
// import { BlogPostDto } from '@/types/blogPost';
// import { UserDto } from '@/types/user';
// import StyledBox from '@/components/controls/Box';
// import StyledText from '@/components/controls/Typography';
// import ProfilePicture from '@/components/controls/ProfilePicture';
// import { SponsorDto } from '@/types/sponsor';
// import sponsorCompanyApi from '@/api/sponsorCompanyApi';
// import StravaIcon from '../../../assets/StravaIcon';
// import { SponsorCompanyDto } from '@/types/sponsorCompany';
// import { SponsorIndividualDto } from '@/types/sponsorIndividual';

// const SponsorCompanyDetail: React.FC = () =>
// {
//     const { sponsorId } = useParams<{ sponsorId: string; }>();
//     const [ sponsor, setSponsor ] = useState<SponsorDto | SponsorCompanyDto | SponsorIndividualDto | null>( null );
//     const [ loading, setLoading ] = useState<boolean>( true );
//     const [ error, setError ] = useState<string | null>( null );
//     const [ profilePicture, setProfilePicture ] = useState<string | null>( null );

//     const [ selectedBlogPost, setSelectedBlogPost ] = useState<BlogPostDto | null>( null );
//     const [ selectedAuthor, setSelectedAuthor ] = useState<UserDto | null>( null );
//     const [ isBlogPostDetailOpen, setIsBlogPostDetailOpen ] = useState<boolean>( false );

//     useEffect( () =>
//     {
//         const fetchSponsorData = async () =>
//         {
//             try
//             {
//                 const result = await sponsorCompanyApi.getSponsorCompanyById( sponsorId! );
//                 setSponsor( result );

//                 if ( result?.pictureId )
//                 {
//                     console.log( 'sponsor picture id:', result?.pictureId );
//                     const picture = await pictureApi.getPictureById( result.pictureId );
//                     setProfilePicture( picture.content );
//                 }

//             } catch ( error )
//             {
//                 setError( "Failed to fetch sponsor details" );
//             } finally
//             {
//                 setLoading( false );
//             }
//         };
//         console.log( 'sponsor id:', sponsorId );
//         fetchSponsorData();

//     }, [ sponsorId ] );

//     if ( loading ) return <CircularProgress />;
//     if ( error ) return <Alert severity='error' variant='filled'>{ error }</Alert>;

//     const handleBlogPostClick = ( blogPost: BlogPostDto, author: UserDto ) =>
//     {
//         setSelectedBlogPost( blogPost );
//         setSelectedAuthor( author );
//         setIsBlogPostDetailOpen( true );
//     };

//     const handleCloseBlogPostDetail = () =>
//     {
//         setIsBlogPostDetailOpen( false );
//         setSelectedBlogPost( null );
//     };

//     return (
//         <Box sx={ { backgroundColor: 'whitesmoke' } }>
//             <Stack alignItems="center" spacing={ 2 }>
//                 <h1>{ sponsor?.name } { sponsor?.lastName}</h1>

//                 { profilePicture ? (
//                     <ProfilePicture
//                         alt="Profile"
//                         src={ `data:image/jpeg;base64,${ profilePicture }` }
//                     />
//                 ) : (
//                     <ProfilePicture
//                         alt='Avatar'>
//                         { sponsor?.name.slice( 0, 1 ) } { sponsor?.lastName.slice( 0, 1 ) }
//                     </ProfilePicture>
//                 ) }
//                 <Divider />
//                 <Divider>General info</Divider>
//                 <StyledBox>

//                 <StyledText>SponsorSphere member since: <strong>{ new Date( sponsor!.created ).toLocaleDateString() }</strong></StyledText>
//                 <StyledText>Age: <strong>{ sponsor?.age }</strong></StyledText>
//                 <Divider hidden />

//                 <Divider hidden />
//                 </StyledBox>
//                 <Divider >Sponsorships</Divider>
//                 <StyledBox>
//                     { sponsor?.sponsorships && sponsor.sponsorships.length > 0 ? (


//                         <StyledText>
//                             { sponsor?.name } is sponsoring { sponsor?.sponsorships.length }
//                             { sponsor?.sponsorships.length === 1 ? ' athlete' : ' athletes' }
//                         </StyledText>

//                     ) : (
//                         <StyledText>{ sponsor?.name } is not sponsoring any athletes yet</StyledText>
//                     ) }
//                 </StyledBox>

//                 <Divider>Blog Posts</Divider>
//                 <StyledBox>
//                     <Stack>
//                         { sponsor?.blogPosts && sponsor.blogPosts.length > 0 ? (
//                             sponsor?.blogPosts.map( ( blogPost, index ) => (
//                                 <List>
//                                     <ListItem key={ index }>
//                                         <ListItemButton onClick={ () => handleBlogPostClick( blogPost, sponsor! ) }>

//                                             <Typography>Posted: { new Date( blogPost.created ).toLocaleDateString() }
//                                                 <strong>{ blogPost.content.slice( 0, 15 ) + "..." }</strong></Typography>
//                                         </ListItemButton>
//                                     </ListItem>
//                                 </List>
//                             ) )

//                         ) : (
//                             <StyledText>{ sponsor?.name } hasn't shared anything yet :( </StyledText>
//                         ) }
//                     </Stack>
//                 </StyledBox>

//                 <Box>
//                     <Stack direction="row" spacing={ 8 } justifyContent="center">
//                         { sponsor?.website && (
//                             <Link href={ sponsor.website } target="_blank" rel="noopener noreferrer">
//                                 <WebhookIcon />
//                             </Link>
//                         ) }
//                         { sponsor?.faceBookLink && (
//                             <Link href={ sponsor.faceBookLink } target="_blank" rel="noopener noreferrer">
//                                 <FacebookIcon />
//                             </Link>
//                         ) }
//                         { sponsor?.instagramLink && (
//                             <Link href={ sponsor.instagramLink } target="_blank" rel="noopener noreferrer">
//                                 <InstagramIcon />
//                             </Link>
//                         ) }
//                         { sponsor?.stravaLink && (
//                             <Link href={ `${ sponsor.stravaLink }` } target="_blank" rel="noopener noreferrer">
//                                 <StravaIcon />
//                             </Link>
//                         ) }
//                         { sponsor?.twitterLink && (
//                             <Link href={ sponsor.twitterLink } target="_blank" rel="noopener noreferrer">
//                                 <TwitterIcon />
//                             </Link>
//                         ) }
//                         { sponsor?.email && (
//                             <Link href={ `mailto:${ sponsor.email }` } target="_blank" rel="noopener noreferrer">
//                                 <EmailIcon />
//                             </Link>
//                         ) }
//                     </Stack>
//                 </Box>
//             </Stack>

//             <BlogPostDetail
//                 blogPost={ selectedBlogPost }
//                 author={ selectedAuthor }
//                 open={ isBlogPostDetailOpen }
//                 onClose={ handleCloseBlogPostDetail }
//             />
//         </Box>
//     );
// };

// export default SponsorCompanyDetail;


import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import CircularProgress from '@mui/material/CircularProgress';
import { Alert, Box, Divider, Link, List, ListItem, ListItemButton, Stack, Typography } from '@mui/material';
import pictureApi from '@/api/pictureApi';
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import WebhookIcon from '@mui/icons-material/Webhook';
import EmailIcon from '@mui/icons-material/Email';
import TwitterIcon from '@mui/icons-material/Twitter';
import BlogPostDetail from '../../blogPosts/BlogPostDetail';
import { BlogPostDto } from '@/types/blogPost';
import { UserDto } from '@/types/user';
import StyledBox from '@/components/controls/Box';
import StyledText from '@/components/controls/Typography';
import ProfilePicture from '@/components/controls/ProfilePicture';
import { SponsorDto } from '@/types/sponsor';
import StravaIcon from '../../../assets/StravaIcon';
import { SponsorCompanyDto } from '@/types/sponsorCompany';
import { SponsorIndividualDto } from '@/types/sponsorIndividual';
import sponsorCompanyApi from '@/api/sponsorCompanyApi';
import sponsorIndividualApi from '@/api/sponsorIndividualApi';

const SponsorCompanyDetail: React.FC = () =>
{
    const { sponsorId } = useParams<{ sponsorId: string; }>();
    const [ sponsor, setSponsor ] = useState<SponsorDto | SponsorCompanyDto | SponsorIndividualDto>( null );
    const [ loading, setLoading ] = useState<boolean>( true );
    const [ error, setError ] = useState<string | null>( null );
    const [ profilePicture, setProfilePicture ] = useState<string | null>( null );

    const [ selectedBlogPost, setSelectedBlogPost ] = useState<BlogPostDto | null>( null );
    const [ selectedAuthor, setSelectedAuthor ] = useState<UserDto | null>( null );
    const [ isBlogPostDetailOpen, setIsBlogPostDetailOpen ] = useState<boolean>( false );

    useEffect( () =>
    {
        const fetchSponsorData = async () =>
        {
            try
            {
                // create a logic to distinguish between company and individual to fetch
                const result = await sponsorCompanyApi.getSponsorCompanyById( sponsorId! );
                // const result = await sponsorIndividualApi.getSponsorIndividualById( sponsorId! );
                setSponsor( result );
                
                if ( result?.pictureId )
                {
                    console.log( 'sponsor picture id:', result?.pictureId );
                    const picture = await pictureApi.getPictureById( result.pictureId );
                    setProfilePicture( picture.content );
                }
            } catch ( error )
            {
                setError( "Failed to fetch sponsor details" );
            } finally
            {
                setLoading( false );
            }
        };
        console.log( 'sponsor id:', sponsorId );
        fetchSponsorData();
    }, [ sponsorId ] );

    if ( loading ) return <CircularProgress />;
    if ( error ) return <Alert severity='error' variant='filled'>{ error }</Alert>;

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

    const isSponsorIndividual = ( sponsor: SponsorDto | SponsorCompanyDto | SponsorIndividualDto ): sponsor is SponsorIndividualDto =>
    {
        return ( sponsor as SponsorIndividualDto ).age !== undefined && ( sponsor as SponsorIndividualDto ).lastName !== undefined;
    };

    return (
        <Box sx={ { backgroundColor: 'whitesmoke' } }>
            <Stack alignItems="center" spacing={ 2 }>
                <h1>{ sponsor?.name } { isSponsorIndividual( sponsor ) && sponsor.lastName }</h1>

                { profilePicture ? (
                    <ProfilePicture
                        alt="Profile"
                        src={ `data:image/jpeg;base64,${ profilePicture }` }
                    />
                ) : (
                    <ProfilePicture
                        alt='Avatar'>
                        { sponsor?.name.slice( 0, 1 ) } { isSponsorIndividual( sponsor ) && sponsor.lastName?.slice( 0, 1 ) }
                    </ProfilePicture>
                ) }
                <Divider />
                <Divider>General info</Divider>
                <StyledBox>
                    <StyledText>SponsorSphere member since: <strong>{ new Date( sponsor!.created ).toLocaleDateString() }</strong></StyledText>
                    {/* <StyledText>IBAN: {sponsor?.iban}</StyledText> */}
                    { isSponsorIndividual( sponsor ) && (
                        <>
                            <StyledText>Age: <strong>{ sponsor.age }</strong></StyledText>
                            <StyledText>LastName: <strong>{ sponsor.lastName }</strong></StyledText>
                        </>
                    ) }
                    <Divider hidden />
                </StyledBox>
                <Divider>Sponsorships</Divider>
                <StyledBox>
                    { sponsor?.sponsorships && sponsor.sponsorships.length > 0 ? (
                        <StyledText>
                            { sponsor?.name } is sponsoring { sponsor?.sponsorships.length }
                            { sponsor?.sponsorships.length === 1 ? ' athlete' : ' athletes' }
                        </StyledText>
                    ) : (
                        <StyledText>{ sponsor?.name } is not sponsoring any athletes yet</StyledText>
                    ) }
                </StyledBox>

                <Divider>Blog Posts</Divider>
                <StyledBox>
                    <Stack>
                        { sponsor?.blogPosts && sponsor.blogPosts.length > 0 ? (
                            sponsor?.blogPosts.map( ( blogPost, index ) => (
                                <List key={ index }>
                                    <ListItem>
                                        <ListItemButton onClick={ () => handleBlogPostClick( blogPost, sponsor! ) }>
                                            <Typography>Posted: { new Date( blogPost.created ).toLocaleDateString() }
                                                <strong>{ blogPost.content.slice( 0, 15 ) + "..." }</strong>
                                            </Typography>
                                        </ListItemButton>
                                    </ListItem>
                                </List>
                            ) )
                        ) : (
                            <StyledText>{ sponsor?.name } hasn't shared anything yet :(</StyledText>
                        ) }
                    </Stack>
                </StyledBox>

                <Box>
                    <Stack direction="row" spacing={ 8 } justifyContent="center">
                        { sponsor?.website && (
                            <Link href={ sponsor.website } target="_blank" rel="noopener noreferrer">
                                <WebhookIcon />
                            </Link>
                        ) }
                        { sponsor?.faceBookLink && (
                            <Link href={ sponsor.faceBookLink } target="_blank" rel="noopener noreferrer">
                                <FacebookIcon />
                            </Link>
                        ) }
                        { sponsor?.instagramLink && (
                            <Link href={ sponsor.instagramLink } target="_blank" rel="noopener noreferrer">
                                <InstagramIcon />
                            </Link>
                        ) }
                        { sponsor?.stravaLink && (
                            <Link href={ `${ sponsor.stravaLink }` } target="_blank" rel="noopener noreferrer">
                                <StravaIcon />
                            </Link>
                        ) }
                        { sponsor?.twitterLink && (
                            <Link href={ sponsor.twitterLink } target="_blank" rel="noopener noreferrer">
                                <TwitterIcon />
                            </Link>
                        ) }
                        { sponsor?.email && (
                            <Link href={ `mailto:${ sponsor.email }` } target="_blank" rel="noopener noreferrer">
                                <EmailIcon />
                            </Link>
                        ) }
                    </Stack>
                </Box>
            </Stack>

            <BlogPostDetail
                blogPost={ selectedBlogPost }
                author={ selectedAuthor }
                open={ isBlogPostDetailOpen }
                onClose={ handleCloseBlogPostDetail }
            />
        </Box>
    );
};

export default SponsorCompanyDetail;
