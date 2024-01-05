import { TaskDto } from "../task/TaskDto"

export interface ProjectDto {
    id: number
    name: string
    dueDate: string
    notes: string
    isCompleted: boolean
    dueTime: string
    completedOnUtc: Date | null
    tasks: TaskDto[]
}