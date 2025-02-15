# UmtbProject
  - API for Calculation: A backend REST API that performs the core calculations based on the input fields.
  - Desktop Application: A Windows-based desktop application that communicates with the API to send and receive data for calculations.
  - Class library (dll): A dll that the API i using for all the Business Logic
  - MS-SQL: The API save all the calculations in the sql.

# System Architecture
  - The Sulotion built from 3 projects (1.API, 2.WPF App, 3.DLL)
  - The API using the DLL for everything
  - In the DLL each calculation using roslyn for runing the script in C# compiler (This allows operators to be added and removed without the need for special development)
  - In addition, every calculation - the record is saved in SQL
  - To maintain ease of use - every time the class logic in the DLL is built, it creates the tables in SQL with default values ​​if they do not exist - there is no need to create the tables.
  - When connecting to SQL, it connects to a special DB database that I created for the project - when running on another machine, you must make sure that the database exists.
  - The application uses the API for calculation - if the URL changes - it must be updated in AppBL.
  - In addition, the app displays the last 3 operations of the last operator + the amount of calculations since the beginning of the month for that operator.

# Built With
 - [ASP.NET](https://learn.microsoft.com/en-us/aspnet/overview) - A Platforam that allow you create and build web application
 - [roslyn](https://www.nuget.org/packages/microsoft.codeanalysis.csharp) - For claculation in real time and save the flaxability
