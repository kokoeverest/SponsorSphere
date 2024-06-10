import React, { useContext, useState } from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { Alert, TextField, Grid, Typography, CircularProgress } from "@mui/material";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import StyledButton from "../../components/controls/Button";
import StyledForm from "../../components/controls/Form";
import { CreateBlogPostFormInput } from "./abstract";
import AuthContext from "@/context/AuthContext";
import UploadPictureButton from "@/components/controls/UploadPictureButton";
import blogPostApi from "@/api/blogPostApi";
import pictureApi from "@/api/pictureApi";
import { PictureDto } from "@/types/picture";
import CreateBlogPostSchema from "./schemas";
import { yupResolver } from "@hookform/resolvers/yup";
import { BlogPostPictureDto } from "@/types/blogPostPicture";
import { BlogPostDto } from "@/types/blogPost";


const CreateBlogPostForm: React.FC = () =>
{
    const { id } = useContext( AuthContext );
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const idAsNumber = Number( id );
    const [ createdBlogPost, setCreatedBlogPost ] = useState<BlogPostDto | null >(null);
    const [ pictures, setPictures ] = useState<PictureDto[]>( [] );
    const [ blogPostPictures, setBlogPostPictures ] = useState<BlogPostPictureDto[]>( [] );

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<CreateBlogPostFormInput>( {
        resolver: yupResolver( CreateBlogPostSchema ),
    } );


    const blogPostCreateMutation = useMutation( {
        mutationFn: blogPostApi.createBlogPost,
        onSuccess: ( result ) =>
        {
            // navigate( `/feed` );
            setCreatedBlogPost(result);
            // queryClient.invalidateQueries( { queryKey: [ 'createBlogPost' ] } );
            <Alert severity='success' variant='filled'>Successful!</Alert>;
        },
        onError: () =>
        {
            pictures.forEach( ( pic ) =>
            {
                pictureDeleteMutation.mutate( pic );
            } );

            if (createdBlogPost)
            blogPostDeleteMutation.mutate(createdBlogPost);
        
            navigate( `/feed` );
        }
    } );

    const blogPostDeleteMutation = useMutation({
        mutationFn: blogPostApi.deleteBlogPost,
    });

    const pictureUploadMutation = useMutation( {
        mutationFn: pictureApi.uploadPicture,
        onSuccess: ( result ) =>
        {
            setPictures( ( prevPictures: PictureDto[] ) => [ ...prevPictures, result ] );
            setBlogPostPictures( ( prevBlogPostPictures: BlogPostPictureDto[] ) =>
                [ ...prevBlogPostPictures, { pictureId: result.id, blogPostId: 0 } ] );
        }
    } );

    const pictureDeleteMutation = useMutation( {
        mutationFn: pictureApi.deletePicture
    } );

    const onSubmitHandler: SubmitHandler<CreateBlogPostFormInput> = async ( data ) =>
    {
        data.pictures = blogPostPictures;
        console.log( data );
        blogPostCreateMutation.mutate( data );
    };

    const handlePictureUpload = async ( file: File ) =>
    {
        pictureUploadMutation.mutate( { formFile: file, modified: null } );
    };

    return (
        <>
            { !blogPostCreateMutation.isError && !blogPostCreateMutation.isPending && (
                <StyledForm onSubmit={ handleSubmit( onSubmitHandler ) }>
                    <input hidden value={ idAsNumber } { ...register( "authorId" ) } />

                    <h1>Create a blog post</h1>
                    <TextField
                        { ...register( 'content' ) }
                        type="text"
                        fullWidth
                        multiline
                        rows={ 10 }
                        placeholder="Show your creativity, don't be shy"
                        error={ !!errors.content }
                        helperText={ errors.content?.message }
                    />
                    <br />
                    <UploadPictureButton onUpload={ handlePictureUpload } />
                    <Typography variant="body2">
                        { pictures.length } { pictures.length === 1 ? 'picture' : 'pictures' } uploaded
                    </Typography>

                    <Grid container spacing={ 2 } { ...register( "pictures" ) }>

                        { ...pictures.map( ( picture, index ) => (
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
            ) }
            { blogPostCreateMutation.isError && <Alert severity='error' variant='filled'>Error</Alert> }
            { ( blogPostCreateMutation.isPending ) && <CircularProgress /> }
        </>
    );
};

export default CreateBlogPostForm;
