import { SponsorIndividualDto } from "../types/sponsorIndividual";
import { RegisterIndividualFormInput } from "@/features/sponsors/individuals/registration/abstract";
import { api } from "./api";
import { UpdateSponsorIndividualProfileFormInput } from "@/features/sponsors/individuals/abstract";
import { AxiosResponse } from "axios";

const sponsorIndividualApi = {
    registerSponsorIndividual: async (data: RegisterIndividualFormInput): Promise<string> => {
        const response = await api.post("/users/sponsors/individuals/register", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });

        return response.data.id;
    },

    updateSponsorIndividual: async ( data: UpdateSponsorIndividualProfileFormInput ): Promise<SponsorIndividualDto> =>
    {
        const response: AxiosResponse<SponsorIndividualDto> = await api.patch( "users/sponsors/individuals/update", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );

        return response.data;
    },


    getSponsorIndividuals: async (queryParams: string): Promise<SponsorIndividualDto[]> => {
        const response = await api.get<SponsorIndividualDto[]>(`/users/sponsors/individuals${queryParams}`);

        return response.data;
    },

    getSponsorIndividualById: async (id: string): Promise<SponsorIndividualDto> => {
        const response = await api.get<SponsorIndividualDto>(`/users/sponsors/individuals/${id}`);
        return response.data;
    },

    getIndividualsCount: async () =>
    {
        const response = await api.get<number>( '/users/sponsors/individuals/count' );
        return response.data;
    }
};

export default sponsorIndividualApi;