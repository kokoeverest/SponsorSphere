// src/api/api.ts
import axios from "axios";

import { AthleteDto } from "../types/athlete";
import { API_BASE_URL } from "@/common/constants";
import { RegisterAthleteFormInput } from "@/features/athletes/registration/abstract";

const api = axios.create({
  baseURL: API_BASE_URL,
});

const athleteApi = {
  register: async (data: RegisterAthleteFormInput): Promise<string> => {
    try {
      const response = await api.post("users/athletes/register", data, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      return response.data.id;
    } catch (err) {
      console.error("Registration failed", err);
      alert(err);
      return "error";
    }
  },

  getAthletes: async (): Promise<AthleteDto[]> => {
    try {
      const response = await api.get<AthleteDto[]>(
        "/users/athletes?pageNumber=1&pageSize=10"
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching data:", error);
      throw error;
    }
  },

  getAthleteById: async (id: string): Promise<AthleteDto> => {
    try {
      const response = await api.get<AthleteDto>(`/users/athletes/${id}`);
      return response.data;
    } catch (error) {
      console.error("Error fetching data:", error);
      throw error;
    }
  },
};

export default athleteApi;
