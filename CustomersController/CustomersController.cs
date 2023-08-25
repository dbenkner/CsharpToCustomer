using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading.Channels;

namespace CustomersControllerProject
{
    public class CustomersController
    {
        public SqlConnection sqlConnection {  get; set; }

        public CustomersController(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }
        public List<Customer> GetAllCustomers()
        {
            var sql = "SELECT * FROM Customers;";
            var cmd = new SqlCommand(sql, sqlConnection);
            var reader = cmd.ExecuteReader();
            var customers = new List<Customer>();
            while (reader.Read())
            {
                customers.Add(ReturnCustomers(reader));
            }
            reader.Close();
            return customers;
        }
        public Customer? GetCustomerByName(string cusName)
        {
            var sql = "SELECT * From Customers where name = @Name";
            var cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Name", cusName);
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var cust = new Customer();
            cust = ReturnCustomers(reader);
            reader.Close();
            return cust;
        }
        public Customer? GetCustomerById(int id)
        {
            var sql = "Select * From Customers where ID = @Id";
            var cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var cust = new Customer();
            cust = ReturnCustomers(reader);
            reader.Close();
            return cust;
        }
        public void AddNewCustomer(Customer cust)
        {
            var sql = "INSERT INTO Customers (Name, City, State, Sales, Active) Values (@Name, @City, @State, @Sales, @Active) ";
            var cmd = new SqlCommand(sql, sqlConnection);
            //cmd.Parameters.AddWithValue("Id", 0);
            cmd.Parameters.AddWithValue("@Name", cust.Name);
            cmd.Parameters.AddWithValue("@City", cust.City);
            cmd.Parameters.AddWithValue("@State", cust.State);
            cmd.Parameters.AddWithValue("@Sales", cust.Sales);
            cmd.Parameters.AddWithValue("@Active", cust.Active);
            var rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                throw new Exception($"Insert failed! RA is {rowsAffected}");
            }
        }
        public void CustomerUpdate(Customer cust)
        {
            var sql = "UPDATE CUSTOMERS " + 
                " SET Name = @Name, " + " City = @City, " +
                " State = @State , " + " Sales = @Sales , " + " Active = @Active WHERE ID = @Id;";
            var cmd = new SqlCommand (sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Name", cust.Name);
            cmd.Parameters.AddWithValue("@City", cust.City);
            cmd.Parameters.AddWithValue("@State", cust.State);
            cmd.Parameters.AddWithValue("@Sales", cust.Sales);
            cmd.Parameters.AddWithValue("@Active", cust.Active);
            cmd.Parameters.AddWithValue("@Id", cust.Id);
            var rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                throw new Exception ($"Update Failed! RA is {rowsAffected}");
            }
        }
        public void DeleteCustomer(int Id)
        {
            var sql = "DELETE from Customers Where Id = @Id";
            var cmd = new SqlCommand(sql , sqlConnection);
            cmd.Parameters.AddWithValue("@Id", Id);
            var rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected != 1)
            {
                throw new Exception ($"Delete Failed! RA is {rowsAffected}");
            }
        }
        public List<Customer>? CusPartialName(string query)
        {
            query = query.ToLower();
            var sql = "SELECT * from customers where Name like '%'+@Name+'%' order by sales desc;";
            var cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Name", query);
            var reader = cmd.ExecuteReader();
            var custs = new List<Customer>();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            while (reader.Read())
            {
                custs.Add(ReturnCustomers(reader));
            }
            reader.Close();
            return custs;   
        }
        public List<Customer>? GetCustomersByCity(string query)
        {
            var sql = "Select * From Customers where City = @City";
            var cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@City", query);
            var reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                return null;
            }
            var Customers = new List<Customer>();
            while (reader.Read())
            {
                Customers.Add(ReturnCustomers(reader));
            }
            reader.Close();
            return Customers;
        }
        public Customer ReturnCustomers(SqlDataReader reader)
        {
            var cust = new Customer();
            cust.Id = Convert.ToInt32(reader["Id"]);
            cust.Name = Convert.ToString(reader["Name"]);
            cust.City = Convert.ToString(reader["City"]);
            cust.State = Convert.ToString(reader["State"]);
            cust.Sales = Convert.ToDecimal(reader["Sales"]);
            cust.Active = Convert.ToBoolean(reader["Active"]);
            return cust;
        }
    }
}