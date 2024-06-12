import * as React from 'react';
import './Form.css';
import { Avatar, AvatarProps } from '@mui/material';

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
        { children }
    </Avatar>
);

export default ProfilePicture; 