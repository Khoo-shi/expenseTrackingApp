# Expense Tracking App

An ASP.NET Core MVC application for managing personal expenses. This project allows users to track their expenses, categorize them, and view summaries. It includes authentication and external login options (Google and Facebook).

---

## Features

- **User Authentication**: Secure login and registration using ASP.NET Core Identity.
- **External Login**: Google and Facebook authentication.
- **Expense Management**: Add, edit, delete, and view expenses.
- **Categories**: Organize expenses into categories.
- **Responsive Design**: Mobile-friendly UI using Bootstrap.
- **Mock Data**: Pre-seeded database with realistic data for testing.

---

## Tech Stack

- **Framework**: ASP.NET Core MVC
- **Language**: C#
- **Database**: SQL Server (using Entity Framework Core)
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **IDE**: Visual Studio Code

---

## Setup Instructions

### Prerequisites

1. Install [.NET SDK 9.0](https://dotnet.microsoft.com/download).
2. Install [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or use Azure SQL.
3. Install [Visual Studio Code](https://code.visualstudio.com/).
4. Clone the repository:
   ```bash
   git clone https://github.com/Khoo-shi/expenseTrackingApp.git
   cd expenseTrackingApp
   ```

---

## Configure User-Secrets

To avoid storing sensitive data in `appsettings.json`, use ASP.NET Core's user-secrets feature:

1. Navigate to the project directory:
   ```bash
   cd WebApplication1
   ```
2. Initialize user-secrets:
   ```bash
   dotnet user-secrets init
   ```
3. Add secrets:
   ```bash
   dotnet user-secrets set "Authentication:Google:ClientId" "your-google-client-id"
   dotnet user-secrets set "Authentication:Google:ClientSecret" "your-google-client-secret"
   dotnet user-secrets set "Authentication:Facebook:AppId" "your-facebook-app-id"
   dotnet user-secrets set "Authentication:Facebook:AppSecret" "your-facebook-app-secret"
   ```
4. The application will automatically use these secrets at runtime.

---

## Run the Project Locally

1. Open the project in Visual Studio Code:
   ```bash
   code .
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update the database:
   ```bash
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run --project WebApplication1
   ```
5. Open your browser and navigate to `https://localhost:5001`.

---

## Publish or Deploy

### Deploy to Azure

1. Create an Azure App Service:
   - Go to the [Azure Portal](https://portal.azure.com).
   - Create a new App Service with the `.NET 9` runtime.
2. Set environment variables in Azure:
   - Navigate to **Configuration** > **Application Settings**.
   - Add the following:
     - `GOOGLE_CLIENT_ID`
     - `GOOGLE_CLIENT_SECRET`
     - `FACEBOOK_APP_ID`
     - `FACEBOOK_APP_SECRET`
3. Publish the app:
   - Use Visual Studio Code's Azure Tools extension, or
   - Use the Azure CLI:
     ```bash
     az webapp up --name <app-name> --resource-group <resource-group> --runtime "DOTNET|9.0"
     ```

---

## Folder Structure

```
expenseTrackingApp/
├── WebApplication1/
│   ├── Controllers/        # MVC Controllers
│   ├── Data/               # Database context and seeders
│   ├── Models/             # Data models
│   ├── Views/              # Razor views
│   ├── wwwroot/            # Static files (CSS, JS, etc.)
│   ├── appsettings.json    # App configuration
│   └── Program.cs          # Application entry point
├── expenseTrackingApp.sln  # Solution file
```

---

## License

This project is licensed under the [MIT License](LICENSE).