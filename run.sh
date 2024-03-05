#!/bin/bash

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the project
dotnet run
