import { SponsorCompanyDto } from "../types/sponsorCompany";
import { RegisterCompanyFormInput } from "@/features/sponsors/companies/registration/abstract";
import { api } from "./api";
import { SponsorDto } from "@/types/sponsor";

const sponsorCompanyApi = {
    register: async (data: RegisterCompanyFormInput): Promise<string> => {
        const response = await api.post("/users/sponsors/companies/register", data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });

        return response.data.id;
    },

    getSponsorCompanies: async (queryParams: string): Promise<SponsorCompanyDto[]> => {
        const response = await api.get<SponsorCompanyDto[]>(`/users/sponsors/companies${queryParams}`);

        return response.data;
    },

    getSponsorCompanyById: async (id: string): Promise<SponsorDto> => {
        const response = await api.get<SponsorDto>(`/users/sponsors/companies/${id}`);
        return response.data;
    },

    getCompaniesCount: async () =>
    {
        const response = await api.get<number>( '/users/sponsors/companies/count' );
        return response.data;
    }
};

export default sponsorCompanyApi;