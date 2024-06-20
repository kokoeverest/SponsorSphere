import pictureApi from "@/api/pictureApi";

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

export const fetchPicture = async ( pictureId: number ): Promise<string> =>
{
    try
    {
        const picture = await pictureApi.getPictureById( pictureId );
        return picture.content;
    } catch ( error )
    {
        console.error( `Failed to fetch picture with id ${ pictureId }`, error );
        return '';
    }
};


export const urlBuilder = ( id: string, role: string, userType: string ): string =>
{
    switch ( userType )
    {
        case 'SponsorCompany':
            return `/${ role.toLowerCase() }s/companies/${ id }`;
        case 'SponsorIndividual':
            return `/${ role.toLowerCase() }s/individuals/${ id }`;
        case 'Athlete':
            return `/${ role.toLowerCase() }s/${ id }`;
        default:
            return '';
    }
};