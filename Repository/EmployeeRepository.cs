using MGQSSimpleEmployeeAppFile.Constants;
using MySql.Data.MySqlClient;
using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;

namespace SimpleEmployeeApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private string connectionString = @"server=localhost;user=root;database=employeeDb;port=3306;password=Olarewaju112$";
        public MySqlConnection conn;
        public EmployeeRepository()
        {
            conn = new MySqlConnection();
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            using (conn = new(connectionString))
            {
                try
                {
                    // SQL Query to execute
                    // selecting only first 10 rows for demo
                    string sql = "SELECT * FROM employees;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    // read the data
                    while (rdr.Read())
                    {
                        Employee employee = new();
                        employee.Id = (int)rdr["EmployeeId"];
                        employee.Code = (string)rdr["EmployeeCode"];
                        employee.FirstName = (string)rdr["FirstName"];
                        employee.LastName = (string)rdr["LastName"];
                        employee.MiddleName = (string)rdr["MiddleName"];
                        employee.Phone = (string)rdr["Phone"];
                        employee.Email = (string)rdr["Email"];
                        employee.DateJoined = (DateTime)rdr["DateJoined"];
                        employees.Add(employee);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }

                return employees;
            }

        }
        public Employee GetByCode(string code)
        {
            // return employees.Find(i => i.Code == code);
            conn = new(connectionString);

            Employee employee = new();

            try
            {
                conn.Open();
                var sql = "SELECT * FROM employees where EmployeeCode = '" + code + "'";

                MySqlCommand command = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = command.ExecuteReader();

                if (rdr.Read())
                {
                    employee.Id = (int)rdr["EmployeeId"];
                    employee.Code = (string)rdr["EmployeeCode"];
                    employee.FirstName = (string)rdr["FirstName"];
                    employee.LastName = (string)rdr["LastName"];
                    employee.MiddleName = (string)rdr["MiddleName"];
                    employee.Phone = (string)rdr["Phone"];
                    employee.Email = (string)rdr["Email"];
                    employee.Password = (string)rdr["EmployeePassword"];
                    employee.DateJoined = (DateTime)rdr["DateJoined"];
                    
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return employee;
        }
        public Employee GetById(int id)
        {
            // return employees.Find(i => i.Id == id);
            conn = new(connectionString);

            Employee employee = null;

            try
            {
                conn.Open();
                var sql = "SELECT EmployeedId, EmployeeCode, FirstName, LastName, Phone FROM employees where EmployeeId = '" + id + "'";

                MySqlCommand command = new MySqlCommand(sql, conn);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    employee.Id = reader.GetInt32(1);
                    employee.Code = reader.GetString(2);
                    employee.FirstName = reader.GetString(3);
                    employee.LastName = reader.GetString(4);
                    employee.Phone = reader.GetString(5);

                    employee = new Employee();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return employee;
        }
        public bool CreateRecord(Employee employee)
        {
            conn = new(connectionString);

            try
            {
                Console.WriteLine("Connecting to MySql...");
                conn.Open();

                string query = "INSERT INTO employees(EmployeeCode, FirstName, LastName, MiddleName, Gender, Roles, Phone, Email, Password, DateJoined) values('" + employee.Code + "', '" + employee.FirstName + "', '" + employee.LastName + "', '" + employee.MiddleName + "', '" + employee.Gender + "', '" + employee.Role + "', '" + employee.Phone + "', '" + employee.Email + "', '" + employee.Password + "', '" + employee.DateJoined.ToString("yyyy-MM-dd") + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            conn.Close();
            return false;
        }
        public bool Update(Employee employee)
        {
            conn = new(connectionString);

            try
            {
                conn.Open();

                var sql = "UPDATE employees SET FirstName ='" + employee.FirstName + "', LastName ='" + employee.LastName + "', MiddleName ='" + employee.MiddleName + "', EmployeeRole ='" + employee.Role + "', DateJoined ='" + employee.DateJoined + "', WHERE EmployeeId ='" + employee.Id + "'";

                MySqlCommand command = new MySqlCommand(sql, conn);
                int count = command.ExecuteNonQuery();

                if (count > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            conn.Close();
            return false;
        }
        public bool Delete(int id)
        {
            conn = new(connectionString);

            try
            {
                Console.WriteLine("Connecting to MySql...");
                conn.Open();

                string query = $"DELETE FROM employees WHERE EmployeeId = {id}";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    conn.Close();
                    return true;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            conn.Close();
            return false;
        }
        public int EmployeeCount()
        {
            try
            {
                conn = new(connectionString);

                string sql = "SELECT count(*) FROM employees";

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    {
                        int totalEmployee = reader.GetInt32(0);
                        return totalEmployee;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return 0;
        }
    }
}