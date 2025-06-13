# MyBackend

## Overview
MyBackend is a .NET 8.0-based backend application designed to manage routines, activities, and users for fitness tracking. It provides RESTful APIs for CRUD operations on routines, activities, and users.

## Index

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
  - [Routines](#routines)
  - [Users](#users)
  - [Activities](#activities)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Installation](#instalation)
- [Testing](#testing)
- [License](#license)
- [Contact](#contact)

## Technologies Used
- **.NET 8.0**
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
- .NET 8.0 SDK
- MySQL Database
- Visual Studio Code or any IDE supporting .NET development

### INSTALATION:
1. Clone the repository:
   ```bash
   git clone https://github.com/sharedordaz/FitTrack_Backend.git
   cd MyBackend/Backend
   ```
2. Run the database:
   **On windows**: RUN AS ADMINISTRATOR the file InstallDB.bat
   ![Tutorial Screenshot](tutorial.png)

   **On linux or MacOS**: Run the file bash_install.sh
3. Verify you have installed dotnet. If not, install dotnet
   ```bash
   dotnet --version
   ```


3. Restore Dependencies:
   ```bash
   dotnet restore
   ```
4. Update the database connection string in `appsettings.json`.
5. Apply migrations:
   ```bash
   dotnet ef database update
   ```
6. Run the application:
   ```bash
   dotnet run
   ```

## Testing
- Swagger UI is available at `http://localhost:{port}/swagger` for testing API endpoints.

## License
This project is licensed under the MIT License.

## Contact
For questions or support, contact [sharedordaz](https://github.com/sharedordaz).