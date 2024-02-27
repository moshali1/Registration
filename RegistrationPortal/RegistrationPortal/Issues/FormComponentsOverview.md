# Form Components Overview

## Table of Contents
1. [FormData Class](#formdata-class)
2. [FormService](#formservice)
3. [FormController](#formcontroller)
4. [Blazor Server](#blazor-server)
5. [Blazor WebAssembly](#blazor-webassembly)
6. [Summary](#summary)

## FormData Class
The `FormData` class serves as the data access layer for form-related operations. It interfaces directly with MongoDB and provides various CRUD methods for manipulating forms. These include `CreateForm`, `UpdateForm`, `DeleteForm`, along with specialized queries like `GetFormsByCreator` and a method for checking duplicates `DoesDuplicateExist`.

## FormService
`FormService` acts as a mediator between the controller and the data access layer. It handles business logic, validations, and invokes methods from `FormData` to interact with the database. 

## FormController
The `FormController` is the HTTP API's entry point for form-related operations. It relies on the `FormService` to manage business logic and database interactions. The controller also takes care of multiple types of exceptions and returns the appropriate HTTP status codes.

## Blazor Server
In a Blazor Server architecture, you can inject `IFormService` directly into your components for seamless server-side operations. Since all components are server-rendered, there's no need to make HTTP API calls; you can directly use the service layer.

## Blazor WebAssembly
For a Blazor WebAssembly setup, an additional client-side service layer is necessary to interact with the `FormController`. This service will use `HttpClient` to make HTTP API calls, serving as an intermediary between the Blazor WebAssembly components and the server-side API.

## Summary
To summarize, the `FormData` class is focused on data access, `FormService` manages business logic, and `FormController` handles HTTP communications. Blazor Server allows direct use of `FormService`, while Blazor WebAssembly requires an additional service layer for HTTP interactions.
