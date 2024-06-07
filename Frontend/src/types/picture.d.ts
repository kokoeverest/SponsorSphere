export interface PictureDto {
    id: number;
    url: string | null;
    content: Uint8Array | ArrayBufferLike;
    modified: Date | null;
}

export interface CreatePictureDto {
    formFile: File;
    modified: null;
}