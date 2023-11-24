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