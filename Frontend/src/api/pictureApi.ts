import { CreatePictureDto, PictureDto } from "@/types/picture";
import { api } from "./api";
import { AxiosResponse } from "axios";

const pictureApi = {

    getPictureById: async (pictureId: number): Promise<PictureDto> =>
        {
            const response = await api.get(`pictures/${pictureId}`);

            return response.data;
        },

    uploadPicture: async ( data: CreatePictureDto ): Promise<PictureDto> =>
    {
        const formData = new FormData();
        formData.append( 'formFile', data.formFile );

        const response = await api.post( 'pictures/upload', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        } );

        return response.data;
    },

    deletePicture: async (picture: PictureDto): Promise<void> =>
    {
        await api.delete( `pictures/delete`, {
            headers: { 'Content-Type': 'application/json' },
            data: picture,
        } );
    },

    updatePicture: async ( picture: PictureDto ): Promise<PictureDto> =>
    {
        const response: AxiosResponse<PictureDto> = await api.patch( 'pictures/update', picture, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );

        return response.data;
    }
};


export default pictureApi;