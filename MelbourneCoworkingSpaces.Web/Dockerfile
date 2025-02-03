#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#STAGE BASE (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base #immagine base di ASP.NET Core 6.0
WORKDIR /app #imposta la directory di lavoro all'interno del container
EXPOSE 80 #HTTP
EXPOSE 443 #HTTPS

#STAGE BUILD
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build #immagine di sviluppo (SDK) di ASP.NET Core 6.0
WORKDIR /src
COPY ["MelbourneCoworkingSpaces.Web.csproj", "."] #copia il file di progetto nel container
RUN dotnet restore "./MelbourneCoworkingSpaces.Web.csproj" #ripristina i pacchetti NuGet

COPY . . #copia tutti i file del progetto nel container
WORKDIR "/src/."
RUN dotnet build "MelbourneCoworkingSpaces.Web.csproj" -c Release -o /app/build
#compila il progetto in modalità Release e output nella directory /app/build

#STAGE DI PUBBLICAZIONE
FROM build AS publish
RUN dotnet publish "MelbourneCoworkingSpaces.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
#compila e pubblica l'applicazione in modalità Release nella directory /app/publish

#STAGE FINALE (Runtime)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . #copia i file pubblicati dallo stage di pubblicazione
ENTRYPOINT ["dotnet", "MelbourneCoworkingSpaces.Web.dll"] #specifica il comando di avvio dell'applicazione