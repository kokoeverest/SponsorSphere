import * as React from 'react';
import Box, { BoxProps } from '@mui/material/Box';
import './Form.css';

const StyledForm: React.FC<BoxProps> = ({ children, ...rest }) => (
    <Box
        component="form"
        sx={ {
            m: 'auto',
            p: 2,
            borderRadius: '10px',
            position: 'sticky',
            border: '1px solid lightgrey',
            gridTemplateColumns: 'repeat(auto-fit, minmax(600px, 1fr))',
            '& .MuiTextField-root': { m: 2, width: '90%' },
            width: 640,
            backgroundColor: 'var(--formGrey)',
            color: 'black'
        } }
        { ...rest }
    >
        { children }
    </Box>
);

export default StyledForm; 