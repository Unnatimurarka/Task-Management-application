import { login, register } from './auth.js';
import { fetchTasks, createTask, updateTaskStatus, deleteTask } from './tasks.js';
// DOM Elements
const views = {
    auth: document.getElementById('auth-view'),
    dashboard: document.getElementById('dashboard-view')
};
const authConfig = {
    isLogin: true,
    form: document.getElementById('auth-form'),
    title: document.getElementById('auth-title'),
    submit: document.getElementById('auth-submit'),
    toggleMsg: document.getElementById('auth-toggle-msg'),
    toggleBtn: document.getElementById('auth-toggle-btn')
};
const dashboardConfig = {
    logoutBtn: document.getElementById('logout-btn'),
    taskForm: document.getElementById('task-form'),
    taskList: document.getElementById('task-list')
};
// Routing logic
function renderView() {
    const hasToken = !!localStorage.getItem('token');
    views.auth.classList.toggle('hidden', hasToken);
    views.dashboard.classList.toggle('hidden', !hasToken);
    dashboardConfig.logoutBtn.classList.toggle('hidden', !hasToken);
    if (hasToken)
        loadAndRenderTasks();
}
// Auth Handlers
authConfig.toggleBtn.addEventListener('click', (e) => {
    e.preventDefault();
    authConfig.isLogin = !authConfig.isLogin;
    authConfig.title.textContent = authConfig.isLogin ? 'Login' : 'Register';
    authConfig.submit.textContent = authConfig.isLogin ? 'Login' : 'Register';
    authConfig.toggleMsg.textContent = authConfig.isLogin ? "Don't have an account?" : "Already have an account?";
    authConfig.toggleBtn.textContent = authConfig.isLogin ? 'Register' : 'Login';
});
authConfig.form.addEventListener('submit', async (e) => {
    e.preventDefault();
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    try {
        if (authConfig.isLogin)
            await login(username, password);
        else {
            await register(username, password);
            alert('Registered! You are now logged in.');
        }
        renderView();
    }
    catch (err) {
        alert(err.message);
    }
});
dashboardConfig.logoutBtn.addEventListener('click', () => {
    localStorage.removeItem('token');
    renderView();
});
// Task Handlers
dashboardConfig.taskForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const title = document.getElementById('task-title').value;
    const desc = document.getElementById('task-desc').value;
    await createTask(title, desc);
    dashboardConfig.taskForm.reset();
    loadAndRenderTasks();
});
let currentTasks = [];
async function loadAndRenderTasks() {
    currentTasks = await fetchTasks();
    dashboardConfig.taskList.innerHTML = '';
    currentTasks.forEach(task => {
        const li = document.createElement('li');
        li.className = `task-item ${task.status === 'completed' ? 'completed' : ''}`;
        li.innerHTML = `
            <div class="task-info">
                <h3>${task.title}</h3>
                <p>${task.description || 'No description'}</p>
            </div>
            <div class="task-actions">
                ${task.status !== 'completed' ?
            `<button class="btn-small btn-success" data-action="complete" data-id="${task.id}">Done</button>` : ''}
                <button class="btn-small btn-danger" data-action="delete" data-id="${task.id}">Delete</button>
            </div>
        `;
        dashboardConfig.taskList.appendChild(li);
    });
}
// Event delegation for dynamically created buttons
dashboardConfig.taskList.addEventListener('click', async (e) => {
    const target = e.target;
    if (target.tagName !== 'BUTTON')
        return;
    const id = parseInt(target.getAttribute('data-id'));
    const action = target.getAttribute('data-action');
    if (action === 'delete' && confirm('Delete this task?')) {
        await deleteTask(id);
        loadAndRenderTasks();
    }
    else if (action === 'complete') {
        const task = currentTasks.find(t => t.id === id);
        await updateTaskStatus(id, task, 'completed');
        loadAndRenderTasks();
    }
});
// Init
renderView();
