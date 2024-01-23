import { CategorySettings } from "../category/CategoryDto";

export interface User {
    id: string;
    userName: string;
    email: string;
    token: string;
    settings: UserSettings;
    hasPassword: boolean;
}

export interface UserSettings {
    theme: string;
    startPage: string;
    language: string;
    timeFormat: string;
    dateFormat: string;
    inboxSettings: CategorySettings;
    todaySettings: CategorySettings;
    nextActionsSettings: CategorySettings;
    waitingForSettings: CategorySettings;
    somedaySettings: CategorySettings;
}

export const UserSettingsEnum = {
    INBOX: {categoryName: 'Inbox', settingsName: 'inboxSettings'},
    TODAY: {categoryName: 'Today', settingsName: 'todaySettings'},
    SOMEDAY: {categoryName: 'Someday/Maybe', settingsName: 'somedaySettings'},
    WAITINGFOR: {categoryName: 'Waiting For', settingsName: 'waitingForSettings'},
    NEXTACTIONS: {categoryName: 'Next Actions', settingsName: 'nextActionsSettings'}
}


