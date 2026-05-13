# MVC Front End and Web API Appointment Booking System

This project is a full-stack appointment booking system built with. The application demonstrates full-stack web development skills including API development, database integration, authentication, session management, and front-end/back-end communication.

## Features

* Patient-facing workflow for selecting a physician and requesting or booking appointments
* Physician availability scheduling and appointment selection
* Appointment booking flow with patient details and reason-for-visit submission
* Physician-facing dashboard for viewing upcoming appointments
* RESTful Web API architecture with separate MVC front end
* Session-based user authentication and role handling
* Entity Framework Core database integration
* Swagger testing support through Swashbuckle

---

# Key Product and Technical Decisions

ASP.NET was selected due to my prior experience with the framework, allowing for rapid and efficient development. The framework also provides a mature ecosystem with built-in tools and libraries that accelerate application development, including Entity Framework Core for ORM/database management and Bootstrap for responsive UI development.

The application was intentionally structured with a separate MVC front end and Web API backend to demonstrate:

* API-driven architecture
* separation of concerns
* DTO mapping
* HTTP communication between services
* scalable application structure

SQLite was chosen as the database for simplicity and fast setup during development. This allowed development effort to remain focused on application architecture and functionality rather than database infrastructure configuration.

Swashbuckle/Swagger was used to streamline API endpoint testing and debugging throughout development.

---

# Technologies Used

* ASP.NET MVC
* ASP.NET Web API
* C#
* Entity Framework Core
* SQLite
* Bootstrap
* Swagger / Swashbuckle
* LINQ
* Session-based Authentication

## AI-Assisted Development

AI tools were used to accelerate development through debugging assistance, rapid UI mockup generation, formatting, proofreading, and boilerplate code creation. All generated code was reviewed, modified, and integrated manually to ensure correctness and maintainability.

---

# How to Run

This project is currently configured for local development purposes.

Recommended IDE:

* Visual Studio

Steps:

1. Clone the repository
2. Open the Web API project in Visual Studio
3. Run in the powershell console: dotnet ef database update
4. Run the WebAPI project in Visual Studio
5. Open the Front End MVC project in Visual Studio and run it
6. Ensure the Front End project targets the same localhost/API port configured in the Web API `launchSettings.json`

---

# Future Improvements

There are several features and architectural improvements I would implement in future iterations of the project.

Potential feature additions include:

* User registration
* Appointment updates and cancellations
* Appointment history
* Improved scheduling conflict validation
* Enhanced physician availability management
* Better authentication/token-based authentication
* Improved UI/UX styling

One of the primary architectural improvements would be redesigning how users are modeled within the system. During development, I identified that `Patient` and `Physician` entities would be better implemented through inheritance from `IdentityUser` rather than existing as separate database entities tied to authentication records. This would eliminate duplicate identifiers for a single user and create a cleaner authentication and authorization design.

Another improvement would be deploying the web app for demonstration purposes.

---

# Purpose of the Project

This project was developed as a technical work sample to demonstrate:

* full-stack development
* REST API design
* database modeling
* Entity Framework Core usage
* MVC architecture
* authentication/session handling
* clean project organization
* practical software engineering decision-making

It was designed with readability, maintainability, and real-world development workflows in mind.
