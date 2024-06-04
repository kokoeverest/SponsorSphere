import { useEffect, useState } from "react";
import { getUserInfo } from "@/api/userApi";

interface User {
    id: string | null;
    role: string | null;
    userName: string | null;
}

export const useAuth = () => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const fetchUserRole = async () => {

            try {
                const userInfo = await getUserInfo();
                if (userInfo.role === 'undefined'){
                    throw new Error("No user role available");
                }
                setIsAuthenticated(true);
                setUser({ id: userInfo.id, role: userInfo.role, userName: userInfo.userName });
            } catch (error) {
                console.error(error);
                setUser({ id: null, role: null, userName: null });
            } finally {
                setLoading(false);
            }
        };

        fetchUserRole();
    }, []);

    return { user, loading, isAuthenticated };
};