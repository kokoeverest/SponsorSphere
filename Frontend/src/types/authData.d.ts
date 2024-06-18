import { UserInfoResponse } from "@/api/userApi";

export interface AuthData {
    isLogged: boolean;
    id: string | null;
    role: string | null;
    userName: string | null;
    userType: string | null,
    login: (userData: UserInfoResponse) => void;
    logout: () => void;
}