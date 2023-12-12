import { Time } from "@angular/common"
import { TagDto } from "../tag/TagDto"

export interface TaskDto {
    id: number
    name: string
    dueDate: Date
    notes: string 
    isCompleted: boolean
    dueTime: Time
    durationTime: number
    priority: number
    energy: number
    projectId: number
    categoryId: number
    completedOnUtc: Date | null
    tags: TagDto[]
}