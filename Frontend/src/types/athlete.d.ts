import { UserDto } from './user';
import { AchievementDto } from './achievement';
import { GoalDto } from './goal';

export interface AthleteDto extends UserDto {
    age: number;
    lastName: string;
    birthDate: Date;
    sport: string;
    achievements: AchievementDto[];
    goals: GoalDto[];
  }