import React from 'react';
import { styled } from '@mui/material/styles';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import StyledButton from './Button';

const VisuallyHiddenInput = styled( 'input' )( {
    clip: 'rect(0 0 0 0)',
    clipPath: 'inset(50%)',
    height: 1,
    overflow: 'hidden',
    position: 'absolute',
    bottom: 0,
    left: 0,
    whiteSpace: 'nowrap',
    width: 1,
} );

interface UploadPictureButtonProps
{
    onUpload: ( file: File ) => Promise<void>;
}

const UploadPictureButton: React.FC<UploadPictureButtonProps> = ( { onUpload } ) =>
{
    const handleFileChange = ( event: React.ChangeEvent<HTMLInputElement> ) =>
    {
        const file = event.target.files?.[ 0 ];
        if ( file )
        {
            onUpload( file );
        }
    };

    return (
        <StyledButton
            component="label"
            role={ undefined }
            variant="contained"
            tabIndex={ -1 }
            startIcon={ <CloudUploadIcon /> }
        >
            Add picture
            <VisuallyHiddenInput type="file" onChange={ handleFileChange } />
        </StyledButton>
    );
};

export default UploadPictureButton;