import React from "react";
import { Typography } from "@mui/material";
import AddAchievement from "@/features/athletes/achievements/AddAchievement";
import AddGoal from "@/features/athletes/goals/AddGoal";

const AthleteDashboard: React.FC = () => (
    <div>
        <Typography variant='h4'>Athlete Dashboard</Typography>
        <AddAchievement />
        <AddGoal />
    </div>
);

export default AthleteDashboard;