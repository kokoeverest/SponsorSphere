import React from "react";
import { Grid, Stack, Typography } from "@mui/material";
import FeedButton from "../controls/FeedButton";

const SponsorDashboard: React.FC = () => (
    <div>
        <Stack spacing={ 2 }>
            <Typography variant='h4'>Sponsor Dashboard</Typography>
            <Grid item>
                <FeedButton />
            </Grid>
            <Grid item>

            </Grid>
        </Stack>
    </div>
);

export default SponsorDashboard;