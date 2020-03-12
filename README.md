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
# Run the compiler. If no file is provided it uses stdin as input
dotnet run --project PascalCompiler /Path/To/SourceCode.pas 
```

## Outputs
You can find the outputs of the examples inside the folder named **Output**.

The **Lexer** folder inside contains the outputs from the Lexer to the provided examples.

## Example errors
The token procedure is not defined on the grammar. It was implemented because it seemed like a typo.

Also, the grammar requires identifiers to be alphanumeric only. But on Example8 an identifier includes a _