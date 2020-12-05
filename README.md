# DNS watcher
Utility to keep an eye on DNS records 

## Introduction
This is a small utility to track the values of DNS records. The API will store your records to watch, and the results will be presented through the Angular PWA. 

## Technology
* API: .NET 5, EF Core 5, PostgreSQL
* PWA: Angular 11, Milligram

## Getting started
### Database
The API is written to work with an empty PostgreSQL database. 
### API
* Fill in the application secrets such as the database connection string in `src/api/DnsWatcher.API/appsettings.json`
* Run the API from `src/api/DnsWatcher.API` with `dotnet run`
### PWA
* Install dependencies with `npm install` in `src/pwa`
* Fill in the correct API url in `src/pwa/src/assets/app-settings.json` 
* Start Angular CLI development server by running `npm run start` from `src/pwa`
