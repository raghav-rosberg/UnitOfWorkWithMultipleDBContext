Unit Of Work With Multiple DBContexts
===============================

A MVC 5 application with Automapper, EF 6, repository pattern, dependency injection using Autofac which uses one unit of work to deal with multiple dbcontexts.

Features:
-------------
- Entity Framework 6
- MVC 5
- Autofac
- AutoMapper
- Unit Of Work with multiple DBContexts
- Repository Pattern
- Service layer
- Code First approach

This application's design pattern overcomes most of the redundant code while creating repositories and resolves a unit of work with multiple contexts.

Dos while creating repositories:
-------------------------------------------
- Create a generic repository class and generic repository interface which exposes common functions to each entity.
- Initialize a generic repository's local DBContext object through its constructor.
- Set this DBContext's entities set in the generic repository's local entities set object for operations performed on them.

Don'ts while creating repositories for unit of work:
-------------------------------------------------------------------
- Do not create multiple repository interface and repository classes which is not required.
- Do not associate or inject your DBContext object to your generic repository class. Get the DBContext reference from Unit of work class.

Good way of designing you Service layer:
--------------------------------------------------------
- Create one service class for one controller.
- Have an individual interface for each service class.
- Inject unit of work objects corresponding to its DBContext.
- Access repositories through Unit of Work object.
- Commit all the transactions corresponding to a DBContext once with its Unit of Work object.
- In the controller inject only its related service object and call its functions for further operations.


Hope this design pattern helps. Please let me know if i had missed anything and any suggession to improve this design is welcome.

-- Raghav
