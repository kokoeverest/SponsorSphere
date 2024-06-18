export interface UpdateSponsorCompanyProfileFormInput
{
    id: number;
    name: string;
    email: string;
    country: string;
    phoneNumber: string;
    created: Date;
    pictureId: File | GetPictureDto | null;
    website: string | null;
    faceBookLink: string | null;
    instagramLink: string | null;
    twitterLink: string | null;
    stravaLink: string | null;
    iban: number;
}