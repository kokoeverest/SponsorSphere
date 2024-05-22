// src/types/athlete.d.ts
import { UserDto } from './user';
import { AchievementDto } from './achievement';

export interface AthleteDto extends UserDto {
    age: number;
    lastName: string;
    birthDate: string;
    sport: string;
    achievements: AchievementDto[];
    goals: GoalDto[];
  }