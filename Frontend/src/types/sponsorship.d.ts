import { AthleteDto } from "./athlete";
import { SponsorCompanyDto } from "./sponsorCompany";
import { SponsorIndividualDto } from "./sponsorIndividual";

export interface SponsorshipDto {
    created: string;
    amount: number;
    athlete: AthleteDto;
    athleteId: number;
    sponsorId: number;
    sponsor: SponsorCompanyDto | SponsorIndividualDto;
}