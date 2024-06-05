// import { styled } from '@mui/material/styles';
// import CloudUploadIcon from '@mui/icons-material/CloudUpload';
// import StyledButton from './Button';

// const VisuallyHiddenInput = styled( 'input' )( {
//     clip: 'rect(0 0 0 0)',
//     clipPath: 'inset(50%)',
//     height: 1,
//     overflow: 'hidden',
//     position: 'absolute',
//     bottom: 0,
//     left: 0,
//     whiteSpace: 'nowrap',
//     width: 1,
// } );

// export default function PictureUpload ()
// {
//     return (
//         <StyledButton
//             component="label"
//             role={ undefined }
//             variant="contained"
//             tabIndex={ -1 }
//             startIcon={ <CloudUploadIcon /> }
//         >
//             Add picture
//             <VisuallyHiddenInput type="file" />
//         </StyledButton>
//     );
// }


// // ----------------------------------------------
// import React, { useState } from 'react';
// import { styled } from '@mui/material/styles';
// import CloudUploadIcon from '@mui/icons-material/CloudUpload';
// import StyledButton from './Button';
// import { Alert, CircularProgress } from '@mui/material';
// import { useMutation } from '@tanstack/react-query';
// import pictureApi from '@/api/pictureApi';
// import { CreatePictureDto } from '@/types/picture';

// const VisuallyHiddenInput = styled( 'input' )( {
//     clip: 'rect(0 0 0 0)',
//     clipPath: 'inset(50%)',
//     height: 1,
//     overflow: 'hidden',
//     position: 'absolute',
//     bottom: 0,
//     left: 0,
//     whiteSpace: 'nowrap',
//     width: 1,
// } );

// const PictureUpload: React.FC = () =>
// {
//     const [ selectedFile, setSelectedFile ] = useState<File | null>( null );

//     const mutation = useMutation( {
//         mutationFn:  pictureApi.uploadPicture, 
//         onSuccess: () =>
//         {
//             // Handle success (e.g., show a success message, update the state, etc.)
//         },
//         onError: ( _: any ) =>
//         {
//             // Handle error (e.g., show an error message)
//         },
//     } );

//     const handleFileChange = ( event: React.ChangeEvent<HTMLInputElement> ) =>
//     {
//         if ( event.target.files && event.target.files[ 0 ] )
//         {
//             setSelectedFile( event.target.files[ 0 ] );
//         }
//     };

//     const handleUpload = () =>
//     {
//         if ( selectedFile )
//         {
//             const uploadData: CreatePictureDto = {
//                 formFile: selectedFile
//             };
//             mutation.mutate( uploadData );
//         }
//     };

//     return (
//         <>
//             <StyledButton
//                 component="label"
//                 role={ undefined }
//                 variant="contained"
//                 tabIndex={ -1 }
//                 startIcon={ <CloudUploadIcon /> }
//             >
//                 Add picture
//                 <VisuallyHiddenInput type="file" onChange={ handleFileChange } />
//             </StyledButton>
//             { selectedFile && (
//                 <StyledButton
//                     variant="contained"
//                     color="primary"
//                     onClick={ handleUpload }
//                 >
//                     Upload
//                 </StyledButton>
//             ) }
//             { mutation.isPending && <CircularProgress /> }
//             { mutation.isError && (
//                 <Alert severity="error">Failed to upload picture</Alert>
//             ) }
//             { mutation.isSuccess && (
//                 <Alert severity="success" >Picture uploaded successfully</Alert>
//             ) }
//         </>
//     );
// };

// export default PictureUpload;


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