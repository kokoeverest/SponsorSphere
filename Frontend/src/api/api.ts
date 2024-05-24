import axios from "axios";
import { API_BASE_URL } from "@/common/constants";

const api = axios.create({
    baseURL: API_BASE_URL,
});

api.interceptors.response.use((response) => response, (error) => {
    switch (error.response.status) {
        case 404:
            console.log("Not found response!");
            alert(error.response.data);
            break;
        default: console.log(error.response.data);
    }

    return Promise.reject(error);
});

export { api };