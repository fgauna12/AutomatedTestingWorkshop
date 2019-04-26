
# Automated Testing Workshop

The goal of this workshop is to help engineers start to unit test an existing application and start practicing the skills required in order to solve those real world problems.

### Pre-Requisites 

- Docker - We'll be using Linux containers
- .NET Core 2.2 SDK - The sample application uses ASP.NET Core
- An IDE - Visual Studio, VS Code, or Rider
- An API Development Tool like Postman
- SQL Server Management Studio

## Overview

### The Sample App - Keto Pal :meat_on_bone:

**Keto** is a trendy diet that has helped many people lose weight, including myself. The diet consists of consuming *very little carbohydrates* including: sugar, many fruits, many starchy vegetables, and many packaged foods. General goal for someone to be doing this diet is to consume less than 50 grams of carbohydrates in a day. 

This sample application is meant to help someone like me understand what foods they could eat. This also includes helping someone track what they've already ate and help them not exceed their daily carb limit (e.g. 50 grams/day).

### Table of Contents

- [Getting Started](#getting-started)
- [Characterization Tests](#characterization-tests)
- [Unit Tests](#unit-tests)
- [Adding a Feature](#adding-a-feature)
- [Fixing a Bug](#fixing-a-bug)

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

Great! Your database is running. Now let's try to connect to it using SQL Server Management Studio.
First, we need to know the IP to use for the SQL Server. 

If on Windows, run `ipconfig` from command line.

Then get the IP address of the virtual ethernet adapter for docker.

For example, on Windows: 

```
λ  ipconfig

Windows IP Configuration


Ethernet adapter vEthernet (DockerNAT):

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::5d2f:b0f5:e236:51b1%5
   IPv4 Address. . . . . . . . . . . : 10.0.75.1
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . . . :
```

Next, launch SQL Server Management Studio and provide the following parameters:

- Server Name - The IP Address from above (e.g. 10.0.75.1)
- Authentication: SQL Authentication
- Username: SA
- Password: The password you picked above on the `docker run` step


![](docs/images/ssms-connect.png?raw=true)

After you connect, you'll see a database called *Foods*

![](docs/images/ssms-explorer.png?raw=true)

### 2. Run the API

If you haven't already, clone this repository.

Open the `KetoPal.sln` solution.

Go to the `appsettings.Development.json` config file and ensure your connection strings have the correct IP for the server. While you're at it, also provide the database password you set up.

After you change the settings, run the project. 
Then verify that the API is working correctly by going to `http://localhost:[some port]:/api/products`

The database is quite big, hopefully it doesn't time out on you. :smile:

## Characterization Tests

You've been tasked with creating a new feature. Clearly, the code is not in the best shape. So you decide you want to do some refactoring and add automated tests. Nice! the boyscout rule - leaving things better than you found them.

Before we start creating the feature, let's create some **characterization tests**. Characterization tests help us learn from the system and how it's currently in production. This is extremly useful when learning what a system does and how it works. If you'd like a refresher on characterization tests, there's a really good [blog post](https://michaelfeathers.silvrback.com/characterization-testing) from Michael Feathers.

The feature you're going to be working on will be related to the calculation of the daily carb consumption. So you know you'll be modifying the `POST api/products/_actions/consume` API call. 

### 1. Create Characterization Tests for the `ProductsController.Get` method

Feel free to create a test project. Start experimenting and creating a characterization test to understand how it works today.

As you start learning, create some test cases to start documenting given some criteria what the expected results are.

>:warning: Note: Remember, don't start refactoring right away. You have running database using docker. Feel free to connect to it using your tests and see the actual data.

### 2. Refactor the method

Run your characterization tests often to make sure your refactoring is not breaking things.

### 1. Create Characterization Tests for the `ProductsController.Get` method

### 2. Refactor the method

## Unit Tests

Now that consumption calculation is a bit cleaner, start adding some unit tests. Run your characterization and newly created unit tests often.

## Adding a Feature

At the moment, 

## Fixing a Bug

