---

# FSMS Application

The File System Management System (FSMS) application is a command-line tool designed to manage files within different user profiles and plans, offering functionalities similar to cloud-based file management systems but localized for single-machine use.

## Getting Started

Follow these steps to set up and start using the FSMS application.

### Prerequisites

- .NET 8.0 SDK or later

Ensure that the .NET SDK is installed on your machine. You can download it from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

### Clone the Repository

First, clone the FSMS repository to your local machine using the following command:

```sh
git clone https://github.com/YaroslavKSE/Solid-Start.git
```

### Open the Project

Navigate to the `FSMS.Starter` project directory:

```sh
cd FSMS.Starter
```

### Start the Application

Run the application using the .NET CLI:

```sh
dotnet run
```

This command compiles and executes the FSMS application. Follow the on-screen prompts and instructions to interact with the application.

## Features

- **User Profiles**: Create and switch between different user profiles.
- **File Management**: Add, remove, and list files within the active user profile.
- **Plan Management**: Support for Basic and Gold plans with specific file and storage limits.
- **Plan Upgrading**: Users can upgrade their plan to increase their file and storage limits.

## Commands

- `login <profilename>`: Log in or switch to a profile.
- `add <filename> <shortcut>`: Add a file to the system with an optional shortcut.
- `remove <shortcut>`: Remove a file from the system using its shortcut.
- `list`: List all files in the current profile.
- `change_plan <plan_name>`: Change the current user's plan to either Basic or Gold.

## Contributing

Contributions to the FSMS application are welcome. Please feel free to submit pull requests or open issues to discuss proposed changes or report bugs.

---
