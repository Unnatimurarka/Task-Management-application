-- Hash for 'admin123' (SHA256 Base64)
INSERT INTO USERS (username, password_hash) VALUES ('admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=');
INSERT INTO TASKS (title, description, status, user_id) VALUES ('Review Architecture', 'Check the README.md and documentation folder.', 'completed', 1);
INSERT INTO TASKS (title, description, status, user_id) VALUES ('Test Docker Setup', 'Ensure docker-compose up --build runs properly.', 'pending', 1);
COMMIT;
