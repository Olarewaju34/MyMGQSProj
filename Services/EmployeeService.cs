using MGQSSimpleEmployeeAppFile.Constants;
using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;
using SimpleEmployeeApp.Repository;
using SimpleEmployeeApp.Shared;
using System;

namespace SimpleEmployeeApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository();
        }

        public void Create(EmployeeDto request)
        {
            var employees = employeeRepository.GetAll();

            int id = (employees.Count != 0) ? employees[employees.Count - 1].Id + 1 : 1;
            string code = Helper.GenerateCode(id);
            DateTime dateJoined = DateTime.Now;

            Console.Write("Enter employee firstname: ");
            request.FirstName = Console.ReadLine();

            Console.Write("Enter employee lastname: ");
            request.LastName = Console.ReadLine();

            Console.Write("Enter employee middlename: ");
            request.MiddleName = Console.ReadLine();

            Console.Write("Enter employee email: ");
            request.Email = Console.ReadLine();

            Console.Write("Enter employee phone number: ");
            request.Phone = Console.ReadLine();
            request.Password = request.Phone;

            int role = Helper.SelectEnum("Enter employee role: \nEnter 1 for Admin\nEnter 2 for Security\nEnter 3 for Cleaner\nEnter 4 for Manager: ", 1, 4);
            request.Role = (Role)role;

            int gender = Helper.SelectEnum("Enter employee gender:\nEnter 1 for Male\nEnter 2 for Female\nEnter 3 for RatherNotSay: ", 1, 3);
            request.Gender = (Gender)gender;

            var employee = new Employee
            {
                Id = id,
                Code = code,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                Role = request.Role,
                Gender = request.Gender,
                DateJoined = dateJoined
            };

            var findEmployee = employeeRepository.GetByIdOrCode(employee.Id, employee.Code);

            if (findEmployee == null)
            {
                employees.Add(employee);
                employeeRepository.WriteToFile(employee);
                Console.WriteLine($"New employee with code \"{employee.Code}\" created successfully!");
            }
            else
            {
                Console.WriteLine($"Employee with {employee.Code} already exist!");
            }
        }

        public void Update(int id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = employeeRepository.GetById(id);

            Console.Write("Enter employee firstname: ");
            updateEmployeeDto.FirstName = Console.ReadLine();

            Console.Write("Enter employee lastname: ");
            updateEmployeeDto.LastName = Console.ReadLine();

            Console.Write("Enter employee middlename: ");
            updateEmployeeDto.MiddleName = Console.ReadLine();

            Console.Write("Enter employee phone: ");
            updateEmployeeDto.Phone = Console.ReadLine();

            Console.Write("Enter employee email: ");
            updateEmployeeDto.Email = Console.ReadLine();

            int newRole = Helper.SelectEnum("Enter employee role: \nEnter 1 for Admin\nEnter 2 for Security\nEnter 3 for Cleaner\nEnter 4 for Manager: ", 1, 4);
            updateEmployeeDto.Role = (Role)newRole;

            int newGender = Helper.SelectEnum("Enter employee gender: \nEnter 1 for Male\nEnter 2 for Female\nEnter 3for RatherNotSay: ", 1, 3);
            updateEmployeeDto.Gender = (Gender)newGender;

            if (employee != null)
            {
                employee.FirstName = updateEmployeeDto.FirstName;
                employee.LastName = updateEmployeeDto.LastName;
                employee.MiddleName = updateEmployeeDto.MiddleName;
                employee.Gender = updateEmployeeDto.Gender;
                employee.Role = updateEmployeeDto.Role;
                employee.Phone = updateEmployeeDto.Phone;
                employee.Email = updateEmployeeDto.Email;

                employeeRepository.RefreshFile();

                Console.WriteLine($"Employee record with code \"{employee.Code}\" is successfully updated!");
            }
            else
            {
                Console.WriteLine($"Employee not found!");
            }
        }

        public void Update(string code, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = employeeRepository.GetByCode(code);

            Console.Write("Enter employee firstname: ");
            updateEmployeeDto.FirstName = Console.ReadLine();

            Console.Write("Enter employee lastname: ");
            updateEmployeeDto.LastName = Console.ReadLine();

            Console.Write("Enter employee middlename: ");
            updateEmployeeDto.MiddleName = Console.ReadLine();

            Console.Write("Enter employee phone: ");
            updateEmployeeDto.Phone = Console.ReadLine();

            Console.Write("Enter employee email: ");
            updateEmployeeDto.Email = Console.ReadLine();

            if (employee != null)
            {
                employee.FirstName = updateEmployeeDto.FirstName;
                employee.LastName = updateEmployeeDto.LastName;
                employee.MiddleName = updateEmployeeDto.MiddleName;
                employee.Phone = updateEmployeeDto.Phone;
                employee.Email = updateEmployeeDto.Email;

                employeeRepository.RefreshFile();

                Console.WriteLine("Record updated successfully!");
            }
            else
            {
                Console.WriteLine("An error occured!");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var employee = employeeRepository.GetById(id);
                var employees = employeeRepository.GetAll();

                if (employee == null)
                {
                    Console.WriteLine($"Employee with the id: {id} not found");
                }

                if (employee.Id == 1)
                {
                    Console.WriteLine($"Record cannot be deleted!");
                }
                else
                {
                    employees.Remove(employee);
                    employeeRepository.RefreshFile();
                    Console.WriteLine($"Employee with the id: {id} successfully deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.Write($"An error occurred: {ex.Message}");
            }
        }

        public void PrintListView(Employee employee)
        {
            Console.WriteLine($"Employee Code: {employee.Code}\tFullname: {employee.LastName} {employee.FirstName} {employee.MiddleName}\tEmail: {employee.Email}\tGender: {employee.Gender}\tRole: {employee.Role}");
        }

        public void PrintDetailView(Employee employee)
        {
            Console.WriteLine($"Code: {employee.Code}\nFullname: {employee.LastName} {employee.FirstName} {employee.MiddleName}\nPhone: {employee.Phone}\nEmail: {employee.Email}\nRole: {employee.Role}\nGender: {employee.Gender}\nDate Joined: {employee.DateJoined}");
        }

        public void GetAll()
        {
            var employees = employeeRepository.GetAll();

            foreach (var employee in employees)
            {
                PrintListView(employee);
            }
        }

        public void GetAnEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);

            if (employee != null)
            {
                PrintDetailView(employee);
            }
            else
            {
                Console.WriteLine("Employee not found!");
            }
        }

        public void ChangePassword(string code, string oldPassword, string newPassword, string confirmPassword)
        {
            var employee = employeeRepository.GetByCode(code);

            if (employee == null)
            {
                Console.WriteLine($"Employee with the code: {code} not found");
                return;
            }

            if (employee.Password != oldPassword)
            {
                Console.WriteLine("Your current password is incorrect!");
                return;
            }

            if (newPassword != confirmPassword)
            {
                Console.WriteLine("Password mismatch!");
                return;
            }

            employee.Password = newPassword;
            employeeRepository.RefreshFile();
            Console.WriteLine("You successfully changed your password!");
        }

        public Employee Login(string code, string password)
        {
            var employee = employeeRepository.GetByCode(code);

            if (employee != null && employee.Password == password)
            {
                return employee;
            }

            return null;
        }

        public void AddAdminRecord()
        {
            var e = new Employee()
            {
                Id = 1,
                Code = "EMP-0001",
                FirstName = "Admin",
                LastName = "Boss",
                Email = "admin@admin.com",
                Password = "password",
                Phone = "08099889988",
                Gender = Gender.Male,
                Role = Role.Admin,
                DateJoined = DateTime.Now
            };
            employeeRepository.WriteToFile(e);
        }
    }
}
