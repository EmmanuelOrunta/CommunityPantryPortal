# Community Pantry Portal

A responsive, data-driven ASP.NET Core MVC web application that helps people find nearby food/community pantries, request assistance, and allows organizations to register pantry services.

---
## 📌 Project Overview

**Community Pantry Portal** was developed for **ITEC634 – Web and Mobile Application Development**.

The platform is designed to:
- Connect people in need with local pantry services
- Simplify help request submission
- Allow charities/churches/community groups to register pantries
- Support mobile-first, accessible, responsive web usage across devices


---
## 🎯 Assignment Requirements Covered

This project implements all core requirements:

- ✅ Dynamic website using **ASP.NET Core MVC**
- ✅ Data-driven operations using **Entity Framework Core + SQLite**
- ✅ **User input validation** (Data Annotations + ModelState)
- ✅ **Error handling** (validation feedback + safe controller flow)
- ✅ **Server-side authentication** with **ASP.NET Identity**
- ✅ **Responsive design** (desktop + mobile layouts)
- ✅ Data display and data updating operations
- ✅ Report + screenshots + presentation support

---

## ✨ Key Features

### 1) Find a Pantry
- Search by suburb/city/keywords
- Filter pantry records
- View pantry cards with details
- Map embed for location awareness

### 2) Register a Pantry
- Authenticated users can submit new pantry records
- Form includes type, address, suburb, state, contact, and description
- Form validation with clear user feedback

### 3) Request Assistance
- Guided request form for individuals/families
- Users select pantry + request type + notes + contact email
- `Status` and `CreatedAt` are set server-side

### 4) Authentication & Authorization
- Register / Login / Logout using ASP.NET Identity
- Protected actions use `[Authorize]`

### 5) About Us
- Mission, values, impact, and contact information
- Explains social purpose of the platform

### 6) Responsive Mobile UX
- Mobile-friendly layout
- Hamburger menu for iPhone/small screens
- Touch-friendly buttons and spacing

---

## 🧱 Tech Stack

- **Backend:** ASP.NET Core MVC (.NET)
- **Database:** SQLite
- **ORM:** Entity Framework Core
- **Auth:** ASP.NET Core Identity
- **Frontend:** Razor Views + CSS + Bootstrap utilities
- **Tools:** VS Code, .NET CLI

---

## 📂 Project Structure

```text
CommunityPantryPortal/
├── Controllers/
│   ├── HomeController.cs
│   ├── PantriesController.cs
│   └── HelpRequestsController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Models/
│   ├── Pantry.cs
│   └── HelpRequest.cs
├── Views/
│   ├── Home/
│   │   └── About.cshtml
│   ├── Pantries/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Details.cshtml
│   ├── HelpRequests/
│   │   ├── Index.cshtml
│   │   └── Create.cshtml
│   └── Shared/
