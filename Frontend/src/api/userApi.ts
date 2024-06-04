// import { apiPrivate } from "./apiPrivate";
import { api } from "./api";

export interface UserInfoResponse {
    id: string;
    role: string;
    userName: string;
}

export const getUserInfo = async (): Promise<UserInfoResponse> =>
{
    const response = await api.get<UserInfoResponse>( 'users/info' );

    return response.data;
};