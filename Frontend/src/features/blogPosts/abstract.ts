import { BlogPostPictureDto } from "@/types/blogPostPicture";

export interface CreateBlogPostFormInput {
    content: string;
    authorId: number;
    pictures?: BlogPostPictureDto[];
}

export interface UpdateBlogPostFormInput
{
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures?: BlogPostPictureDto[];
}