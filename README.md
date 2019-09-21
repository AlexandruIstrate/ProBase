# ProBase

[![Build status](https://dev.azure.com/Alexandru-Istrate/ProBase/_apis/build/status/azure/ProBase-Default)](https://dev.azure.com/Alexandru-Istrate/ProBase/_build/latest?definitionId=2)

ProBase is the new way of accessing databases in C#. Unlike any other strategies like using query strings directly in the code or using an ORM, ProBase alows you to communicate with a database by using the most flexible and safe system - stored procedures. What ProBase adds on top of the classic ADO.NET data primitives is the ability to generate procedure calls based on annotated methods, thus allowing you to call a procedure in the same way you would call a method.

![Main Image](images/Main.png)

## Example
Firstly, you need to define a type that maps a set of methods to the database procedures. Because all of the database access logic is handled by ProBase, the type has to be an interface so it only contains the signatures of the methods:

```csharp
[DatabaseInterface]
interface IDatabaseOperations
{
    [Procedure("CreateItem")]
    void Create(object item);

    [Procedure("ReadItem")]
    object Read(int id);

    [Procedure("UpdateItem")]
    void Update(int id, object item);

    [Procedure("DeleteItem")]
    void Delete(int id);
}
```

In the snippet above, you can see that there are a bunch of attributes declared. The one applied to the interface (```DatabaseInterface```) specifies that the interface is to be used for interfacing with a database. This is a required attribute and must be present on any interface you want to use with ProBase. The attributes applied to each method are ```Procedure``` attributes which specify how a certain method maps to a procedure. In the example above you can see that the argument passed into the attribute constructor is the name of the procedure this method maps to.

Once we have our mappings set up, we can create an instance of this interface on which we can call the actual methods. For this we need to create a ```DatabaseContext``` object passing in a ```DbConnection``` to the constructor. With the object created, we can invoke the ```GenerateObject``` method that takes in the type of the operations class and returns an instance of that class with all of the database access logic implemented for us:

```csharp
SqlConnection connection = new SqlConnection(connectionString);

DatabaseContext databaseContext = new DatabaseContext(connection);
IDatabaseOperations operations = databaseContext.GenerateObject<IDatabaseOperations>();
```

With the ```IDatabaseOperations``` object created we can begin calling methods which in turn will call the database procedures they are bound to:

```csharp
operations.Create(new object());
operations.Read(id: 5);
```

## Built With
- C#
- .NET Standard
- ADO.NET
- NUnit

## Building From Source
In order to build ProBase you need:
- Visual Studio
- .NET Framework 4.8 or .NET Core 2.2

## Disclaimer
- The main image is made using [WordArt.com](https://wordart.com)
