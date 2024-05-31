import React from "react";
import AdminDashboard from "./dashboards/AdminDashboard";
import SponsorDashboard from "./dashboards/SponsorDashboard";
import AthleteDashboard from "./dashboards/AthleteDashboard";
import { useAuth } from "@/hooks/useAuth";
import { Typography, CircularProgress, Container } from "@mui/material";

const Dashboard: React.FC = () => {
    const { user, loading, isAuthenticated } = useAuth();

    if (loading) {
        return (
            <Container>
                <CircularProgress />
                <Typography variant="h6">Loading...</Typography>
            </Container>
        );
    }

    if (!isAuthenticated) {
        return (
            <Container>
                <Typography variant="h6">You need to log in.</Typography>
            </Container>
        );
    }

    if (!user || user.role === null) {
        return (
            <Container>
                <Typography variant="h6">Role information is not available.</Typography>
            </Container>
        );
    }

    return (
        <div>
            <h3>Welcome to your {user.role}</h3>
            { user.role === 'Admin' && <AdminDashboard /> }
            { user.role === 'Athlete' && <AthleteDashboard /> }
            { user.role === 'Sponsor' && <SponsorDashboard /> }
        </div>
    );
}

export default Dashboard;