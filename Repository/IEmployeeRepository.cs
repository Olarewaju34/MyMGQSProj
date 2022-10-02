using SimpleEmployeeApp.Entities;
using System.Collections.Generic;

namespace SimpleEmployeeApp.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetByIdOrCode(int id, string code);
        Employee GetById(int id);
        Employee GetByCode(string code);
        List<Employee> GetAll();
        void WriteToFile(Employee employee);
        void RefreshFile();
    }
}