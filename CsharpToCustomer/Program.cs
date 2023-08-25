using CustomersControllerProject;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

var connStr = "server=localhost\\sqlexpress;" +
    "database=SalesDb;" +
    "trusted_connection=true;" +
    "trustServerCertificate=true;";

var conn = new SqlConnection(connStr);
conn.Open();
if(conn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("Connection Did not Open!");
}
Console.WriteLine("Connection with SQL Server Open");
var custCtrl = new CustomersController(conn);
List<Customer> customers = custCtrl.GetCustomersByCity("Cincinnati");
if (customers == null)
{
    Console.WriteLine("No Customers Found");
}
else
{
    foreach (var customer in customers)
    {
        Console.WriteLine(customer.ToString());
    }
}
customers.Clear();

customers = custCtrl.GetAllCustomers();
foreach (var customer in customers)
{
    Console.WriteLine(customer.ToString());
}


conn.Close();