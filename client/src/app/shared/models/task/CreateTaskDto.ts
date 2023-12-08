import { Time } from "@angular/common"
import { TagDto } from "../tag/TagDto"

export interface CreateTaskDto {
    name: string
    dueDate: string
    notes: string
    durationTime: number
    dueTime: string
    priority: number
    energy: number
    projectId: number
    categoryId: number
    tags: number[]
}