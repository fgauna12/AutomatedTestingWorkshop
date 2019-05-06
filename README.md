
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

## Getting Started

The sample application is an ASP.NET Core API with .NET Core. It uses a SQL Server database which we'll be able to pull using Docker. 

### 1. Running the Database

To save time, let's use Docker to pull down an image of SQL Server 2017 on Linux with a database and data already populated.

Run the following command using a password of your choice: 

``` powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=#newPass1" -p 1433:1433 --name foodsql -d fgauna12/fooddb:latest
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

> :fire: Note - If you have trouble connecting to the database using SSMS, you can try using the command line:
[MSFT Docs - Connection to SQL Server](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-2017&pivots=cs1-powershell#connect-to-sql-server). If you can't connect, you can try killing your container using a new SA password.

### 2. Run the API

If you haven't already, clone this repository.

Open the `KetoPal.sln` solution.

Go to the `appsettings.Development.json` config file and ensure your connection strings have the correct IP for the server. While you're at it, also provide the database password you set up.

After you change the settings, run the project. 
Then verify that the API is working correctly by going to `http://localhost:[some port]:/api/products`

The database is quite big, hopefully it doesn't time out on you. :smile:

> :alarm_clock: **Pause** - Wait for the proctor to move on

## Characterization Tests

<img src="http://www.informit.com/ShowCover.aspx?isbn=0131177052" alt="working with legacy code" height="200"/>

You've been tasked with creating a new feature. Clearly, the code is not in the best shape. So you decide you want to do some refactoring and add automated tests. Nice! the boyscout rule - leaving things better than you found them.

Before we start creating the feature, let's create some **characterization tests**. Characterization tests help us learn from the system and how it's currently in production. This is extremly useful when learning what a system does and how it works. If you'd like a refresher on characterization tests, there's a really good [blog post](https://michaelfeathers.silvrback.com/characterization-testing) from Michael Feathers.

### Useful Links

- [Testing Controller Logic in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2)
- [Parsing Results from ActionResult](https://stackoverflow.com/a/51489502)

### 1. Create Characterization Tests for the `ProductsController.Get` method

Feel free to create a test project. Start experimenting and creating a characterization test to understand how it works today.

As you start learning, create some test cases to start documenting given some criteria what the expected results are.

>:warning: Note: Remember, don't start refactoring right away. You have running database using docker. Feel free to connect to it using your tests and expirement away.

### 2. Refactor the method

Run your characterization tests often to make sure your refactoring is not breaking things.

Experiment with using SOLID principles and trying out concepts like _Clean Architecture_.

#### 2.B Stretch Goal 

What happens when the database goes down? Can you similate this?

> :alarm_clock: **Pause** - Wait for the proctor to move on

### 3. Create Characterization Tests for the `ProductsController.Consume` method

Now let's do it again for the consumption api call.

### 4. Refactor the method

After you have the tests working, refactor and re-run your tests often.

Are you able to reuse some of the code you previously created?

#### 4.B Stretch Goal 

Do some thinking and Googlefu:

- What's are the downsides of using a static class like `InMemoryUsers`?
- Where and how would you do validation API requests? 
- How could you test the logic inside the stored procedures?

> :alarm_clock: **Pause** - Wait for the proctor to move on

## Unit Tests

<img src="http://t1.gstatic.com/images?q=tbn:ANd9GcRll7vIIAPsaPfALjtDK-jVGFa2KZ4ZRsccYeBm2viTHQ-e_VNr" alt="Unit Tests" height="200"/>

Now that consumption calculation is a bit cleaner, start adding some unit tests. Run your characterization and newly created unit tests often.

### Useful Packages

- [Bogus](https://github.com/bchavez/Bogus)
- [Moq](https://github.com/moq/moq)
- [Shouldly](https://github.com/shouldly/shouldly)

### 1. Create Unit Tests for the logic related to `ProductsController.Get`

Know that you know some of the characteristics of the code and how it behaves *and* you have refactored it using well-known practices. Add some unit tests for the test cases you do know. **It's ok to keep refactoring**, it didn't have to be perfect the first time.

Run your tests often.

### 2. Create Unit Tests for the logic related to `ProductsController.Consume`

Same Idea!

### 3. Now get rid of your characterization tests :wink:

After you're done, feel free to blow away your characterization tests, leaving behind the unit tests. :muscle:

Or you could choose to convert those tests to clean integration tests. 
