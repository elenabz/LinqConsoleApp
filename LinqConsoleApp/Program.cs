using System.Diagnostics.CodeAnalysis;
using System.Linq;

List<Customer> customers = new List<Customer>
{
  new Customer("Bartleby", "London"),
  new Customer("Benjamin", "Philadelphia"),
  new Customer("Michelle", "Busan" )
};


// LINQ is a set of features for writing queries on collection types
// using System.Linq; for LINQ
//  the type of an executed LINQ query’s result is not always known, it is common to store the result in an implicitly typed variable

// In C#, LINQ queries can be written in method syntax or query syntax.
// Linq Operations: Select data, Projection = change shape of object into another object, Order, Get an Element, 
//                  Filter (with where), Iteration, Quantify, Set Comparison, Set Operations, Joining, Grouping,
//                  Distinct Sets, Aggregation(min, max)

// a query is executed when the value resulted is requested = defered execution
// immediate execution = ToList(), OrderBy()
// Streaming Operator = we don't loop over collection each time we use a Linq method, we loop over the result 
//                      list.Select().Where() = where is executed first and Select next 
// Non-Streaming: GroupBy, Except, Join, OrderBy ...
// Yield keyword is used to create a streaming operation 

// aggregate method , iterate over the collection a do an operation (ex: sum of all items)
// calculate a single value from a property from all the collection objects 
// aggregate is called last, after all the other Linq methods 
// Min, Sum, Count, Max
// using Aggragate() with a custom expresion
// 

// Combine two collections : Union(), UnionBy() to eliminate duplicates
//                           Concat() to add all the items from the both collections 



// Method syntax
var custQuery2 = customers.Where(cust => cust.City == "London").ToList();

// Query syntax
var custQuery =
    from cust in customers
    where cust.City == "London"
    select cust;

var only = customers.SingleOrDefault(c => c.Name == "CHIPS");
Console.WriteLine(only?.Name);
foreach (var cust in custQuery2)
{
    Console.WriteLine(cust.Name);
}
//Console.WriteLine(custQuery2.ToString());
// Where, From, Select, foreach, count, 

// ========================================

List<Product> products = new List<Product>()
{
    new Product {
        Name = "Frame",
        Id = 1,
        Color = "red",
        StandardCost = 200,
        ListPrice = 900,
        Size = "58"
    },
    new Product {
        Name = "Frame 2",
        Id = 2,
        Color = "yellow",
        StandardCost = 100,
        ListPrice = 400,
        Size = "38"
    },
    new Product {
        Name = "Frame 3",
        Id = 3,
        Color = "yellow",
        StandardCost = 150,
        ListPrice = 400,
        Size = "44"
    }


};

// get the list 

List<Product> list = (from p in products select p).ToList();
List<Product> list2 = products.Select(p => p).ToList();
foreach (var product in list)
{
    Console.WriteLine("Name: " + product.Name + " - Id: " + product.Id + " - Size:" 
        + product.Size + " - StandardCost: " + product.StandardCost + " - Color: " + product.Color);
}

foreach (var product in list2)
{
    Console.WriteLine("Name: " + product.Name + " - Id: " + product.Id + " - Size:" + product.Size + " - StandardCost: " + product.StandardCost);
}


// get a single column/property
List<string> strList = new List<string>();
strList.AddRange(from prod in products select prod.Name); // AddRange = add at the end of list

List<string> strList2 = new List<string>();
strList2.AddRange(products.Select(p => p.Name));
foreach (var name in strList)
{
    Console.WriteLine(name);
}
foreach (var name in strList2)
{
    Console.WriteLine(name);
}


// get specific columns

List<Product> columnsOfProd = new List<Product>();
columnsOfProd = (from col in products
                 select new Product
                 {
                     Name = col.Name,
                     Size = col.Size
                 }).ToList();

List<Product> columnsOfProd2 = products.Select(prod => new Product
{
    Name = prod.Name,
    Size = prod.Size
}).ToList();

foreach (var product in columnsOfProd)
{
    Console.WriteLine(product.Name);
}
foreach (var product in columnsOfProd2)
{
    Console.WriteLine(product.Name);
}

// use Linq to order data (orderby = sql clause, OrderBy = Linq method)
// by default is ordering Ascending 

