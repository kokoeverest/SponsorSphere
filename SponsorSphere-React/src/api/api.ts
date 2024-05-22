// src/api/api.ts
import axios from 'axios';
import { AthleteDto } from '../types/athlete';

const API_BASE_URL = 'https://localhost:7026/';

const api = axios.create({
  baseURL: API_BASE_URL,
});

export const getAthletes = async (): Promise<AthleteDto[]> => { 
  try {
    const response = await api.get<AthleteDto[]>('/users/athletes?pageNumber=1&pageSize=10');
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
};

export const getAthleteById = async (id: string): Promise<AthleteDto> => {
  try {
    const response = await api.get<AthleteDto>(`/users/athletes/${id}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
};

export default api;
