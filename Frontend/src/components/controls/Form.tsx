import * as React from 'react';
import Box, { BoxProps } from '@mui/material/Box';
import './Form.css';

const StyledForm: React.FC<BoxProps> = ({ children, ...rest }) => (
    <Box
        component="form"
        sx={ {
            p: 2,
            border: '1px solid lightgrey',
            gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))',
            '& .MuiTextField-root': { m: 2, width: '25ch' },
            width: 600,
            backgroundColor: 'var(--formGrey)',
            color: 'black'
        } }
        { ...rest }
    >
        { children }
    </Box>
);

export default StyledForm; 