export interface AuthData {
    isLogged: boolean;
    login: (userData: userInfoResponse) => void;
    logout: () => void;
}