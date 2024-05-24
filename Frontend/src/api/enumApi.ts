import { api } from "./api";

const enumApi = {
    getCountries: async (): Promise<string[]> => {
        const response = await api.get('enums/countries');
        return response.data;
    },
    getSports: async (): Promise<string[]> => {
        const response = await api.get('enums/sports');
        return response.data;
    },
};

export default enumApi;