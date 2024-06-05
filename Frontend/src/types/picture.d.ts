export interface PictureDto {
    id: number;
    url: string | null;
    content: Uint8Array | string;
    modified: Date;
}

export interface CreatePictureDto {
    formFile: File;
}