var orderProducts = (from prod in products 
                     orderby prod.Size // ascending
                     select prod).ToList();
var orderProducts2 = products.OrderBy(prod => prod.Size).ToList(); // no need for .Select(prod => prod)
foreach (var prod in orderProducts)
{
    Console.WriteLine(prod.Size);
}
foreach(var product in orderProducts2)
{
    Console.WriteLine(product.Size);
}

// Descending Order 

var desOrder = (from prod in products
                orderby prod.Name descending
                select prod).ToList();
var desOrder2 = products.OrderByDescending(prod => prod.Name).ToList();

// Two Field Ordering 

var twoFieldOrder = (from prod in products
                     orderby prod.Size descending, prod.Name
                     select prod).ToList();
var twoFieldOrder2 = products.OrderByDescending(prod => prod.Size).ThenBy(prod => prod.Name).ToList();
foreach (var product in twoFieldOrder2)
{
    Console.WriteLine(product.Name);
}

// Linq Where Clause to find items in a collection 

var prodGratterThan100 = (from prod in products
                          where prod.StandardCost > 100
                          select prod).ToList();
var prodGratterThan100_2 = products.Where(prod => prod.StandardCost > 100).ToList();
foreach (var product in prodGratterThan100_2)
{
    Console.WriteLine("name: " + product.Name);
}

// Where with Two Fields Query 

var twoWhere = (from prod in products
                where prod.StandardCost > 100 && prod.Name.Contains("F")
                select prod).ToList();

var twoWhere2 = products.Where(prod => prod.StandardCost > 100 && prod.Name.Contains("F")).ToList();

Customer.printArrayInfo(twoWhere2);

// Find One Item in Collection: Last, First - throw exception if no item found, code is similar
//                              Single, SingleOrDefault - only one item should pass the expression,
//                                  if collection null or different than one items found - throws exception 
// FirstOrDefault(expresion, defaultValue) -- returns the first item found or default value if no item was found


var firstProd = (from prod in products select prod).First(prod => prod.Color == "red");
Console.WriteLine(firstProd.Name);

// handle Exception thrown by First
try
{
    var firstProd2 = products.First(prod => prod.Color == "ui");
} catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

var firstOrDefProd = (from prod in products select prod).FirstOrDefault(prod => prod.Color == "red");

var firstOrDefProd2 = products.FirstOrDefault(prod => prod.Color == "gray"); // returns null
Console.WriteLine("null " + firstOrDefProd2);

// with default value 
var firstDefaultValue = products.FirstOrDefault(prod => prod.Color == "gray", new Product { Id = -1, Name = "Not Found" });
Console.WriteLine(firstDefaultValue.Name);

var firstDefaultValue2 = products.FirstOrDefault(prod => prod.Color == "gray", products[0]);
Console.WriteLine(firstDefaultValue2.Name);

var singleItem = (from prod in products select prod).Single(prod => prod.Color == "red");
Console.WriteLine("single: " + singleItem.Name);

try
{
    var singleItem2 = products.Single(prod => prod.Color == "yellow");
} catch(Exception ex)
{
    Console.WriteLine("ex thrown: " + ex.Message);
}

var singleODefItem2 = products.SingleOrDefault(prod => prod.Color == "po", products[0]);
Console.WriteLine("single or default: " + singleODefItem2.Id);


// 1. First()/Last()
// *****************
// we expect the element to be present 
// we want to handle/throw an exception if the element wasn't found
// we want to search until it finds an element

// 2. FirstOrDefault()/LastOrDefault()
// ***********************************
// if we are not sure if the element is present 
// we don't want to handle an exception, more simply to check if the element found is null
// we want to get back a null or other default value 



// 3. Single()
// ***********
// we expect the element to be present 
// we want to handle or throw the exception if not found 
// we want to search the entire list 
// is slower than First()


// ******************************
// First is preferred over Single
// Default Value is used when method returns null

// ======================================================

// Take : a specific amount of element from the beginning 

var take1 = (from prod in products 
             orderby prod.Color descending
             select prod)
             .Take(1)
             .ToList();
Customer.printArrayInfo(take1);
var takeOneItem2 = products.OrderBy(prod => prod.Id).Take(1).ToList();
Customer.printArrayInfo(takeOneItem2);

