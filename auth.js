import { request } from './api.js';
export async function login(username, password) {
    const res = await request('/auth/login', {
        method: 'POST',
        body: JSON.stringify({ username, password })
    });
    localStorage.setItem('token', res.token);
}
export async function register(username, password) {
    const res = await request('/auth/register', {
        method: 'POST',
        body: JSON.stringify({ username, password })
    });
    localStorage.setItem('token', res.token);
}
