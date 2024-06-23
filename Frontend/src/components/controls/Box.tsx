import * as React from 'react';
import Box, { BoxProps } from '@mui/material/Box';
import './Form.css';

const StyledBox: React.FC<BoxProps> = ( { children, ...rest } ) => (
    <Box
        gap={ 2 }
        sx={ {
            display: 'flex',
            justifyContent: "center",
            borderRadius: '10px',
            border: '2px solid var(--backGroundOrange)',
            m: 2,
            p: 2,
            width: '60em',
            maxWidth: '80%'
        } }
        { ...rest }
    >
        { children }
    </Box>
);

export default StyledBox; 