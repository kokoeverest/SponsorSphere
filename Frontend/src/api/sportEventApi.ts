import { SportEventDto } from "@/types/sportEvent";
import { api } from "./api";
import { AxiosResponse } from "axios";
import { CreateSportEventFormInput } from "@/features/sportEvents/abstract";


const sportEventApi = {
    getSportEventById: async ( id: number ): Promise<SportEventDto> =>
    {
        const response: AxiosResponse<SportEventDto> = await api.get( `sportEvents/${ id }` );

        return response.data;
    },

    getFinishedSportEvents: async (): Promise<SportEventDto[]> =>
    {
        const response: AxiosResponse<SportEventDto[]> = await api.get( "sportEvents/finished" );

        return response.data;
    },

    getUnFinishedSportEvents: async (): Promise<SportEventDto[]> =>
    {
        const response: AxiosResponse<SportEventDto[]> = await api.get( "sportEvents/unfinished" );

        return response.data;
    },

    getPendingSportEvents: async (): Promise<SportEventDto[]> =>
    {
        const response: AxiosResponse<SportEventDto[]> = await api.get( "sportEvents/pending" );

        return response.data;
    },

    getPendingSportEventsCount: async (): Promise<number> =>
    {
        const response: AxiosResponse<number> = await api.get( "sportEvents/pendingCount" );

        return response.data;
    },

    createSportEvent: async ( data: CreateSportEventFormInput ): Promise<SportEventDto> =>
    {
        const response: AxiosResponse<SportEventDto> = await api.post( "sportEvents/create", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    },

    deleteSportEvent: async ( id: number ): Promise<void> =>
    {
        await api.delete( `sportEvents/delete?sportEventId=${ id }` );
    },

    updateSportEvent: async ( sportEvent: CreateSportEventFormInput ): Promise<SportEventDto> =>
    {
        const response: AxiosResponse<SportEventDto> = await api.patch( 'sportEvents/update', sportEvent, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    }
};

export default sportEventApi;