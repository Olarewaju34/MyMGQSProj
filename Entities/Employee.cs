using SimpleEmployeeApp.Enums;
using System;

namespace SimpleEmployeeApp.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateJoined { get; set; }

        public override string ToString()
        {
            return $"{Id}\t{Code}\t{FirstName}\t{LastName}\t{MiddleName}\t{Gender}\t{Role}\t{Phone}\t{Email}\t{Password}\t{DateJoined}";
        }

        public static Employee ToEmployee(string str)
        {
            var type = str.Split("\t");

            var employee = new Employee
            {
                Id = int.Parse(type[0]),
                Code = type[1],
                FirstName = type[2],
                LastName = type[3],
                MiddleName = type[4],
                Gender = Enum.Parse<Gender>(type[5]),
                Role = Enum.Parse<Role>(type[6]),
                Phone = type[7],
                Email = type[8],
                Password = type[9],
                DateJoined = DateTime.Parse(type[10])
            };
            return employee;
        }
    }
}
