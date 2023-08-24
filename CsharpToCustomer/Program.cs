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
List<Customer> customers = custCtrl.GetAllCustomers();
foreach(var cust in customers)
{
    Console.WriteLine(cust.ToString());
}

List<Customer>? customers2 = custCtrl.CusPartialName("C");
if (customers2 == null)
{
    Console.WriteLine("No Customers found!");
}
else
{
    foreach(var cust in customers2)
    {
        Console.WriteLine(cust.ToString());
    }
}

conn.Close();