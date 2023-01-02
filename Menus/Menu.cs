using MGQSSimpleEmployeeAppFile.Constants;
using SimpleEmployeeApp.Entities;
using SimpleEmployeeApp.Enums;
using SimpleEmployeeApp.Repository;
using SimpleEmployeeApp.Services;
using System;

namespace SimpleEmployeeApp.Menus
{
    public class Menu
    {
        private static IEmployeeService employeeService;
        private static EmployeeDto employeeDto; // a field injection
        private static UpdateEmployeeDto updateEmployeeDto;
        public Menu() //is the best form of injection to a class
        {
            updateEmployeeDto = new UpdateEmployeeDto();
            employeeService = new EmployeeService();
            employeeDto = new EmployeeDto();
        }
        public void MyMenu()
        {
            var flag = true;

            while (flag)
            {
                PrintMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Write("Enter your employee code: ");
                        var code = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        var password = Console.ReadLine();

                        var employee = employeeService.Login(code, password);
                        if (employee == null)
                        {
                            Console.WriteLine("Invalid code or password!");
                        }
                        else
                        {
                            if (employee.Role == Role.Admin)
                            {
                                AdminMenu();
                            }
                            else if (employee.Role == Role.SubAdmin)
                            {
                                SubAdminMenu();
                            }
                            else
                            {
                                StaffMenu(employee);
                            }
                        }
                        break;
                    case "0":
                        flag = false;
                        Console.WriteLine("Thank you for using our App...");
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }
        public void AdminMenu()
        {
            var flag = true;

            while (flag)
            {
                PrintAdminMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        employeeService.Create(employeeDto);
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        employeeService.GetAll();
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.Write("Enter the id of employee to view: ");
                        int viewId = int.Parse(Console.ReadLine());
                        employeeService.GetAnEmployee(viewId);
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        Console.Write("Enter employee Id to update: ");
                        int id = int.Parse(Console.ReadLine());
                        employeeService.Update(id, updateEmployeeDto);
                        Console.WriteLine("");
                        break;
                    case "5":
                        Console.WriteLine("");
                        Console.Write("Enter the id of employee to delete: ");
                        int empId = int.Parse(Console.ReadLine());
                        employeeService.Delete(empId);
                        Console.WriteLine("");
                        break;
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

       public void SubAdminMenu()
       {
           var flag = true;

            while (flag)
            {
                PrintSubAdminMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        employeeService.Create(employeeDto);
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        employeeService.GetAll();
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.Write("Enter the id of employee to view: ");
                        int viewId = int.Parse(Console.ReadLine());
                        employeeService.GetAnEmployee(viewId);
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        Console.Write("Enter employee Id to update: ");
                        int id = int.Parse(Console.ReadLine());
                        employeeService.Update(id, updateEmployeeDto);
                        Console.WriteLine("");
                        break;
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
       }
        public void StaffMenu(Employee employee)
        {
            var flag = true;

            while (flag)
            {
                PrintStaffMenu();
                Console.Write("\nPlease enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        employeeService.PrintDetailView(employee);
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        employeeService.Update(employee.Code, updateEmployeeDto);
                        Console.WriteLine("");
                        break;

                    case "3":
                        Console.WriteLine("");
                        Console.Write("Enter your old password: ");
                        var oldPassword = Console.ReadLine();
                        Console.Write("Enter your new password: ");
                        var newPassword = Console.ReadLine();
                        var confirmPassword = Console.ReadLine();
                        employeeService.ChangePassword(employee.Code, oldPassword, newPassword, confirmPassword);
                        Console.WriteLine("");
                        break;
                    case "0":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }
        public void PrintMenu()
        {
            employeeService.SeedData();
            Console.WriteLine("Enter 1 to login");
            Console.WriteLine("Enter 0 to exit");
        }
        public void PrintAdminMenu()
        {
            Console.WriteLine("Enter 1 to add new Employee.");
            Console.WriteLine("Enter 2 to view all employees.");
            Console.WriteLine("Enter 3 to view an employee.");
            Console.WriteLine("Enter 4 to update an employee.");
            Console.WriteLine("Enter 5 to delete an employee.");
            Console.WriteLine("Enter 0 to go back to main menu.");
        }
        public void PrintSubAdminMenu()
        {
            Console.WriteLine("Enter 1 to add new Employee.");
            Console.WriteLine("Enter 2 to view all employees.");
            Console.WriteLine("Enter 3 to view an employee.");
            Console.WriteLine("Enter 4 to update an employee.");
            Console.WriteLine("Enter 0 to go back to main menu.");
        }
        public void PrintStaffMenu()
        {
            Console.WriteLine("Enter 1 to view your details.");
            Console.WriteLine("Enter 2 to edit your profile.");
            Console.WriteLine("Enter 3 to change your password.");
            Console.WriteLine("Enter 0 to go back to main menu.");
        }
    }
}
