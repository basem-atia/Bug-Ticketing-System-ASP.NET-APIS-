# 🐞 Bug Ticketing System APIs

Welcome! 👋  
This is a **Bug Ticketing Web API** built using **ASP.NET Core APIs**.

The Bug Ticketing System is a web application that helps teams manage bugs and issues in software projects. It enables users (Managers, Developers, and Testers) to track, report, and resolve bugs effectively. The system allows users to create, view, and manage bugs, handle user accounts, and manage attachments related to bugs.

> Whether you're here to run it, develop it, or just understand how it works — this README will guide you step by step.

---

## 📚 Table of Contents

- [🚧 Project Overview](#🚧-project-overview)
- [🖥️ API Modules](#️🖥️-api-modules)
- [📤 Request Models](#📤-request-models)
- [🎛️ Response Structure](#️🎛️-response-structure)
- [🚀 How to Run the Project](#🚀-how-to-run-the-project)

---

## 🚧 Project Overview

This API allows you to:

- Register users and admins
- Login and generate token
- Create projects
- Report bugs under specific projects
- Assign bugs to users
- Upload and view bug attachments
- Assign attachments to bugs

All endpoints return a consistent response format (**General Result**) and are documented using **Scalar**.

---

## 🖥️ API Modules

### 🧑 Users

| Method | Endpoint                    | Purpose                       |
| ------ | --------------------------- | ----------------------------- |
| GET    | `/api/Users`                | Get all users                 |
| GET    | `/api/Users/{email}`        | Get user by email             |
| POST   | `/api/Users/register`       | Register user                 |
| POST   | `/api/Users/register/admin` | Register admin                |
| POST   | `/api/Users/login`          | Login and get token with role |

### 📁 Projects

| Method | Endpoint             | Purpose           |
| ------ | -------------------- | ----------------- |
| GET    | `/api/Projects`      | Get all projects  |
| POST   | `/api/Projects`      | Add a new project |
| GET    | `/api/Projects/{id}` | Get project by ID |

### 🐞 Bugs

| Method | Endpoint                                    | Purpose                 |
| ------ | ------------------------------------------- | ----------------------- |
| GET    | `/api/Bugs`                                 | Get all bugs            |
| POST   | `/api/Bugs`                                 | Report a new bug        |
| GET    | `/api/Bugs/{id}`                            | Get bug details         |
| POST   | `/api/Bugs/{id}/assignees`                  | Assign user to bug      |
| DELETE | `/api/Bugs/{id}/assignees/{userId}`         | Unassign user from bug  |
| POST   | `/api/Bugs/{id}/attachments`                | Upload file to a bug    |
| GET    | `/api/Bugs/{id}/attachments`                | List bug attachments    |
| DELETE | `/api/Bugs/{id}/attachments/{attachmentId}` | Delete a bug attachment |

---

## 📤 Request Models

> ⚠️ **Note**: All routes require a **Bearer token** in the header.  
> 🚨 Removing a user from a bug requires **Admin privileges**.

### 🧑 User Model

**Register**

- `POST https://localhost:7151/api/Users/register`

```json
{
  "fullName": "",
  "emailAddress": "",
  "password": ""
}
```

**Admin Register Dto**

- `https://localhost:7151/api/Users/register/admin`

```json
{
  "fullName": "",
  "emailAddress": "",
  "password": ""
}
```

**Login Dto**

- `https://localhost:7151/api/Users/login`

```json
{
  "emailAddress": "",
  "password": ""
}
```

### 📁 Projects Model

**Add Project**

- `https://localhost:7151/api/Projects`

```json
{
  "name": "",
  "description": ""
}
```

### 🐞 Bugs Model

**Add Bug**

- `https://localhost:7151/api/Bugs`

```json
{
  "title": "",
  "description": "",
  "status": "",
  "projectId": ""
}
```

**Assign Bug to User**

- `https://localhost:7151/api/Bugs/{id}/assignees`

```json
{
  "userId": ""
}
```

**Upload an attachment**

- `POST https://localhost:7151/api/Bugs/{id}/attachments`
- Body Type: `multipart/form-data`
- Key: `file` → Upload your file

---

## 🎛️ Response Structure

```json
{
  "success": true,
  "errors": [
    {
      "code": "string",
      "message": "string",
      "propertyName?": "string",
      "attemptedValue?": "string"
    }
  ],
  "data": "any"
}
```

## 🚀 How to Run the Project

### 1. Clone the repository

- git clone https://github.com/your-username/bug-ticketing-system.git
