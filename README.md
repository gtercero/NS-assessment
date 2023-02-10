# NS-Assessment

NSApp contain an ASP.NET MVC web application where the users can upload, download and delete their files. 

The web app diplays a list with all the files uploaded and their metadata.The user can upload news files and add them a description. 
Also, the user can download and delete any file. 

Azure Services
1-The user authentication system uses Azure AD B2C to register and users login. It's necessary create an account with an email and a code will sent to the email. 
The user must use it to verify the account and login. 
2-The files are uploaded in Azure Blob Storage.
3-Metadata of the files is stored in Azure Cosmos DB.

Development
The web app was developed in Visual Studio Code IDE with mvc and --auth IndividualB2C command to include the Azure AD B2C configuration automatically. 
Also, Program.cs contains the initialization of Azure Cosmos DB and Azure Blob Storage. Their keys and connections are defined in appsetting.json and 
two settings classes manage them. Three environment variables were created as user-secrets.
CosmosDBSettings:Key = <value>
BlobStorageSettings:AccountKey = <value>
AzureAdB2C:ClientId = <value>

Deployment
The web app was deployed to an Azure App Service and the same environment variables were defined in the configuration settings.

Running the web app
1-Copy the link of the repository and clone it in Visual Studio Code.
2-Add assets to build (C# extensions) if VS Code request them.
3-Execute from the terminal
//to add packages
dotnet add package Microsoft.Azure.Cosmos --version 3.32.0
dotnet add package WindowsAzure.Storage --version 9.3.3
//to enable user-secrets
dotnet user-secrets init
//create the environment variables mencioned early as user secrets (detail will be attach to the email).
3-Execute dotnet run. 
