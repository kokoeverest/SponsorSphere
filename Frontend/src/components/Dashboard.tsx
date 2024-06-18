import React, { useContext } from "react";
import { Drawer, Toolbar, Box } from "@mui/material";
import AdminDashboard from "./dashboards/AdminDashboard";
import SponsorDashboard from "./dashboards/SponsorDashboard";
import AthleteDashboard from "./dashboards/AthleteDashboard";
import { useNavigate } from "react-router-dom";
import AuthContext from "@/context/AuthContext";

const drawerWidth = 240;

const Dashboard: React.FC = () =>
{
    const { role, isLogged } = useContext( AuthContext );
    const navigate = useNavigate();

    if ( !isLogged )
    {
        navigate( '/login' );
    }

    const renderDashboard = () =>
    {
        switch ( role )
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
            [ `& .MuiDrawer-paper` ]: { marginTop: 'auto', width: drawerWidth, backgroundColor: 'var(--backGroundBlue)' },
        } }>
            <Toolbar />
            <Box>
                { renderDashboard() }
            </Box>
        </Drawer>
    );
};

export default Dashboard;
