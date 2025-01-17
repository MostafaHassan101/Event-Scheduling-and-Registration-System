# Event Scheduling and Registration System

## Overview

This project is a .NET Core event scheduling and registration system that demonstrates Domain-Driven Design (DDD), Clean Architecture, and deployment with Docker. It includes middleware for logging and exception handling, uses PostgreSQL (configured in Docker Compose), and illustrates best practices for managing user sessions and identity securely.

## Features

- Event Management (Create, Get, List)
- User Management (Register)
- Event Registration (Register, Cancel, List events for user, List users for event)
- Domain-Driven Design implementation
- Clean Architecture
- PostgreSQL database
- Logging and Exception Handling middleware
- Secure user session and identity management
- Deployment in Docker

## Technology Stack

- .NET Core 8
- PostgreSQL
- Docker
- Entity Framework Core
- ASP.NET Core Identity

## Project Structure

The solution is divided into four main projects following Clean Architecture principles:

1. Domain
2. Application
3. Infrastructure
4. Presentation (API)

## Getting Started

### Prerequisites

- .NET Core 8 SDK


## Setup and Installation

### Docker Images and Usage
This project uses Docker images for the API and PostgreSQL database. Follow these steps to pull and use the images:
Pulling Docker Images
To pull the Docker images, use the following commands:

- Pull the PostgreSQL image:
    -  `pull mostafahassanmo/postgres:v1.0`

- Pull the .NET Core 8 API image:
    -  `pull mostafahassanmo/eventscheduleapi:v1.0`

### Development
1. Clone the repository: `git clone hhttps://github.com/MostafaHassan101/Event-Scheduling-and-Registration-System.git`
2. Navigate to the project directory `Event-Scheduling-and-Registration-System`

#### Running Locally

1. Ensure you have .NET Core SDK installed
2. Set up a local PostgreSQL database
3. Update the connection string in `appsettings.json`
4. Run the application