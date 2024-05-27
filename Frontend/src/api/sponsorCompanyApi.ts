import { SponsorCompanyDto } from "../types/sponsorCompany";
import { RegisterCompanyFormInput } from "@/features/sponsors/companies/registration/abstract";
import { api } from "./api";

const sponsorCompanyApi = {
    register: async (data: RegisterCompanyFormInput): Promise<string> => {
        const response = await api.post("/users/sponsors/companies/register", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });

        return response.data.id;
    },

    getSponsorIndividuals: async (): Promise<SponsorCompanyDto[]> => {
        const response = await api.get<SponsorCompanyDto[]>("/users/sponsors/companies?pageNumber=1&pageSize=10");

        return response.data;
    },

    getSponsorIndividualById: async (id: string): Promise<SponsorCompanyDto> => {
        const response = await api.get<SponsorCompanyDto>(`/users/sponsors/companies/${id}`);
        return response.data;
    }
};

export default sponsorCompanyApi;