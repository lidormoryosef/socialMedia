
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 443

COPY ./out . 
ENTRYPOINT ["dotnet", "socialMedia.dll"]
