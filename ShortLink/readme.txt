Hello! 

About application.


The server-side application is an ASP.NET Core Web API written in .NET 8, which interacts with a local MSSQL database. For the main application page, authorization, and redirection, an application created using the Angular framework in TypeScript is used.

Application Architecture
The server-side application architecture is divided into:

DomainLayout

BusinessLayout

DataAccessLayout

Web

Test

Logging is implemented in the DbContextInitialiser in the DAL layer. The logs are written to files in the Logs folder of the Web layer using the Serilog library. The MediatR framework is used to handle requests and commands. Database interactions are carried out using the ApplicationDbContext (Microsoft.EntityFrameworkCore). User authentication is implemented using Microsoft.AspNetCore.Identity.

Running the Application
To run the application, copy the HTTPS link to the repository. The repository can be found here: https://github.com/MikalaiRakhman/ShortLink

Clone the repository using the link. The project can be opened and run using Visual Studio. Select the HTTPS protocol to launch the application. Once launched, a database will be created, and an admin user will be added. A browser will then open with Swagger, displaying available requests and providing the option to authenticate.

Running the UI Part
To run the UI part of the application, copy the HTTPS link to the repository. The repository can be found here: https://github.com/MikalaiRakhman/short-link-client

Next, open Visual Studio Code and clone the repository using the link. Visual Studio Code will prompt you to open the newly cloned project. Open it. In the terminal, install the Node packages by running the command npm install. After the packages are installed, run the command ng serve. The application will start on the local address http://localhost:4200/. Enter this address in your browser to open the UI part of the application.

Usage
To work with the applications, ensure that both the server-side and UI parts are running.

Thank you, and have a great day! 😊