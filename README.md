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
In this solution you can find a very simple Blazor application, implemented using a modular approach with lazy loading.  
A series of microservices: 
> - `XmasSagas` to manage the Saga  
> - `XmasReceveir` delegate to receive the xmas letter from children  
> - `XmasWarehouses` delegate to prepare the xmas presents  
> - `XmasLogistics` delegate to load the Santa Claus sleigh to send the presents to the children
You need .NET 8 installed to run the examples.

# CQRS
Is a pattern used to split the process of writing from the process of reading the data.
It was introduced by Greg Young, and, as every pattern, is not a "one size fit all" solution, so please, handle with care!!!

# `Microservices` are not `Distributed Monolithic`
Each microservice in this solution has its own ReadModel (MongoDb) and its own EventStore.

# Run Solution
> - docker build -t xmasdev2023 .  
> - docker run --rm -p 8000:80 xmasdev2023