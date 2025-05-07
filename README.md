# ASP.NET API with React SPA

# Setup

- run `npm install` from the ReactWeb folder root (using node v22.14.0)

<strong>NOTE - </strong>if you run into dependency errors, try running `npm install --legacy-peer-deps`

- The ASP.NET API has been configured to host the React SPA. In order for this to function, run the `publish-client-app` script to publish & copy the React app to the `wwwroot` folder where it can be served by .NET

- run `dotnet run` command from the API folder and navigate to [https://localhost:4000/]

Any routes not served by the .NET API will be served by the React SPA.

API endpoint details - [https://localhost:4000/swagger/index.html]

## Authentication

Cookie authentication has been implemented to lock down the endpoints/routes.

Navigate to [https://localhost:4000/account/login] and enter the following credentials (really secure :-))

Username: ben
Password: secret
