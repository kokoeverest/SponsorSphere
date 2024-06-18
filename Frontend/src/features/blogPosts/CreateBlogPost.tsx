import React from 'react';
import StyledButton from '@/components/controls/Button';
import { useNavigate } from 'react-router-dom';


const CreateBlogPost: React.FC = () =>
{
    const navigate = useNavigate();

    const createBlogPostHandler = () =>
    {
        navigate( '/blogposts/create' );
    };
    return (
        <StyledButton onClick={ createBlogPostHandler } sx={ { color: 'black', backgroundColor: 'var(--backGroundOrange)' } }>Create a blog post</StyledButton>
    );
};

export default CreateBlogPost;