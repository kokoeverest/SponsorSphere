export interface CreateBlogPostFormInput {
    content: string;
    authorId: number;
    pictures: File[];
}

export interface UpdateBlogPostFormInput
{
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures?: File[];
}