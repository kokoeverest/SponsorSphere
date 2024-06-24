import { BlogPostDto } from "./blogPost";
import { SponsorshipDto } from "./sponsorship";

export interface UserDto {
    id: number;
    name: string;
    email: string;
    country: string;
    phoneNumber: string;
    created: Date;
    pictureId: number | null;
    website: string;
    faceBookLink: string;
    instagramLink: string;
    twitterLink: string;
    stravaLink: string;
    blogPosts: BlogPostDto[];
    sponsorships: SponsorshipDto[];
  }