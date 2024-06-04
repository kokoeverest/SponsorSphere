import React from "react";
import { Grid, Stack, Typography } from "@mui/material";
import FeedButton from "../controls/FeedButton";
import PendingSportEvents from "@/features/admins/PendingSportEvents";

const AdminDashboard: React.FC = () => (
    <div>
        <Stack spacing={ 2 }>
            <Typography variant='h4'>Admin Dashboard</Typography>

            <Grid item>
                <PendingSportEvents />
            </Grid>
            <Grid item>
                <FeedButton />
            </Grid>
            <Grid item>

            </Grid>
        </Stack>
    </div>
);
export default AdminDashboard;