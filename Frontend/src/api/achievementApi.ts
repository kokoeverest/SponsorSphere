import { AchievementDto } from "@/types/achievement";
import { api } from "./api";
import { CreateAchievementFormInput } from "@/features/athletes/achievements/abstract";
import { AxiosResponse } from "axios";

const achievementApi = {

    getAthleteAchievements: async ( athleteId: number, pageNumber: number ) =>
    {
        const response: AxiosResponse<AchievementDto[]> = await api.get( `achievements/${ athleteId }?pageNumber=${ pageNumber }&pageSize=10` );

        return response.data;
    },

    createAchievement: async ( data: CreateAchievementFormInput ): Promise<AchievementDto> =>
    {
        const response: AxiosResponse<AchievementDto> = await api.post( 'achievements/create', data, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    },

    deleteAchievement: async ( sportEventId: number, athleteId: number ): Promise<void> =>
    {
        await api.delete( `achievements/delete?sportEventId=${ sportEventId }&athleteId=${ athleteId }` );
    },

    updateAchievement: async ( achievement: CreateAchievementFormInput ): Promise<AchievementDto> =>
    {
        const response: AxiosResponse<AchievementDto> = await api.patch( 'achievements/update', achievement, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        } );
        return response.data;
    }
};


export default achievementApi;