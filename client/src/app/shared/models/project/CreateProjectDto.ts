import { TaskDto } from "../task/TaskDto"

export interface CreateProjectDto {
    name: string
    dueDate: string
    notes: string
    dueTime: string
}