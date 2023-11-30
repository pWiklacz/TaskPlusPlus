import { Time } from "@angular/common"
import { TagDto } from "./TagDto"

export interface TaskDto {
    id: number
    name: string
    dueDate: Date
    notes: string 
    isCompleted: boolean
    durationTime: Time
    priority: number
    energy: number
    projectId: number
    categoryId: number
    completedOnUtc: Date
    tags: TagDto[]
}