import React from "react";
import AdminDashboard from "./dashboards/AdminDashboard";
import SponsorDashboard from "./dashboards/SponsorDashboard";
import AthleteDashboard from "./dashboards/AthleteDashboard";
import { useAuth } from "@/hooks/useAuth";
import { Typography } from "@mui/material";

const Dashboard: React.FC = () => {
    const user = useAuth();

    if (!user) {
        return <Typography variant='h6'>Loading...</Typography>;
    }

    if (user.role === null) {
        return <Typography variant="h6">Please log in</Typography>
    }

    return (
        <div>
            { user.role === 'admin' && <AdminDashboard /> }
            { user.role === 'athlete' && <AthleteDashboard /> }
            { user.role === 'sponsor' && <SponsorDashboard /> }
        </div>
    );
}

export default Dashboard;