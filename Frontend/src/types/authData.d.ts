export interface AuthData {
    login: (userData: userInfoResponse) => void;
    logout: () => void;
}