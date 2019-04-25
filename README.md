
# Automated Testing Workshop

The goal of this workshop is to help engineers start to unit test an existing application and start practicing the skills required in order to solve those real world problems.

### Pre-Requisites 

- Docker - We'll be using Linux containers
- .NET Core 2.2 SDK - The sample application uses ASP.NET Core
- An IDE - Visual Studio, VS Code, or Rider
- An API Development Tool like Postman

## Overview

### The Sample App - Keto Pal

**Keto** is a trendy diet that has helped many people lose weight, including myself. The diet consists of consuming *very little carbohydrates* including: sugar, many fruits, many starchy vegetables, and many packaged foods. General goal for someone to be doing this diet is to consume less than 50 grams of carbohydrates in a day. 

This sample application is meant to help someone like me understand what foods they could eat. This also includes helping someone track what they've already ate and help them not exceed their daily carb limit (e.g. 50 grams/day).

### Table of Contents

- Getting Started
- Characterization Tests
- Unit Tests
- Adding a Feature
- Fixing a Bug

## Getting Started

The sample application is an ASP.NET Core API with .NET Core. It uses a SQL Server database which we'll be able to pull using Docker. 

### 1. Running the Database

To save time, let's use Docker to pull down an image of SQL Server 2017 on Linux with a database and data already populated.

Run the following command using a password of your choice: 

``` powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=[some silly password]" -p 1433:1433 --name foodsql -d fgauna12/fooddb:latest
```

Verify that the database is running:

``` powershell
docker ps
```

You should see something like this:

```
CONTAINER ID        IMAGE                    COMMAND                  CREATED             STATUS              PORTS                    NAMES
185ee7e5db9f        fgauna12/fooddb:latest   "/opt/mssql/bin/sqls…"   22 hours ago        Up 22 hours         0.0.0.0:1433->1433/tcp   foodsql
```