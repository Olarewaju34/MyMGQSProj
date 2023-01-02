using SimpleEmployeeApp.Entities;

namespace SimpleEmployeeApp.Services
{
    public interface IEmployeeService
    {
        Employee Login(string code, string password);
        void Create(EmployeeDto request);
        void GetAll();
        void GetAnEmployee(int id);
        void Update(int id, UpdateEmployeeDto updateEmployeeDto);
        void Update(string code, UpdateEmployeeDto updateEmployeeDto);
        void Delete(int id);
        void PrintListView(Employee employee);
        void PrintDetailView(Employee employee);
        void ChangePassword(string code, string oldPassword, string newPassword, string confirmPassword);
        void SeedData();
    }
}
