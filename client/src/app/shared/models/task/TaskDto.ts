import { Time } from "@angular/common"
import { TagDto } from "../tag/TagDto"

export interface TaskDto {
    id: number
    name: string
    dueDate: string
    notes: string 
    isCompleted: boolean
    dueTime: string
    durationTime: number
    priority: number
    energy: number
    projectId: number
    categoryId: number
    completedOnUtc: Date | null
    tags: TagDto[]
}