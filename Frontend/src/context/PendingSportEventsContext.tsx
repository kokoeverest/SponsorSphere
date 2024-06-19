import React, { createContext, useContext, useState, useEffect } from 'react';
import sportEventApi from '@/api/sportEventApi';

interface PendingSportEventsContextType
{
    count: number;
    fetchPendingSportEventsCount: () => void;
}

const PendingSportEventsContext = createContext<PendingSportEventsContextType | undefined>( undefined );

export const usePendingSportEvents = () =>
{
    const context = useContext( PendingSportEventsContext );
    if ( !context )
    {
        throw new Error( 'usePendingSportEvents must be used within a PendingSportEventsProvider' );
    }
    return context;
};

export const PendingSportEventsProvider: React.FC<{ children: React.ReactNode; }> = ( { children } ) =>
{
    const [ count, setCount ] = useState<number>( 0 );

    const fetchPendingSportEventsCount = async () =>
    {
        try
        {
            const count = await sportEventApi.getPendingSportEventsCount();
            setCount( count );
        } catch ( error )
        {
            console.error( error );
        }
    };

    useEffect( () =>
    {
        fetchPendingSportEventsCount();
    }, [] );

    return (
        <PendingSportEventsContext.Provider value={ { count, fetchPendingSportEventsCount } }>
            { children }
        </PendingSportEventsContext.Provider>
    );
};
