# TeleCAM - Real-time Messaging Application
*A project developed for the Web, Mobile and Cloud Applications course at University of Camerino*

TeleCAM is a web-based real-time messaging application, featuring a .NET backend API and a Svelte frontend. This project provides secure user authentication, real-time chat capabilities, contact management, and more. 

## Project Overview

TeleCAM consists of three main components:

- **API**: A .NET 8 backend providing REST endpoints and SignalR real-time communication
- **Client**: A Svelte 5 frontend with a responsive user interface
- **Domain**: Shared domain logic and entity definitions

## Technology Stack

### Backend
- .NET 8.0
- Entity Framework Core with PostgreSQL
- ASP.NET Core SignalR for real-time communication
- JWT-based authentication system
- Swagger documentation with SignalR support

### Frontend
- Svelte 5 
- TailwindCSS for styling (with typography and form plugins)
- SignalR client for real-time communication
- LucideSvelte icons for UI elements

## Features

- User authentication (register, login, token refresh)
- Real-time messaging with SignalR
- Contact management
- Message history
- REST API endpoints for CRUD operations
- Responsive UI

## Running the Project

Simply run `./telecam.sh start` in a bash terminal at the root of the project. This script will start the API, the PostreSQL container, and client applications concurrently. The API will be available at `https://localhost:5001` and the client at `http://localhost:5000`.

Be sure to have Docker installed on your machine.