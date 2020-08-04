# Anonymized-Assessment
This Repository contains the solution, an account management application, for the received Codding Assignment from Anonymized, as part of the Recruitment Process.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

.NET Core 2.2

### Installing

Navigate to the root folder of the solution and run

``
dotnet restore
``

``
dotnet build
``

Navigate to the Anonymized.Assessment.Api project folder

``
cd Anonymized.Assessment.Api
``

To start the application, run:

``
dotnet start
``

The application will load the list of customers from *dataSeed.json* file into an In-Memory Database **(Microsoft.EntityFrameworkCore)**


The application will start at port 5000 and the api can be accessed from */api* path.
For swagger, open 

http://localhost:5000/swagger


### Use the application

Two interact with the application, use one of the two endpoints:

``
GET http://localhost:5000/api/customer/{id}
``

To get a customer for the {id}


``
POST http://localhost:5000/api/accountmanagement
``

``
{
  "customerId": "string",
  "initialCredit": double
}
``

To open an acount for a customer

## Test

* UnitTest can be found in *Anonymized.Assessment.Api.UnitTests* project.
* IntegrationTests can be found in *Anonymized.Assessment.Api.IntegrationTests* project. Those tests are incomplete, because they do not fake the database.


## Furthure Work

**Front-End** - Work in Progress found on **implement-account-management-front-end** branch. 

At this moment, it contains only a simple page to get the customers. 

It uses the **Angular Template Project** generated by the framework.


## Authors

* **Dinu Ionut Stefan** - [LinkedIn](https://www.linkedin.com/in/stefan-dinu-479a469b/)