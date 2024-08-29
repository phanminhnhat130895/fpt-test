# Auto Driving Car Simulation Console Application

This is a simple .NET 8 console application, which demonstrates basic functionality in a console environment. Below are the steps to get started, including building, running, and publishing the application.

## Prerequisites

Ensure that you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A code editor such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

To verify if the .NET 8 SDK is installed, run this command:

```bash
dotnet --version
```

You should see output similar to:

```bash
8.0.100
```

## Cloning the Repository

If this project is stored in a Git repository, you can clone it using:

```bash
git clone https://github.com/phanminhnhat130895/fpt-test.git
cd fpt-test
```

## Building the Application

To build the project, follow these steps:

1. Open a terminal or command prompt.
2. Navigate to the project directory (if not already there):

   ```bash
   cd path/to/your/project
   ```

3. Run the build command:

   ```bash
   dotnet build
   ```

This command compiles the application and generates the output files in the `bin/` directory.

### Build Configuration

You can specify the build configuration using the `--configuration` option. By default, it is set to `Debug`, but for a production build, use `Release`:

```bash
dotnet build --configuration Release
```

## Running the Application

There are two main ways to run the application.

### Option 1: Run from Source

Use this command to run the application directly from the source:

```bash
dotnet run
```

### Option 2: Run Compiled Output

If you'd like to run the application from its compiled output:

```bash
dotnet bin/Debug/net8.0/AutoDrivingCarSimulation.dll
```

If you built it for `Release`, run it with:

```bash
dotnet bin/Release/net8.0/AutoDrivingCarSimulation.dll
```

## Publishing the Application

To publish the application for deployment, run the following command:

```bash
dotnet publish --configuration Release --output ./publish
```

This will create a self-contained package in the `publish/` directory, which can be deployed or distributed.

## Running Tests

If the project contains unit tests, you can run them using:

```bash
dotnet test
```

This will build and run all the tests in the solution.

## Cleaning the Build

Before switching between build configurations (Debug/Release) or for a fresh build, it's recommended to clean the project:

```bash
dotnet clean
```

This will remove all compiled files from the `bin/` and `obj/` directories.

## Additional Resources

- [.NET Documentation](https://learn.microsoft.com/en-us/dotnet/core/introduction)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Visual Studio](https://visualstudio.microsoft.com/)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
