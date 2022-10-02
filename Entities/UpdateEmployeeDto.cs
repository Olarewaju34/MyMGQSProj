using SimpleEmployeeApp.Enums;

namespace SimpleEmployeeApp.Entities
{
    public class UpdateEmployeeDto
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string Email {get; set;}
        public string Phone {get; set;}
    }
}