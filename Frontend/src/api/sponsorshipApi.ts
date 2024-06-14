import { api } from "./api";
import { AxiosResponse } from "axios";
import { CreateSponsorshipFormInput } from "@/features/sponsorships/abstract";
import { SponsorshipDto } from "../types/sponsorship";

const sponsorshipApi = {

    getSponsorship: async ( athleteId: number, sponsorId: number ) =>
    {
        const response: AxiosResponse<SponsorshipDto> = await api.get( `sponsorships/${ athleteId }/${ sponsorId }` );

        return response.data;
    },

    getSponsorshipsByLevel: async ( level: string, queryParams: string ) =>
    {
        const response: AxiosResponse<SponsorshipDto[]> = await api.get( `sponsorships/level?level=${ level }${ queryParams}` );

        return response.data;
    },

    createSponsorship: async ( data: CreateSponsorshipFormInput ): Promise<SponsorshipDto> =>
    {
        const response: AxiosResponse<SponsorshipDto> = await api.post( 'sponsorships/create', data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    },

    deleteSponsorship: async ( athleteId: number ): Promise<void> =>
    {
        await api.delete( `sponsorships/delete?athleteId=${ athleteId }` );
    },

    updateSponsorship: async ( sponsorship: CreateSponsorshipFormInput ): Promise<SponsorshipDto> =>
    {
        const response: AxiosResponse<SponsorshipDto> = await api.patch( 'sponsorships/update', sponsorship, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    }
};


export default sponsorshipApi;