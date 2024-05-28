import { useNavigate } from "react-router-dom";
import { useQueryClient } from "@tanstack/react-query";
import { logout as performLogout } from "@/utils/auth";

const useLogout = () => {
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const logout = () => {
        
        performLogout();

        queryClient.invalidateQueries();
        
        navigate('/');
    }
    return logout;
};

export default useLogout;