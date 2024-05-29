// import axios from "axios";
// import { API_BASE_URL } from "@/common/constants";

// const apiPrivate = axios.create( {
//     baseURL: API_BASE_URL,
//     // headers: {'Content-Type': 'application/json'},
//     withCredentials: true
// } );

// const privateRequestIntercept = apiPrivate.interceptors.request.use(
//     ( config ) =>
//     {
//         const token = localStorage.getItem( 'token' );
//         if ( token )
//         {
//             config.headers[ 'Authorization' ] = `Bearer ${ token }`;
//         }
//         return config;
//     },
//     ( error ) =>
//     {
//         return Promise.reject( error );
//     }
// );

// // const privateResponseIntercept = apiPrivate.interceptors.response.use(
// //     response => response,
// //     async ( error ) =>
// //     {
// //         const prevRequest = error?.config;
// //         if ( error?.response?.status === 403 && !prevRequest?.sent )
// //         {
// //             prevRequest.sent = true;
// //             const newAccessToken = await refresh();
// //             prevRequest.headers[ 'Authoriztion' ] = `Bearer ${ newAccessToken }`;
// //             return apiPrivate( prevRequest );
// //         }
// //     }
// // );

// // return () =>
// // {
// //     apiPrivate.interceptors.request.eject( privateRequestIntercept );
// //     apiPrivate.interceptors.response.eject( privateResponseIntercept );
// // }

// // return apiPrivate;


// export {apiPrivate};
