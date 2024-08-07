import React, { useEffect, useState } from "react";
import AuthContext from "./AuthContext";
import { UserInfoResponse, getUserInfo } from "@/api/userApi";

interface AuthContextWrapperProps
{
    children: React.ReactNode;
}

const checkCookieValidity = async () =>
{
    try
    {
        const response = await getUserInfo();
        return response.id !== null;

    } catch ( error )
    {
        console.error( "Error checking cookie validity:", error );
        return false;
    }
};

const AuthContextWrapper: React.FC<AuthContextWrapperProps> = ( { children } ) =>
{
    const isLoggedLocalStorage = localStorage.getItem( 'isLogged' ) === "true";
    const idLocalStorage = localStorage.getItem( 'id' );
    const roleLocalStorage = localStorage.getItem( 'role' );
    const userNameLocalStorage = localStorage.getItem( 'userName' );
    const userTypeLocalStorage = localStorage.getItem( 'userType' );

    const [ isLogged, setLogged ] = useState( isLoggedLocalStorage );
    const [ id, setId ] = useState( idLocalStorage );
    const [ role, setRole ] = useState( roleLocalStorage );
    const [ userName, setUserName ] = useState( userNameLocalStorage );
    const [ userType, setUserType ] = useState( userTypeLocalStorage );

    useEffect( () =>
    {
        const checkValidity = async () =>
        {
            if ( isLogged )
            {
                console.log(`Is logged: ${isLogged}`)
                const isValid = await checkCookieValidity();
                console.log( `Is valid: ${ isValid}` )
                if ( !isValid )
                {
                    logout();
                }
            }
        };

        checkValidity();
    }, [] );

    const login = ( userData: UserInfoResponse ) =>
    {
        if ( !isLogged )
        {
            localStorage.setItem( "isLogged", "true" );
            localStorage.setItem( 'id', userData.id );
            localStorage.setItem( "role", userData.role );
            localStorage.setItem( "userName", userData.userName );
            localStorage.setItem( 'userType', userData.userType );

            setLogged( true );
            setId( userData.id );
            setRole( userData.role );
            setUserName( userData.userName );
            setUserType( userData.userType );
        }
    };

    const logout = () =>
    {
        localStorage.clear();

        setLogged( false );
        setId( null );
        setRole( null );
        setUserName( null );
        setUserType( null );
    };

    return (
        <AuthContext.Provider value={ { isLogged, id, userName, role, userType, login, logout } }>
            { children }
        </AuthContext.Provider>
    );
};

export default AuthContextWrapper;
