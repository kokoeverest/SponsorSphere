import { LoginFormInput } from "@/features/login/abstract";
import { api } from "./api";


const loginApi = {
    login: async(data: LoginFormInput): Promise<string> => {

        const response = await api.post('https://localhost:7026/login', data, {
            headers: {
                'Content-Type': 'application/json',
            },
        });

        // console.log(response.data['accessToken'])
        localStorage.setItem('token', response.data['accessToken']);
        return response.data['accessToken'];
    }}

export default loginApi;