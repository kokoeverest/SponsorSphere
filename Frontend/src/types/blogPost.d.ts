import { PictureDto } from "./picture";

export interface BlogPostDto {
    id: number;
    created: string;
    content: string;
    authorId: number;
    pictures: PictureDto[];
}