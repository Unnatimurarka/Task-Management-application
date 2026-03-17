import { request } from './api.js';
import { AuthResponse } from './types.js';

export async function login(username: string, password: string): Promise<void> {
    const res = await request<AuthResponse>('/auth/login', {
        method: 'POST',
        body: JSON.stringify({ username, password })
    });
    localStorage.setItem('token', res.token);
}

export async function register(username: string, password: string): Promise<void> {
    const res = await request<AuthResponse>('/auth/register', {
        method: 'POST',
        body: JSON.stringify({ username, password })
    });
    localStorage.setItem('token', res.token);
}
