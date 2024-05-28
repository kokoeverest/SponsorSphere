import { useEffect, useState } from "react";

interface User {
    role: 'admin' | 'athlete' | 'sponsor' | null;
}

export const useAuth = () => {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        const token = localStorage.getItem('token');

        if (token) {
            const userRole = localStorage.getItem('userRole');
            setUser({ role: userRole as User['role']});
        } else {
            setUser({ role: null});
        }
    }, []);

    return user;

}