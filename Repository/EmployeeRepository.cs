using MGQSSimpleEmployeeAppFile.Constants;
using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;

namespace SimpleEmployeeApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public static List<Employee> employees;

        public EmployeeRepository()
        {
            employees = new List<Employee>();
            ReadFromFile();
        }

       
        public void WriteToFile(Employee employee)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(Constants.fullPath, true))
                {
                    write.WriteLine(employee.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RefreshFile()
        {
            try
            {
                using (StreamWriter write = new StreamWriter(Constants.fullPath))
                {
                    foreach (var a in employees)
                    {
                        write.WriteLine(a.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Employee> GetAll()
        {
            return employees;
        }

        public Employee GetByCode(string code)
        {
            return employees.Find(i => i.Code == code);
        }

        public Employee GetById(int id)
        {
            return employees.Find(i => i.Id == id);
        }

        public Employee GetByIdOrCode(int id, string code)
        {
            return employees.Find(i => i.Id == id || i.Code == code);
        }

        private void ReadFromFile()
        {
            try
            {
                if (File.Exists(Constants.fullPath))
                {
                    var lines = File.ReadAllLines(Constants.fullPath);

                    foreach (var line in lines)
                    {
                        var employee = Employee.ToEmployee(line);
                        employees.Add(employee);
                    }
                }
                else
                {
                    var dir = Constants.dir;
                    Directory.CreateDirectory(dir);
                    var fileName = Constants.filename;
                    var fullPath = Path.Combine(dir, fileName);
                    using (File.Create(fullPath))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}