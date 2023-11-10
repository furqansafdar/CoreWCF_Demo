// See https://aka.ms/new-console-template for more information

using ClientDomain;

var manager = new Manager("Mark");
manager.AddEmployee(new Employee("Smith"));
manager.AddEmployee(new Employee("Ross"));

var proxy = new ClientApp.Service_Contracts.MyService();

var result = await proxy.SaveApplication(manager);

Console.WriteLine(result.Name);