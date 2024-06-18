export interface UpdateAthleteProfileFormInput
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
    lastName: string;
    birthDate: Date;
    sport: string;
}