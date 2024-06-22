import * as React from 'react';
import './Form.css';
import { Avatar, AvatarProps } from '@mui/material';
import StyledText from './Typography';

const ProfilePicture: React.FC<AvatarProps> = ( { children, ...rest } ) => (
    <Avatar
        sx={
            {
                width: 150,
                height: 150
            }
        }
        { ...rest }
    >
        <StyledText variant='h3'>{ children }</StyledText>
    </Avatar>
);

export default ProfilePicture; 