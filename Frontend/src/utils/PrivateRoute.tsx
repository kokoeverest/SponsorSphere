import { Outlet, Navigate } from "react-router-dom";
import { useContext } from "react";

import AuthContext from "@/context/AuthContext";

const PrivateRoute: React.FC = (): JSX.Element => {
    const authData = useContext(AuthContext);
    return authData.isLogged ? <Outlet /> : <Navigate to="/login" />;
};

export default PrivateRoute;