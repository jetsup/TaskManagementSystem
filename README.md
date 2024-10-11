# Task Management System

This is a simple task management system that allows users to create, read, update, and delete tasks. The system is built using ASP.NET Core and Entity Framework Core. The system uses MariaDB as the database, but MySQL can be used as well.

## Requirements

-   .NET Core 6+
-   MariaDB or MySQL

## Installation

1. Clone the repository
2. Open the project in Visual Studio or Visual Studio Code and install the dependencies

    ```bash
    dotnet restore
    ```

3. Update the connection string in `appsettings.json` to point to your MariaDB or MySQL database with your database login credentials
4. Run the following commands in the terminal to create the database and run the migrations:

    ```bash
    dotnet ef database update
    ```

5. Run the project

    ```bash
    dotnet run
    ```

## Usage

The system has a simple web interface that allows users to create, read, update, and delete tasks. Users can also mark tasks as complete.

## Testing

The system was run on Ubuntu 20.04 with MariaDB but should work on other operating systems and with MySQL as well.
