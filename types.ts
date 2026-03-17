export interface AuthResponse {
    token: string;
    username: string;
}
export interface TaskItem {
    id: number;
    title: string;
    description: string;
    status: string;
    createdAt: string;
}
