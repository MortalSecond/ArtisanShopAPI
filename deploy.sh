#!/bin/bash
echo "Running database migrations..."
dotnet ef database update --no-build

echo "Starting application..."
dotnet ArtisanShopAPI.dll