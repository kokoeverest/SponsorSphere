import React, { useContext, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useNavigate } from "react-router-dom";
import { Alert, TextField, Grid, Typography } from "@mui/material";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import StyledButton from "../../components/controls/Button";
import StyledForm from "../../components/controls/Form";
import { CreateBlogPostFormInput } from "./abstract";
import CreateBlogPostSchema from "./schemas";
import AuthContext from "@/context/AuthContext";
import UploadPictureButton from "@/components/controls/UploadPictureButton";
import blogPostApi from "@/api/blogPostApi";
import pictureApi from "@/api/pictureApi";
import { PictureDto } from "@/types/picture";

const CreateBlogPostForm: React.FC = () =>
{
    const { id } = useContext( AuthContext );
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const idAsNumber = Number( id );

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<CreateBlogPostFormInput>( {
        resolver: yupResolver( CreateBlogPostSchema ),
    } );

    const [ pictures, setPictures ] = useState<any[]>( [] );

    // Mutations
    const mutation = useMutation( {
        mutationFn: blogPostApi.createBlogPost,
        onSuccess: () =>
        {
            <Alert severity='success' variant='filled'>Successful!</Alert>;
            navigate( `/feed` );
            queryClient.invalidateQueries( { queryKey: [ 'createBlogPost' ] } );
        },
    } );

    const onSubmitHandler: SubmitHandler<CreateBlogPostFormInput> = async ( data ) =>
    {
        data.authorId = idAsNumber;
        data.pictures = pictures;
        mutation.mutate( data );
    };

    const handlePictureUpload = async ( file: File ) =>
    {
        try
        {
            const uploadedPicture: PictureDto = await pictureApi.uploadPicture( { formFile: file } );
            setPictures( ( prevPictures ) => [ ...prevPictures, uploadedPicture ] );
        } catch ( error )
        {
            console.error( 'Error uploading picture:', error );
        }
    };

    return (

        <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
            <h1>Create a blog post</h1>

            <input type="hidden" value={ id ? Number( id ) : '' } { ...register( "authorId" ) } />

            <TextField
                { ...register( 'content' ) }
                label="Content"
                type="text"
                fullWidth
                multiline
                rows={ 10 }
                placeholder="Show your creativity, don't be shy"
                error={ !!errors.content }
                helperText={ errors.content?.message }
            />

            <UploadPictureButton onUpload={ handlePictureUpload } />
            <Typography variant="body2">
                { pictures.length } { pictures.length === 1 ? 'picture' : 'pictures' } uploaded
            </Typography>

            <Grid container spacing={ 2 }>
                { pictures.map( ( picture, index ) => (
                    <Grid item key={ index }>
                        <img
                            src={ `data:image/jpeg;base64,${ picture.content }` }
                            alt={ `Uploaded ${ index + 1 }` }
                            width={ 100 }
                            height={ 100 }
                            style={ { objectFit: 'cover' } }
                        />
                    </Grid>
                ) ) }
            </Grid>

            <br />
            <StyledButton type="submit">Submit</StyledButton>
        </StyledForm>
    );
};

export default CreateBlogPostForm;
