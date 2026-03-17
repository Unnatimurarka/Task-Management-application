import { request } from './api.js';
export async function fetchTasks() {
    return request('/tasks');
}
export async function createTask(title, description) {
    return request('/tasks', {
        method: 'POST',
        body: JSON.stringify({ title, description })
    });
}
export async function updateTaskStatus(id, task, newStatus) {
    await request(`/tasks/${id}`, {
        method: 'PUT',
        body: JSON.stringify({ title: task.title, description: task.description, status: newStatus })
    });
}
export async function deleteTask(id) {
    await request(`/tasks/${id}`, { method: 'DELETE' });
}
