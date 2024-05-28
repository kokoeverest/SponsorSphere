import { useEffect, useState } from "react";
import { getUserRole } from "@/api/userApi";

interface User {
    role: 'Admin' | 'Athlete' | 'Sponsor' | null;
}

export const useAuth = () => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const fetchUserRole = async () => {

            try {
                const role = await getUserRole();
                setIsAuthenticated(true);
                setUser({ role });
            } catch (error) {
                console.error(error);
                setUser({ role: null });
            } finally {
                setLoading(false);
            }
        };

        fetchUserRole();
    }, []);

    return { user, loading, isAuthenticated };
};