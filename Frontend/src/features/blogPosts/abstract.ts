import { CreatePictureDto } from "@/types/picture";

export interface CreateBlogPostFormInput {
    content: string;
    authorId: number;
    pictures: CreatePictureDto[];
}

export interface UpdateBlogPostFormInput
{
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures: CreatePictureDto[];
}