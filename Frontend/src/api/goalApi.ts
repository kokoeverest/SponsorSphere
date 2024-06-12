import { GoalDto } from "@/types/goal";
import { api } from "./api";
import { CreateGoalFormInput } from "@/features/athletes/goals/abstract";
import { AxiosResponse } from "axios";

const goalApi = {

    getAthleteGoals: async ( athleteId: number, queryParams: string ) =>
    {
        const response: AxiosResponse<GoalDto[]> = await api.get( `goals/${ athleteId }?${ queryParams }` );

        return response.data;
    },

    createGoal: async ( data: CreateGoalFormInput ): Promise<GoalDto> =>
    {
        const response: AxiosResponse<GoalDto> = await api.post( 'goals/create', data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    },

    deleteGoal: async ( sportEventId: number, athleteId: number ): Promise<void> =>
    {
        await api.delete( `goals/delete?sportEventId=${ sportEventId }&athleteId=${ athleteId }` );
    },

    updateGoal: async ( goal: CreateGoalFormInput ): Promise<GoalDto> =>
    {
        const response: AxiosResponse<GoalDto> = await api.patch( 'goals/update', goal, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    }
};


export default goalApi;