export interface CreateCategoryDto {
    name: string;
    isFavorite: boolean;
    colorHex: string;
    icon: string;
}

export interface CategoryDto {
    id: number;
    name: string;
    isFavorite: boolean;
    colorHex: string;
    icon: string;
}

export const InboxId: number = 1;
export const NextActionsId: number = 2;
export const SomedayMaybeId: number = 6;
export const WaitingForId: number = 5;