import { LoginFormInput } from "@/features/login/abstract";
import { api } from "./api";


const loginApi = {
    login: async ( data: LoginFormInput ) =>
    {
        await api.post( 'login?useCookies=true', data, {
            headers: {
                'Content-Type': 'application/json',
            }
        } );
    }
};

export default loginApi;