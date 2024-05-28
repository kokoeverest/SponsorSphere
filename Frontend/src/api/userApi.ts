import { apiPrivate } from "./api";

export const getUserRole = async () => {
    const token = localStorage.getItem('token');
    // if (!token) throw new Error('authorisation failed');
    const bearerString = "Bearer ${token}";
    console.log(bearerString);
    
    const response = await apiPrivate.get('users/role', {
        headers: {
            Authorization: bearerString
        }
    });

    return response.data.role;
};