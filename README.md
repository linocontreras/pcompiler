# ECPascal Compiler

## Dependencies
This project was built using the following tools

- dotnet-runtime 3.1.2.sdk102-1
- dotnet-sdk 3.1.2.sdk102-1

## Building
Inside the root folder run the next commands.
```bash
# Restore the dependencies and tools for the projects
dotnet restore
# Build the solution
dotnet build
```

## Usage
```bash
# Run the compiler. If no file is provided it uses stdin as input
dotnet run --project PascalCompiler /Path/To/SourceCode.pas 
```
