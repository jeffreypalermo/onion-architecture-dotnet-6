Thanks to [Clear Measure](https://www.clearmeasure.com) for sponsoring this sample and episode of [Programming with Palermo](https://www.palermo.network).

[Code Sample](https://github.com/jeffreypalermo/onion-architecture-dotnet-6/)

[![.NET](https://github.com/jeffreypalermo/onion-architecture-dotnet-6/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jeffreypalermo/onion-architecture-dotnet-6/actions/workflows/dotnet.yml)


# .NET 6 using Onion Architecture
This is a sample of .NET 6 implemented with Onion Architecture. This is how to get started with the core of the onion architecture. We start with a clean class library, unit tests, and a build. It's important to get this foundation correct before proceeding. We want our build working before adding any domain model or interfaces.  We then add an interface to a database with a "DataAccess" project. Then we add an interface to a user, with the Blazor UI project. We add UnitTests and IntegrationTests, wire it up to both GitHub Actions as well as Azure Pipelines to show how both work easily for continuous integration. We add a private build as well as CI build build scripts. Then we configure dependency management with an Inversion of Control container and refactor the project structure so that there is one project as an entry point while keeping all interfaces independent. The Blazor webassembly application has no access to the web services project (UI.Api). The Api project that contains the web services has no access to DataAccess. Additionally, the UI.Server project is stripped of all responsibility except to start the application and configure dependencies. 

![Onion Architecture solution structure](https://raw.githubusercontent.com/jeffreypalermo/onion-architecture-dotnet-6/master/arch/SolutionStructure.png)

![Application](https://user-images.githubusercontent.com/104212/175699896-6061441b-f969-4312-ba22-3f2edac0a1d9.png)

Thanks to [Clear Measure](https://www.clearmeasure.com) for sponsoring this sample and episode of [Programming with Palermo](https://www.palermo.network).
