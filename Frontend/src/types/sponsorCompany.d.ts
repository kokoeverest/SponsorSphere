import { SponsorDto } from './sponsor';

export interface SponsorCompanyDto extends SponsorDto {
    iban: string;
}