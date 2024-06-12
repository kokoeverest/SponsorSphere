import { BlogPostPictureDto } from "./blogPostPicture";

export interface BlogPostDto {
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures: PictureDto[];
}