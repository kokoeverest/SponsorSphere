import { PictureDto } from "@/types/picture";

export interface CreateBlogPostFormInput {
    content: string;
    authorId: number;
    pictures?: PictureDto[];
}

export interface UpdateBlogPostFormInput
{
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures?: any[];
}