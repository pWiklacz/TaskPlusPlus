import { CategorySettings } from "./CategoryDto";

export interface UpdateCategorySettingsDto {
    id: number,
    settings: CategorySettings
}