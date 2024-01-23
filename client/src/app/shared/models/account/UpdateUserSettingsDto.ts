import { UserSettings } from "./user";

export interface UpdateUserSettingsDto {
    UserId: string;
    settings: UserSettings;
}