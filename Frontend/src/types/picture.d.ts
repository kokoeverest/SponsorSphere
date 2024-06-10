export interface PictureDto {
    id: number;
    url: string | null;
    content: Uint8Array;
    modified: Date | null;
}

export interface GetPictureDto
{
    id: number;
    url: string | null;
    content: string;
    modified: Date | null;
}

export interface CreatePictureDto {
    formFile: File;
    modified: null;
}