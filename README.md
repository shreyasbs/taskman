# taskman
## Overview

Task Management Software is a simple and efficient application designed to help users manage their tasks. It provides functionalities to create, update, and delete tasks, ensuring an organized and productive workflow.

## Features

- **Create Tasks**: Easily create new tasks with relevant details.
- **Update Tasks**: Modify existing tasks to reflect changes in priority, description, or status.
- **Delete Tasks**: Remove tasks that are no longer needed.
- **Task List**: View a list of all tasks with their current status.

## Technologies Used

- **Backend**: .NET Core Web API
- **Frontend**: Angular 17
- **Database**: SQL Server (with Entity Framework Core)
- **Others**: HTML, CSS, TypeScript

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js and npm](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/shreyasbs/taskman.git
   cd taskman
   
2. Backend Setup

Navigate to the API project directory:
Install dependencies:
dotnet restore

Update the appsettings.json file with your SQL Server connection string:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
Run the application:
dotnet run

3. Frontend Setup
 cd TaskMan
 
 Install dependencies:
 npm install
 
 Run the application:
 ng serve
 
 Change the environemnt file to the API running URL
 
Open your browser and navigate to http://localhost:4200.


Usage
Creating a Task
Click on the "Add Task" button.
Fill in the task details (title, description, status).
Click the "Save" button to save the task.
Updating a Task
Click on the edit link in the Action column of the individual task row.
Click on the task you want to update.
Modify the task details as needed.
Click the "Save" button to save the changes.
Deleting a Task
Navigate to the task list.
Click the delete text in the Action column of the individual task row to the task you want to remove.
Confirm the deletion.


Project Structure
backend/: Contains the .NET Core Web API project.
Controllers/: API controllers.
Models/: Entity models.
Data/: Data context.
Repositories/: Data access logic.
Services/: Business logic.
ViewModels/: Seperating Business Models structure to limit to the user view.

frontend/: Contains the Angular 17 project.
src/: Angular application source code.
app/: Main application module and components.
assets/: Static assets.
environments/: Environment configuration.

Contact
For any inquiries, please contact shreyasbs86@gmail.com.

