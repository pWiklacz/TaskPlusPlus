export interface CalendarTaskDto {
    id: number
    name: string
    dueDate: Date
    isCompleted: boolean
    projectId: number
    categoryId: number
    categoryColor: string
}