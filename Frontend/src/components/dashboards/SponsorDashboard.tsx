import React from "react";
import { Grid, Stack, Typography } from "@mui/material";
import FeedButton from "../controls/FeedButton";
import CreateBlogPost from "@/features/blogPosts/CreateBlogPost";

const SponsorDashboard: React.FC = () => (
    <div>
        <Stack spacing={ 2 }>
            <Typography variant='h4'>Sponsor Dashboard</Typography>
            <Grid item>
                <FeedButton />
            </Grid>

            <Grid item>
                <CreateBlogPost />
            </Grid>


            <Grid item>

            </Grid>
        </Stack>
    </div>
);

export default SponsorDashboard;