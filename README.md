# MyWorkout

## Overview
This is a React/.NET Core web application for tracking exercise programs.

## Definitions
- Program - A collection of ProgramExercises
- ProgramExercise - A movement or activity
- Workout - A Program that is associated with a particular day
- WorkoutExercise - A ProgramExercise that is associated with a particular Workout

## Getting started

In order to set up and connect to the SQL Server DB, you will need to do the following:
1. SQL Server Management Studio
   - Create a new login with SQL Server authentication
   - Assign the 'dbcreator' server role
   - Set Server properties -> Security -> Server authentication to 'SQL Server and Windows Authentication mode'
2. Enable TCP/IP in SQL Server Configuration Manager
3. Set the SSMS login username and password in appsettings.Development.json
4. Open Command Prompt, navigate to the project and run the command to create the DB
   - dotnet ef --startup-project MyWorkout.Api/MyWorkout.Api.csproj database update

## Resources

- React - https://medium.com/bb-tutorials-and-thoughts/how-to-develop-and-build-react-app-with-net-core-backend-59d4fc5e3041
- .NET Core - https://medium.com/swlh/building-a-nice-multi-layer-net-core-3-api-c68a9ef16368
