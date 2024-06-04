import React from "react";
import { Typography, Grid, Stack } from "@mui/material";
import AddAchievement from "@/features/athletes/achievements/AddAchievement";
import AddGoal from "@/features/athletes/goals/AddGoal";
import FeedButton from "../controls/FeedButton";

const AthleteDashboard: React.FC = () => (
    <Stack spacing={ 2 }>
        <Typography variant='h4' gutterBottom>Athlete Dashboard</Typography>

        <Grid item>
            <FeedButton />
        </Grid>
        <Grid item>
            <AddAchievement />
        </Grid>
        <Grid item>
            <AddGoal />
        </Grid>
    </Stack>
);

export default AthleteDashboard;