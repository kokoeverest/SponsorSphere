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
    onUpload: ( file: File ) => void;
}

const UploadPictureButton: React.FC<UploadPictureButtonProps> = ( { onUpload } ) =>
{
    const handleFileChange = ( event: React.ChangeEvent<HTMLInputElement> ) =>
    {
        const file = event.target.files?.[ 0 ];

        if ( file )
        {
            const endIndex = file?.name.length;
            const startIndex = endIndex - 3;
            console.log( file.name.slice( startIndex, endIndex ) );
            if ( [ 'jpg', 'png', 'gif' ].includes( file.name.slice( startIndex, endIndex ) ) )
            {
                onUpload( file );
            }
            else
            {
                alert( 'Not a valid image file. Only .jpg, .png, .gif are accepted' );
            }
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