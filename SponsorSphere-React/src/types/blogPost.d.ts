import { PictureDto } from "./picture";

export interface BlogPostDto {
//     public int Id { get; set; }
// public DateTime Created { get; set; } = DateTime.UtcNow;
// public required string Content { get; set; }
// public int AuthorId { get; set; }
// public User ? Author { get; set; }
// public ICollection<Picture> ? Pictures { get; set; }
    id: number;
    created: string;
    content: string;
    authorId: number;
    pictures: PictureDto[];
}