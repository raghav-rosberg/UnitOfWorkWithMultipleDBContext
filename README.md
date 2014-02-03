Unit Of Work With Multiple DBContexts
===============================

A MVC 5 application with Automapper, EF 6, repository pattern, dependency injection using Autofac which uses one unit of work to deal with multiple dbcontexts with Async functions.

Features:
-------------
- Entity Framework 6
- MVC 5 Web App
- Async MVC 5 Web App
- MVC 5 WepAPI 2
- Dependency Injection and IOC using Autofac
- AutoMapper
- Unit Of Work with multiple DBContexts
- Generic Repository with Async functions
- Service layer with Async functions
- Code First approach
- Power of repository extensibility
- NUnit with Async Tests
- Moq

Instructions:
-----------------
- Please alter connection strings in the application's webconfig file.
- Go to package manager console -> select UoW_MultipleDBContext.Data and execute the command 'update-database -verbose'.

-------------------------------------------

This application's design pattern overcomes most of the redundant code while creating repositories and resolves a unit of work with multiple Dbcontexts.

Dos while creating repositories:
-------------------------------------------
- Create a generic repository class and generic repository interface which exposes common functions to each entity.
- Create individual repository classes which extends generic repository with additional functions of its interface where ever it is required.
- Initialize a generic repository's local DBContext object through its constructor.
- Set this DBContext's entities-set in to the generic repository's local entities-set object.

Don'ts while creating repositories for unit of work:
-------------------------------------------------------------------
- Do not create multiple repository interface and repository classes for all the repositories which is not required.
- Do not associate or inject your DBContext object to your generic repository class. Get the DBContext reference from Unit of work class.

Good way of designing your service layer:
--------------------------------------------------------
- Create one service class for one controller.
- Have an individual interface for each service class - Which helps customizing functions related to its service.
- Inherit generic repository interface to the service interface - Which forces the service class to expand all the methods in generic interface (optional).
- Inject unit of work objects corresponding to its DBContext in the service constructor.
- Access repositories through Unit of Work object.
- Commit all the transactions corresponding to a DBContext once with its Unit of Work object.
- In the controller inject only its related service object and call its functions for further operations.


Hope this design pattern helps. Please let me know if i had missed anything and any suggession to improve this design is welcome.

-- Raghav
