import { useState } from "react";
import AuthContext from "./AuthContext";
import { UserInfoResponse } from "@/api/userApi";

interface AuthContextWrapperProps
{
    children: React.ReactNode;
}

const AuthContextWrapper: React.FC<AuthContextWrapperProps> = ( { children } ) =>
{

    //TODO: Store the cookie and access token expiration dates and invalidate the auth state
    // if any of the two has expired.

    const isLoggedLocalStorage = localStorage.getItem( 'isLogged' ) === "true";
    const userNameLocalStorage = localStorage.getItem( 'userName' );
    const roleLocalStorage = localStorage.getItem( 'role' );
    const idLocalStorage = localStorage.getItem( 'id' );

    const [ isLogged, setLogged ] = useState( isLoggedLocalStorage );
    const [ userName, setUserName ] = useState( userNameLocalStorage );
    const [ role, setRole ] = useState( roleLocalStorage );
    const [ id, setId ] = useState( idLocalStorage );

    const login = ( userData: UserInfoResponse ) =>
    {
        if ( !isLogged )
        {
            localStorage.setItem( "isLogged", "true" );
            localStorage.setItem( "userName", userData.userName );
            localStorage.setItem( "role", userData.role );
            localStorage.setItem( 'id', userData.id );

            setLogged( true );
            setUserName( userData.userName );
            setRole( userData.role );
            setId( userData.id );
        }
    };

    const logout = () =>
    {
        localStorage.clear();

        setLogged( false );
        setUserName( null );
        setRole( null );
        setId( null );
    };

    return <AuthContext.Provider value={ { isLogged, id, userName, role, login, logout } }>
        { children }
    </AuthContext.Provider>;
};

export default AuthContextWrapper;