import { UserDto } from './user';

export interface SponsorIndividualDto extends UserDto {
    age: number;
    lastName: string;
    birthDate: string;
}