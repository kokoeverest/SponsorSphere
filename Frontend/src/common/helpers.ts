export const formatDate = ( date: any ): string =>
{
    if ( !( date instanceof Date ) )
    {
        date = new Date( date );
    }

    const year = date.getFullYear();
    const month = ( '0' + ( date.getMonth() + 1 ) ).slice( -2 );
    const day = ( '0' + date.getDate() ).slice( -2 );
    return `${ year }-${ month }-${ day }`;
};