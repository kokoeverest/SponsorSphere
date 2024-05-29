// import { apiPrivate } from "./apiPrivate";
import { api } from "./api";

interface UserInfoResponse {
    role: string;
    userName: string;
}

export const getUserInfo = async () =>
{

    const response = await api.get<UserInfoResponse>( 'users/info' );

    return response.data;
};