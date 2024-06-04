import { UserDto } from './user';

export interface SponsorCompanyDto extends UserDto {
    iban: string;
}