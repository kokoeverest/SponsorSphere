import { UserInfoResponse } from "@/api/userApi";

export interface AuthData {
    isLogged: boolean;
    id: string | null;
    userName: string | null;
    role: string | null;
    login: (userData: UserInfoResponse) => void;
    logout: () => void;
}