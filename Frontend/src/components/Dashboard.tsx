import React from "react";
import { useAuth } from "@/hooks/useAuth";
import { Typography, CircularProgress, Container, Drawer, Toolbar, Box } from "@mui/material";
import AdminDashboard from "./dashboards/AdminDashboard";
import SponsorDashboard from "./dashboards/SponsorDashboard";
import AthleteDashboard from "./dashboards/AthleteDashboard";
import { useNavigate } from "react-router-dom";

const drawerWidth = 240;

const Dashboard: React.FC = () =>
{
    const { user, loading, isAuthenticated } = useAuth();
    const navigate = useNavigate();

    if ( loading )
    {
        return (
            <Container>
                <CircularProgress />
                <Typography variant="h6">Loading...</Typography>
            </Container>
        );
    }

    if ( !user || !user.role || !isAuthenticated )
    {
        navigate( '/login' );
    }

    const renderDashboard = () =>
    {
        switch ( user!.role )
        {
            case 'Admin':
                return <AdminDashboard />;
            case 'Athlete':
                return <AthleteDashboard />;
            case 'Sponsor':
                return <SponsorDashboard />;
            default:
                navigate( '/' );
        }
    };

    return (
        <Drawer variant="permanent" sx={ {
            width: drawerWidth,
            flexShrink: 0,
            [ `& .MuiDrawer-paper` ]: { marginTop: 14, width: drawerWidth, backgroundColor: 'var(--backGroundBlue)' },
        } }>
            <Toolbar />
            <Box>
                { renderDashboard() }
            </Box>
        </Drawer>
    );
};

export default Dashboard;
