const BASE_URL = '/api';

function getHeaders() {
    const token = localStorage.getItem('token');
    return {
        'Content-Type': 'application/json',
        ...(token ? { 'Authorization': `Bearer ${token}` } : {})
    };
}

export async function request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
    const res = await fetch(`${BASE_URL}${endpoint}`, {
        ...options,
        headers: getHeaders()
    });

    if (res.status === 401) {
        localStorage.removeItem('token');
        window.location.reload();
    }

    if (!res.ok && res.status !== 204) {
        const err = await res.json().catch(() => ({}));
        throw new Error(err.message || 'API Error');
    }

    return (res.status !== 204 ? res.json() : null) as Promise<T>;
}
