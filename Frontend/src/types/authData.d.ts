export interface AuthData {
    isLogged: boolean;
    id: string | null;
    userName: string | null;
    role: string | null;
    login: (userData: userInfoResponse) => void;
    logout: () => void;
}