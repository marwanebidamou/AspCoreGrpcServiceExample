// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using GrpcService1.Protos;




var channel = GrpcChannel.ForAddress("https://localhost:7042");


//1st example
var client = new Greeter.GreeterClient(channel);

var input = new HelloRequest { Name = "Marwan" };
var reply = await client.SayHelloAsync(input);

Console.WriteLine($"Hello: {reply}");


//2nd example
var client2 = new Customer.CustomerClient(channel);

Console.WriteLine("Enter the id of the customer (1,2 or 3)");
var id = int.Parse(Console.ReadLine());

var result = await client2.GetCustomerInfoAsync(new CustomerLookupModel { UserId = id });
Console.WriteLine($"name: {result.FirstName} {result.LastName}");
Console.WriteLine($"email: {result.EmailAddress}");

Console.WriteLine();
Console.WriteLine("********* new customer list ***********");
Console.WriteLine();

//3rd example
using (var call = client2.GetNewCustomer(new NewCustomerRequest()))
{
    while(await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"current customer =>name: {currentCustomer.FirstName} {currentCustomer.LastName} - {currentCustomer.EmailAddress}");
    }
}

Console.ReadLine();