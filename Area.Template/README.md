# Introduction 
Microservice Template: "Area.Template"
This template is designed to help you create microservices. In this README, we'll cover how to get started and the prerequisites you need to meet.

# Prerequisites
Before running the microservice, ensure you have the following:

1. Operating System: This template assumes you're using Windows. If you're on a different OS, make sure to  install PowerShell before proceeding.
2. Preinstalled Tools:
    - PowerShell: Ensure that PowerShell is installed.
    - Docker Desktop: Required for working with Docker containers.
    - IDE with .NET Support: Choose any development environment that supports .NET.

# Getting Started
1.  Clone this repository to your local machine.
2.  Run the RenameMicroserviceTemplate.ps1 script. It will guide you through renaming the template for your project.
3.	Run "docker-compose" profile.
4.	Run health check: https://localhost:5001/health.

# Running the PowerShell Script

  # Available Parameters 
    - Path: The root folder where replacements should occur (empty by default).
    - NewValue: The new value to insert (will chage all "Area.Template" to new value).
    - NewValuePath: The new value for file and folder paths (will chage all "/area" paths to new value path).

  # Example:
    ```powershell
    .\RenameMicroserviceTemplate.ps1 -Path "C:\Common\Microservice Template\Test\Area.Template" -NewValue "TestVM" -NewValuePath "/testvm"
    ```

# MS SQL Server
You can connect to local instance 
  Server name: localhost,1433
  Authentication: SQL Server Authentication
  Login: SA
  Password: copy value from docker-compose.yml file 
  

