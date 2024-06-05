import { PictureDto } from "./picture";

export interface BlogPostDto {
    id: number;
    created: Date;
    content: string;
    authorId: number;
    pictures: PictureDto[];
}