import { SponsorIndividualDto } from "../types/sponsorIndividual";
import { RegisterIndividualFormInput } from "@/features/sponsors/individuals/registration/abstract";
import { api } from "./api";

const sponsorIndividualApi = {
    register: async (data: RegisterIndividualFormInput): Promise<string> => {
        const response = await api.post("/users/sponsors/individuals/register", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });

        return response.data.id;
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