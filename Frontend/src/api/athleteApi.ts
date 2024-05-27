// src/api/api.ts
import { AthleteDto } from "../types/athlete";
import { RegisterAthleteFormInput } from "@/features/athletes/registration/abstract";
import { api } from "./api";

const athleteApi = {
  register: async (data: RegisterAthleteFormInput): Promise<string> => {
      const response = await api.post("users/athletes/register", data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      return response.data.id;
  },

  getAthletes: async (): Promise<AthleteDto[]> => {
      const response = await api.get<AthleteDto[]>("/users/athletes?pageNumber=1&pageSize=10");

      return response.data;
  },

  getAthleteById: async (id: string): Promise<AthleteDto> => {
      const response = await api.get<AthleteDto>(`/users/athletes/${id}`);
      return response.data;
    }
};

export default athleteApi;
