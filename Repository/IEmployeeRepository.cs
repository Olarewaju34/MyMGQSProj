using SimpleEmployeeApp.Entities;

namespace SimpleEmployeeApp.Repository
{
     public interface IEmployeeRepository
    {
        Employee GetById(int id);
        Employee GetByCode(string code);
        List<Employee> GetAll();
        bool CreateRecord(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
        int EmployeeCount();
    }
}



