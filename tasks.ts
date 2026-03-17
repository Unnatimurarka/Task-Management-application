import { request } from './api.js';
import { TaskItem } from './types.js';

export async function fetchTasks(): Promise<TaskItem[]> {
    return request<TaskItem[]>('/tasks');
}

export async function createTask(title: string, description: string): Promise<TaskItem> {
    return request<TaskItem>('/tasks', {
        method: 'POST',
        body: JSON.stringify({ title, description })
    });
}

export async function updateTaskStatus(id: number, task: TaskItem, newStatus: string): Promise<void> {
    await request(`/tasks/${id}`, {
        method: 'PUT',
        body: JSON.stringify({ title: task.title, description: task.description, status: newStatus })
    });
}

export async function deleteTask(id: number): Promise<void> {
    await request(`/tasks/${id}`, { method: 'DELETE' });
}
