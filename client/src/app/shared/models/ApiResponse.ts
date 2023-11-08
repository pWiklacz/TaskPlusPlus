export interface ApiResponse<T>
{
    statusCode: string;
    message: string;
    value: T;
}