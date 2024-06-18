import { AthleteDto } from "../types/athlete";
import { RegisterAthleteFormInput } from "@/features/athletes/registration/abstract";
import { api } from "./api";
import { UpdateAthleteProfileFormInput } from "@/features/athletes/abstract";
import { AxiosResponse } from "axios";

const athleteApi = {
  registerAthlete: async (data: RegisterAthleteFormInput): Promise<number> => {
      const response:AxiosResponse<AthleteDto> = await api.post("users/athletes/register", data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      return response.data.id;
  },

  updateAthlete: async ( data: UpdateAthleteProfileFormInput ): Promise<AthleteDto> =>
  {
    const response: AxiosResponse<AthleteDto> = await api.patch( "users/athletes/update", data, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    } );

    return response.data;
  },

  getAthletes: async ( queryParams: string ): Promise<AthleteDto[]> => {
      const response = await api.get<AthleteDto[]>(`/users/athletes${queryParams}`);

      return response.data;
  },

  getAthleteById: async (id: string): Promise<AthleteDto> => {
      const response = await api.get<AthleteDto>(`/users/athletes/${id}`);
      return response.data;
  },

  getAthletesCount: async () => {
    const response = await api.get<number>('/users/athletes/count');
    return response.data;
  }
};

export default athleteApi;
