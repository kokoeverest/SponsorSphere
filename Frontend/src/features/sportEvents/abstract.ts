export interface CreateSportEventFormInput
{
    name: string;
    country: string;
    eventDate: Date;
    eventType: string;
    sport: string;
}

export interface UpdateSportEventFormInput
{
    id: number;
    name: string;
    country: string;
    eventDate: Date;
    eventType: string;
    sport: string;
    status: string;
}
