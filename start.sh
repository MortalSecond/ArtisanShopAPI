#!/usr/bin/env bash
set -e

echo "Starting ASP.NET Core application..."

export ASPNETCORE_URLS=http://0.0.0.0:${PORT}

dotnet ArtisanShopAPI.dll
