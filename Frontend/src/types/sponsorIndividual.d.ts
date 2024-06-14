import { SponsorDto } from './sponsor';

export interface SponsorIndividualDto extends SponsorDto {
    age: number;
    lastName: string;
    birthDate: Date;
}