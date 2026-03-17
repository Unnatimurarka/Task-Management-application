# Task Manager Application

A production-grade, containerized full-stack web application built to demonstrate clean architecture, REST APIs, and Docker orchestration.

## Technologies
- **Frontend**: TypeScript, HTML5, CSS3
- **Backend**: C# 12, ASP.NET Core 8 Web API, Dapper ORM
- **Database**: Oracle Database 21c Express Edition
- **Infrastructure**: Nginx, Docker, Docker Compose

## Architecture Diagram (Simplified)
```text
           +-------------+
           |   Browser   |
           +------+------+
                  | (Port 8080)
           +------v------+
           |    Nginx    +----(Serve Static)----> Frontend Code
           +------+------+
                  | (/api proxy)
           +------v------+
           | .NET 8 API  |
           +------+------+
                  | (Dapper / TCP 1521)
           +------v------+
           |  Oracle DB  |
           +-------------+
```

## Setup Instructions

1. **Prerequisites**: Ensure Docker and Docker Compose are installed.
2. **Deployment**:
   Navigate to the `infrastructure/docker` directory and run:
   ```bash
   docker-compose up --build -d
   ```
   *Note: The Oracle Database container takes several minutes to fully initialize. The API container will wait until the database is healthy before starting.*
3. **Access**: 
   Open a browser to `http://localhost:8080`

### Test Credentials
The database seeds a default user automatically:
- **Username**: `admin`
- **Password**: `admin123`
