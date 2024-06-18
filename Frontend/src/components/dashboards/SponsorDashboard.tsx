import React from "react";
import { Grid, Stack, Typography } from "@mui/material";
import FeedButton from "../controls/FeedButton";
import CreateBlogPost from "@/features/blogPosts/CreateBlogPost";
import MyProfile from "@/features/usersCommon/MyProfile";
import { UpdateUserProfile } from "@/features/usersCommon/UpdateUserProfile";

const SponsorDashboard: React.FC = () => (
    <div>
        <Stack spacing={ 2 }>
            <Typography variant='h4'>Sponsor Dashboard</Typography>

            <Grid item>
                <UpdateUserProfile />
            </Grid>

            <Grid item>
                <MyProfile />
            </Grid>

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