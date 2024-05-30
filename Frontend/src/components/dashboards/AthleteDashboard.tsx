import React from "react";
import { Typography } from "@mui/material";
import AddAchievement from "@/features/athletes/achievements/AddAchievement";

const AthleteDashboard: React.FC = () => (
    <div>
        <Typography variant='h4'>Athlete Dashboard</Typography>
        <AddAchievement>Add new achievement</AddAchievement>
    </div>
);

export default AthleteDashboard;