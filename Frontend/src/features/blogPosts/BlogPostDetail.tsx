import * as React from 'react';
import Dialog, { DialogProps } from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import AuthContext from '@/context/AuthContext';
import { useContext, useEffect, useRef, useState } from 'react';
import { BlogPostDto } from '@/types/blogPost';
import StyledButton from '@/components/controls/Button';
import { UserDto } from '@/types/user';
import { ImageList, ImageListItem } from '@mui/material';

interface BlogPostDetailProps
{
    blogPost: BlogPostDto | null;
    author: UserDto | null;
    open: boolean;
    onClose: () => void;
}

const BlogPostDetail: React.FC<BlogPostDetailProps> = ( { blogPost, author, open, onClose } ) =>
{
    const { id } = useContext( AuthContext );
    const [ scroll ] = useState<DialogProps[ 'scroll' ]>( 'paper' );

    const descriptionElementRef = useRef<HTMLElement>( null );

    useEffect( () =>
    {
        if ( open && descriptionElementRef.current )
        {
            descriptionElementRef.current.focus();
        }
    }, [ open ] );

    if ( !blogPost ) return null;

    return (
        <Dialog
            open={ open }
            onClose={ onClose }
            scroll={ scroll }
            aria-labelledby="scroll-dialog-title"
            aria-describedby="scroll-dialog-description"
        >
            <DialogTitle id="scroll-dialog-title">{ author?.name }'s post from { new Date( blogPost.created ).toLocaleDateString() }:</DialogTitle>
            
            <DialogContent dividers={ scroll === 'paper' }>
                <DialogContentText
                    id="scroll-dialog-description"
                    ref={ descriptionElementRef }
                    tabIndex={ -1 }
                >
                    { blogPost.content }
                </DialogContentText>


                <ImageList sx={ { display: 'flex', alignItems: 'center', maxWidthwidth: 640, maxHeight: 480 } } cols={ 3 }>
                    { blogPost.pictures.map( ( picture, index ) => (
                        <ImageListItem key={ index }>
                            <img
                                width={'inherit'}
                                height={'inherit'}
                                src={ `data:image/jpeg;base64,${ picture.content }` }
                                alt={ `picture${ index + 1 }` }
                                loading="lazy"
                            />
                        </ImageListItem>
                    ) ) }

                </ImageList>

            </DialogContent>
            <DialogActions>
                <StyledButton onClick={ onClose }>Close</StyledButton>
                { Number( id ) === blogPost.authorId && <StyledButton>Edit post</StyledButton> }
            </DialogActions>
        </Dialog>
    );
};

export default BlogPostDetail;
