export interface UpdatePasswordDto {
    userId: string;
    newPassword: string;
    currentPassword: string;
}