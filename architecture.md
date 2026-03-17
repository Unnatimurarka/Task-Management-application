# System Architecture

## Overview
The Task Manager application uses a modern multi-tier architecture deployed via Docker.

## Components
1. **Nginx Reverse Proxy**: Single entry point. Routes `/api/*` to ASP.NET Core API and `/` to static compiled frontend files.
2. **TypeScript Frontend**: Vanilla TS compiled to ES2020 via `tsc`. Uses Fetch API for REST requests. 
3. **ASP.NET Core API**: Implements Domain-driven design structure (Core + API layers) using strict DTOs. Uses Dapper as a lightweight ORM for database connection.
4. **Oracle Database XE**: Containerized relational DB storing Users and Tasks. Initialized using startup scripts.

## Data Flow
Browser -> (HTTP 8080) -> Nginx
Nginx -> (Static HTML/JS/CSS) -> Browser
Browser -> (Fetch API) -> Nginx `/api` -> Backend Container
Backend Container -> (Dapper / Oracle.ManagedDataAccess) -> Oracle DB Container (port 1521)
