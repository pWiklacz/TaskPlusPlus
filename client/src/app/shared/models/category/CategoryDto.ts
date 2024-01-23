export interface CategoryDto {
    id: number;
    name: string;
    isFavorite: boolean;
    isImmutable: boolean;
    colorHex: string;
    icon: string;
    settings: CategorySettings;
}

export interface CategorySettings {
    grouping: string;
    sorting: string;
    direction: boolean;
}

export const InboxId: number = 1;
export const NextActionsId: number = 2;
export const SomedayMaybeId: number = 6;
export const WaitingForId: number = 5;