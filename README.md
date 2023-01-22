# Instructions for running

## Backend
Prerequesites: Local SQL server running. Visual Studio with .Net6.0 SDK installed. Node&NPM installed.
* In project root directory:
* Open dictionary.sln in Visual Studio.
* Build the project.
* Run migrations

 `dotnet ef database update --project DictionaryDataAccess`
* Run DataLoader project
* Run DictionaryApi project in 'http' configuration.
## Client
* In /DictionaryWebApp directory:
* run `npm i`
* run `npm start`

