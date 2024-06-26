// import { useState } from "react";
// import AuthContext from "./AuthContext";
// import { UserInfoResponse } from "@/api/userApi";

// interface AuthContextWrapperProps
// {
//     children: React.ReactNode;
// }

// const AuthContextWrapper: React.FC<AuthContextWrapperProps> = ( { children } ) =>
// {

//     //TODO: Store the cookie and access token expiration dates and invalidate the auth state
//     // if any of the two has expired.

//     const isLoggedLocalStorage = localStorage.getItem( 'isLogged' ) === "true";
//     const idLocalStorage = localStorage.getItem( 'id' );
//     const roleLocalStorage = localStorage.getItem( 'role' );
//     const userNameLocalStorage = localStorage.getItem( 'userName' );
//     const userTypeLocalStorage = localStorage.getItem( 'userType' );

//     const [ isLogged, setLogged ] = useState( isLoggedLocalStorage );
//     const [ id, setId ] = useState( idLocalStorage );
//     const [ role, setRole ] = useState( roleLocalStorage );
//     const [ userName, setUserName ] = useState( userNameLocalStorage );
//     const [ userType, setUserType ] = useState( userTypeLocalStorage );

//     const login = ( userData: UserInfoResponse ) =>
//     {
//         if ( !isLogged )
//         {
//             localStorage.setItem( "isLogged", "true" );
//             localStorage.setItem( 'id', userData.id );
//             localStorage.setItem( "role", userData.role );
//             localStorage.setItem( "userName", userData.userName );
//             localStorage.setItem( 'userType', userData.userType );

//             setLogged( true );
//             setId( userData.id );
//             setRole( userData.role );
//             setUserName( userData.userName );
//             setUserType( userData.userType );
//         }
//     };

//     const logout = () =>
//     {
//         localStorage.clear();

//         setLogged( false );
//         setId( null );
//         setRole( null );
//         setUserName( null );
//         setUserType( null );

//     };

//     return <AuthContext.Provider value={ { isLogged, id, userName, role, userType, login, logout } }>
//         { children }
//     </AuthContext.Provider>;
// };

// export default AuthContextWrapper;

import React, { useState, useEffect } from "react";
import AuthContext from "./AuthContext";
import { UserInfoResponse } from "@/api/userApi";

interface AuthContextWrapperProps
{
    children: React.ReactNode;
}

const AuthContextWrapper: React.FC<AuthContextWrapperProps> = ( { children } ) =>
{
    const isLoggedLocalStorage = localStorage.getItem( 'isLogged' ) === "true";
    const idLocalStorage = localStorage.getItem( 'id' );
    const roleLocalStorage = localStorage.getItem( 'role' );
    const userNameLocalStorage = localStorage.getItem( 'userName' );
    const userTypeLocalStorage = localStorage.getItem( 'userType' );
    const tokenExpiryLocalStorage = localStorage.getItem( 'tokenExpiry' );
    const cookieExpiryLocalStorage = localStorage.getItem( 'cookieExpiry' );

    const [ isLogged, setLogged ] = useState( isLoggedLocalStorage );
    const [ id, setId ] = useState( idLocalStorage );
    const [ role, setRole ] = useState( roleLocalStorage );
    const [ userName, setUserName ] = useState( userNameLocalStorage );
    const [ userType, setUserType ] = useState( userTypeLocalStorage );

    useEffect( () =>
    {
        const now = new Date();

        if ( tokenExpiryLocalStorage && new Date( tokenExpiryLocalStorage ) < now )
        {
            logout();
        } else if ( cookieExpiryLocalStorage && new Date( cookieExpiryLocalStorage ) < now )
        {
            logout();
        }
    }, [] );

    const login = ( userData: UserInfoResponse) =>
    {
        if ( !isLogged )
        {
            localStorage.setItem( "isLogged", "true" );
            localStorage.setItem( 'id', userData.id );
            localStorage.setItem( "role", userData.role );
            localStorage.setItem( "userName", userData.userName );
            localStorage.setItem( 'userType', userData.userType );
            localStorage.setItem( 'tokenExpiry', userData.cookieExpiry.toString() );
            localStorage.setItem( 'cookieExpiry', userData.cookieExpiry.toString() );

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
