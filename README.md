# MyBackend

## Overview
MyBackend is a .NET 8.0-based backend application designed to manage routines, activities, and users for fitness tracking. It provides RESTful APIs for CRUD operations on routines, activities, and users.

## Features
- **Routines Management**: Create, read, update, and delete routines.
- **Activities Management**: Manage activities associated with routines.
- **User Management**: Handle user accounts and their associated routines.
- **Entity Framework Core**: Used for database interactions.
- **Swagger Integration**: API documentation and testing.

## Technologies Used
- **.NET 9.0**
- **Entity Framework Core**
- **MySQL Database** (via Pomelo.EntityFrameworkCore.MySql)
- **ASP.NET Core**
- **Swagger** (via Swashbuckle.AspNetCore)

## Project Structure
- **Controllers**: Contains API controllers for handling HTTP requests.
  - [`RoutinesController`](Controllers/RoutinesController.cs): Manages routines-related endpoints.
- **Models**: Defines the data models used in the application.
  - [`Routine`](Models/Routine.cs): Represents a routine entity.
  - [`User`](Models/User.cs): Represents a user entity.
  - [`Activity`](Models/Activity.cs): Represents an activity entity.
- **Data**: Contains database context and configurations.
- **obj**: Auto-generated files for build and runtime.

## API Endpoints
### Routines
- `GET /api/routines`: Retrieve all routines.
- `GET /api/routines/{id}`: Retrieve a routine by ID.
- `POST /api/routines`: Create a new routine.
- `PUT /api/routines/{id}`: Update an existing routine.
- `DELETE /api/routines/{id}`: Delete a routine.

### Users
- `GET /api/users`: Retrieve all users.
- `GET /api/users/{id}`: Retrieve a user by ID.
- `POST /api/users`: Create a new user.
- `PUT /api/users/{id}`: Update an existing user.
- `DELETE /api/users/{id}`: Delete a user.

### Activities
- `GET /api/activities`: Retrieve all activities.
- `GET /api/activities/{id}`: Retrieve an activity by ID.
- `POST /api/activities`: Create a new activity.
- `PUT /api/activities/{id}`: Update an existing activity.
- `DELETE /api/activities/{id}`: Delete an activity.

## Setup Instructions
### Prerequisites
- .NET 9.0 SDK
- MySQL Database
- Visual Studio Code or any IDE supporting .NET development

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/sharedordaz/FitTrack_Backend.git
   cd MyBackend/Backend
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update the database connection string in `appsettings.json`.
4. Apply migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## Testing
- Swagger UI is available at `http://localhost:{port}/swagger` for testing API endpoints.

## License
This project is licensed under the MIT License.

## Contact
For questions or support, contact [sharedordaz](https://github.com/sharedordaz).