// Range operator .. : is used to specify the start and end of a range 
// Take(5..8), takes elements 6, 7, 8
// Take(..4) = 0 to 3
// Take(10..) = 11 to end 

var rangeQuery = (from prod in products select prod).Take(..2).ToList();
Console.WriteLine(" range");
//Customer.printArrayInfo(rangeQuery);
var rangeQuery2 = products.OrderBy(prod => prod.Name).Take(2).ToList();


var takeWhile = (from prod in products select prod).TakeWhile(prod => prod.StandardCost > 101).ToList();
var takeWhile2 = products.TakeWhile(prod => prod.StandardCost > 100).ToList();
//Customer.printArrayInfo(takeWhile2);

// Skip a specific amount of elements or while condition is true 

var skipElements = (from prod in products select prod).Skip(1).Take(1).ToList();
var skipElem = products.Skip(1).Take(1).ToList();

var skipWhile = (from prod in products select prod).SkipWhile(prod => prod.StandardCost < 200).ToList();
var skipWhile2 = products.SkipWhile(prod => prod.StandardCost >= 200).ToList();

// Distinct elements, not copy 

var disElem = (from prod in products select prod.Color).Distinct().ToList();
var distElem2 = products.Select(prod => prod.Color).Distinct().OrderByDescending(c => c).ToList();
foreach (var color in distElem2)
{
    Console.WriteLine("color: " + color);
}

var distObj = (from prod in products select prod).DistinctBy(prod => prod.Color).ToList();
var distObj2 = products.DistinctBy(prod => prod.Color).ToList();

// Partitioning data = break data in smaller collection

var chunkData = (from prod in products select prod).Chunk(1).ToList();
var chunkData2 = products.Chunk(2).ToList();
Console.WriteLine(chunkData2.Count);
//Customer.printArrayInfo(chunkData.Count);

// type of data in a connection , collection contains a certain item , all the items of the collection meet a condition , or any of the item meets the condition

// All meet condition
var allHaveName = (from prod in products select prod).All(prod => prod.Name.Contains("x"));
var allHaveName2 = products.All(item => item.Name.Contains("F"));
Console.WriteLine(allHaveName + "  " + allHaveName2);

// Any item meet condition

var anyItem = (from prod in products select prod).Any(prod => prod.StandardCost > 200);
var anyItem2 = products.Any(item => item.StandardCost >= 100);
Console.WriteLine(anyItem + " " + anyItem2);

//siple data collection = List<int>

// Contains 
var contains2 = (from num in new List<int> { 1, 2, 3} select num).Contains(3);

// Contains() = compare objects, by default is comparing the reference of the objects
// usualy, we want to compare the properties of the objects 
// to compare the objects in this way we need to create a class that inherits from EqualityComparer<T>
// we need to override the method Equals that accepts two objects 
// and in the method body we write the code for comparing the objects 

// overrite GetHashCode(Class obj) method and return an unic id for each object  
ProductStandardCostComparer pc = new ProductStandardCostComparer();
// containsLastPrice is a boolean that will be true if the collection contains the item searched  
bool containsLastPrice = (from prod in products select prod).Contains(new Product {  Id = 3, StandardCost = 200 }, pc);
Console.WriteLine(containsLastPrice);

bool containsLastPrice2 = products.Contains(new Product { Id = 2, StandardCost = 100 }, pc);


// Iterate over collection
// let is used to assigned values 
(from prod in products let temp = prod.ListPrice = prod.StandardCost * 2 select prod).ToList();
products.ForEach(prod => prod.ListPrice = prod.StandardCost * 2);

class ProductStandardCostComparer : EqualityComparer<Product>
{
    public override bool Equals(Product? x, Product? y)
    {
        return x?.StandardCost == y?.StandardCost;
    }

    public override int GetHashCode([DisallowNull] Product obj)
    {
        return obj.Id.GetHashCode();
    }
}


class Customer { 
    public string City;
    public string Name;

    public Customer(string name, string city)
    {
        this.Name = name;
        this.City = city;
    }
    public static void printArrayInfo(List<Product> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine("name in print function: " + product.Name + " standard cst: " + product.StandardCost);
        }
    }
}

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal StandardCost { get; set; }
    public decimal ListPrice { get; set; }
    public string Size { get; set; }
}