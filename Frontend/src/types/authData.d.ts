export interface AuthData {
    isLogged: boolean;
    userName: string | null;
    role: string | null;
    login: (userData: userInfoResponse) => void;
    logout: () => void;
}