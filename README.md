# Music Store Application

## Overview
Music Store is a fully functional online vinyl shop built with **.NET 8**, following the principles of **Onion Architecture** with clearly separated layers: Domain, Application, Infrastructure, and Presentation.

The application allows users to browse and purchase vinyl albums, manage artists, genres, and record labels. It includes models such as `Album`, `Artist`, `Genre`, `Label`, `ShoppingCart`, and `Order`. External data is fetched via DTOs using real-time API calls.

> ⚠️ **Note:** This project is not intended for production use. It was developed as a learning exercise focused on exploring Azure cloud deployment, API integrations, and enterprise-level architecture using .NET 8.

## Key Features
- 🔗 **Discogs API Integration** – Fetches real-time data for vinyl albums (info, prices, ratings)
- 💳 **Stripe Payments** – Secure checkout and payment processing
- 📧 **Email Notifications** – Confirmation emails sent after successful orders
- 🧱 **Onion Architecture** – Clean separation of concerns and scalable project structure
- ☁️ **Azure Hosting** – Deployed on Azure App Service and Azure SQL Database

## Technologies Used
- ASP.NET Core 8  
- Entity Framework Core  
- Azure App Service & Azure SQL Database  
- Discogs REST API  
- Stripe API  
- SMTP Email Service  
- DTO Mapping, Dependency Injection, Async Programming

## Architecture Layers
- **Domain** – Core business models and logic  
- **Application** – Interfaces and application-level services  
- **Infrastructure** – Database and external service implementations  
- **Presentation** – Web application using ASP.NET MVC

---

**Author:** Dimitar Iliev  
