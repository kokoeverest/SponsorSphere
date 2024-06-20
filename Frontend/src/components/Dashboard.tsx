import React, { useContext } from "react";
import { Drawer, Toolbar, Box, Stack, Typography, Grid } from "@mui/material";
import { useNavigate } from "react-router-dom";
import AuthContext from "@/context/AuthContext";
import StyledButton from "./controls/Button";
import MyProfile from "@/features/usersCommon/MyProfile";
import FeedButton from "./controls/FeedButton";
import CreateBlogPost from "@/features/blogPosts/CreateBlogPost";
import AddAchievement from "@/features/athletes/achievements/AddAchievement";
import AddGoal from "@/features/athletes/goals/AddGoal";
import PendingSportEvents from "@/features/admins/PendingSportEvents";
import { PendingSportEventsProvider } from "@/context/PendingSportEventsContext";


const Dashboard: React.FC = () =>
{
    const drawerWidth = 240;
    const navigate = useNavigate();
    const { role, isLogged } = useContext( AuthContext );
    const [ open, setOpen ] = React.useState( false );

    const toggleDrawer = ( newOpen: boolean ) => () =>
    {
        setOpen( newOpen );
    };

    if ( !isLogged )
    {
        navigate( '/login' );
    }

    const AdminDashboard = (
        <PendingSportEventsProvider>
            <Box sx={
                {
                    width: drawerWidth,
                    display: 'flex',
                    justifyContent: 'center'
                }
            }
                role="presentation"
                onClick={ toggleDrawer( false ) } >

                <Stack spacing={ 2 }>
                    <Typography variant='h4'>Admin</Typography>

                    <Grid item>
                        <PendingSportEvents />
                    </Grid>

                    <Grid item>
                        <FeedButton />
                    </Grid>
                    <Grid item>

                    </Grid>
                </Stack>
            </Box>
        </PendingSportEventsProvider>
    );

    const AthleteDashboard = (


        <Box sx={
            {
                width: drawerWidth,
                display: 'flex',
                justifyContent: 'center'
            }
        }
            role="presentation"
            onClick={ toggleDrawer( false ) } >

            <Stack spacing={ 2 }>
                <Typography variant='h4' gutterBottom>Athlete</Typography>

                <Grid item>
                    <MyProfile />
                </Grid>

                <Grid item>
                    <FeedButton />
                </Grid>

                <Grid item>
                    <CreateBlogPost />
                </Grid>

                <Grid item>
                    <AddAchievement />
                </Grid>

                <Grid item>
                    <AddGoal />
                </Grid>
            </Stack>
        </Box>
    );

    const SponsorDashboard = (
        <Box sx={
            {
                width: drawerWidth,
                display: 'flex',
                justifyContent: 'center'
            }
        }
            role="presentation"
            onClick={ toggleDrawer( false ) } >

            <Stack spacing={ 2 }>
                <Typography variant='h4'>Sponsor</Typography>

                <Grid item>
                    <MyProfile />
                </Grid>

                <Grid item>
                    <FeedButton />
                </Grid>

                <Grid item>
                    <CreateBlogPost />
                </Grid>

                <Grid item>

                </Grid>
            </Stack>
        </Box>
    );

    const renderDashboard = () =>
    {
        switch ( role )
        {
            case 'Admin':
                return AdminDashboard;
            case 'Athlete':
                return AthleteDashboard;
            case 'Sponsor':
                return SponsorDashboard;
            default:
                navigate( '/' );
        }
    };

    return (
        <div>
            <StyledButton onClick={ toggleDrawer( true ) }>Dashboard</StyledButton>
            <Drawer open={ open } anchor={ 'right' } onClose={ toggleDrawer( false ) }
                sx={ {
                    width: drawerWidth,
                    flexShrink: 0,

                    [ `& .MuiDrawer-paper` ]: {
                        marginTop: 'auto',
                        width: drawerWidth,
                        backgroundColor: 'var(--backGroundBlue)'
                    },
                } }>
                <Toolbar />
                <Box>
                    { renderDashboard() }
                </Box>
            </Drawer>
        </div>
    );
};

export default Dashboard;
