import { GroupingOptionsEnum } from "./GroupingOptionsEnum";
import { SortingOptionsEnum } from "./SortingOptionsEnum";

export class GetTasksQueryParams {
    categoryId = 0;
    sortDescending = false;
    sortBy = SortingOptionsEnum.NAME;
    groupBy = GroupingOptionsEnum.NONE;
    search = '';
    date = '';
}
