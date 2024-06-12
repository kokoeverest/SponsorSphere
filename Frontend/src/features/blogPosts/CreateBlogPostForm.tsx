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
import CreateBlogPostSchema from "./schemas";
import { yupResolver } from "@hookform/resolvers/yup";
import { BlogPostDto } from "@/types/blogPost";


const CreateBlogPostForm: React.FC = () =>
{
    const { id } = useContext( AuthContext );
    const navigate = useNavigate();
    const queryClient = useQueryClient();
    const idAsNumber = Number( id );
    const [ _, setCreatedBlogPost ] = useState<BlogPostDto | null>( null );
    const [ pictures, setPictures ] = useState<File[]>( [] );


    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm( {
        resolver: yupResolver( CreateBlogPostSchema ),
    } );


    const blogPostCreateMutation = useMutation( {
        mutationFn: blogPostApi.createBlogPost,
        onSuccess: ( result ) =>
        {
            setCreatedBlogPost( result );
            alert("Successful!")
            queryClient.invalidateQueries( { queryKey: [ 'createBlogPost' ] } );
            navigate( `/feed` );
        },
        onError: () =>
        {
            <Alert severity='error' variant='standard'>Failed to create blog post!</Alert>;
        }
    } );

    const onSubmitHandler: SubmitHandler<CreateBlogPostFormInput> = async ( data ) =>
    {
        const formData = new FormData();
        formData.append( 'authorId', idAsNumber.toString() );
        formData.append( 'content', data.content );
        pictures.forEach( ( file, _ ) =>
        {
            formData.append( `pictures`, file );
        } );
        blogPostCreateMutation.mutate( formData );
    };

    const handlePictureUpload = ( file: File ) =>
    {
        setPictures( ( prevPictures ) => [ ...prevPictures, file ] );
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
                        multiline
                        fullWidth
                        rows={ 10 }
                        placeholder="Show your creativity, don't be shy"
                        error={ !!errors.content }
                        helperText={ errors.content?.message }
                        sx={{p: 2, m: 2}}
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
                                    src={ URL.createObjectURL( picture ) }
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
