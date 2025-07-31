# Hacker News

A .NET-based Hacker News clone implementation focusing on top news aggregation functionality.

## 🚀 Existing Features

### Core Functionality
- **Story Listing**: Browse submitted stories in a clean interface
- **Web API**: RESTful endpoints

### Technical Implementation
- **.NET Core/ASP.NET Core**
- **Swagger Integration**

## 🎯 Purpose

This project demonstrates:
- Clean architecture principles in .NET
- RESTful API design patterns

## 📋 Prerequisites

- .NET SDK (version as specified in project)

**Start the application**
```bash
dotnet run
```

### Usage

#### API Endpoints

- top stories: GET http://{host}/api/v1/stories?count=15
- metrics: GET http://{host}/metrics
- health: GET http://{host}/health

## 🏗️ Current Architecture

The application follows a standard Clean Architecture pattern:
- Application layer
- Infrastructure layer
- Application host
- Integration tests using Aspire
- Unit tests using xUnit

## 📝 TODO List

### High Priority Infrastructure
- [ ] **Redis Integration**: Add distributed caching
- [ ] **Integration Tests**: Comprehensive testing infrastructure
- [ ] **Unit Tests**: Core functionality testing