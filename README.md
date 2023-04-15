# Accolite.Bank.API

## To run the application locally:
1. Have Docker Desktop installed on your local machine
1. Clone the repository
1. Open repository in Visual Studio
1. Update the paths to your local ones [here](src/docker-compose/docker-compose.yml)
1. Make sure that docker-compose project is selected as a Startup project
1. Launch the solution (this will spin up 2 docker containers, one for database, another one for the API)
1. Connect to the database using MSSQL and run this [script](/db/Accolite_Bank.sql)
1. Open user secrets for the API project (you can do that by right-clicking on the API project and choosing `Manage User Secrets` menu option)
1. Add the connection string to Database in the form of:
`"ConnectionStrings:AccoliteBank": "Server=host.docker.internal;Database=Accolite_Bank;User=sa;Password=password;TrustServerCertificate=True"`
1. Restart the solution
1. If browser didn't open for some reason, go to `https://localhost:8088/swagger/index.html` to view and test the API.
