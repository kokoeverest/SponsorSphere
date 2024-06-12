import * as React from 'react';
import Box, { BoxProps } from '@mui/material/Box';
import './Form.css';

const StyledBox: React.FC<BoxProps> = ( { children, ...rest } ) => (
    <Box
        display='flex'
        alignItems="center"
        gap={ 2 }
        sx={ { border: '2px solid lightgrey', m: 2, p: 2, width: '85%' } }
        { ...rest }
    >
        { children }
    </Box>
);

export default StyledBox; 