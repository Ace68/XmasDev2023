# XmasDev2023
Example for XmasDev 2023 conference


# Scenario
When we have to deal with distributed systems we need to balance three forces 
> - Communication (sync vs async)
> - Persistence (strong vs eventually)
> - Workflow (orchestrator vs choreography)


# What is a Saga?
When your business workflow depends by more than one microservice, you have to manage it.
A Saga is a Long Running Process that involves all the microservices requested. There are two kind of Sagas
> - Orchestrator  
> - Choreography  
As you know, in Software Architecture, all is a Trade-Off, and which kind of Saga you choose is a trade-off.
For this example I choose 
> - Asynchronous communication
> - Eventually Consistency
> - Orchestrator workflow

# The solution
In this solution you can find:  
> - `XmasBlazor` example of Modular Architecture with lazy loading in FrontEnd  
A series of microservices: 
> - `XmasSagas` to manage the Saga  
> - `XmasReceveir` delegate to receive the xmas letter from children  
> - `XmasWarehouses` delegate to prepare the xmas presents  
> - `XmasLogistics` delegate to load the Santa Claus sleigh to send the presents to the children
You need .NET 8 installed to run the examples.

# CQRS
Is a pattern used to split the process of writing from the process of reading the data.
It was introduced by Greg Young, and, as every pattern, is not a "one size fit all" solution, so please, handle with carefully!!!

# Muflone
It's an open-source project to help you implementing Domain-Driven Design with Event-Driven approach.  
[You can find more details here](https://github.com/cqrs-muflone)  
[You can find more examples here](https://github.com/brewup)  


# `Microservices` are not `Distributed Monolithic`
Each microservice in this solution has its own ReadModel (MongoDb) and its own EventStore.

# Run Solution
> - Prepare Infrastructure: docker-compose up -d (inside docker folder)  
> - XmasSagas:
> > - docker build xmassagas .
> > - docker run --rm -p 50000:80 xmassagas
> - XmasReceiver:
> > - docker build xmasreceiver .
> > - docker run --rm -p 50100:80 xmasreceiver
> - XmasWarehouses:
> > - docker build xmaswarehouses .
> > - docker run --rm -p 50200:80 xmaswarehouses
> - XmasLogistics:
> > - docker build xmaslogistics .
> > - docker run --rm -p 50300:80 xmaswarehouses