using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
    internal class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
    }

    internal static class DataManager

    {
        internal static List<Employee> GetData()
        {
            return new List<Employee>
            {
                new Employee { ID = 1, FirstName = "John", LastName = "Smith", Age = 30, Department = "HR", Salary = 25000},
                new Employee { ID = 2, FirstName = "Alice", LastName = "Johnson", Age = 28, Department = "Finance", Salary = 32000 },
                new Employee { ID = 3, FirstName = "Robert", LastName = "Brown", Age = 45, Department = "IT", Salary = 50000 },
                new Employee { ID = 4, FirstName = "Emily", LastName = "Davis", Age = 35, Department = "Marketing", Salary = 42000 },
                new Employee { ID = 5, FirstName = "Michael", LastName = "Wilson", Age = 50, Department = "Operations", Salary = 55000 },
                new Employee { ID = 6, FirstName = "Jessica", LastName = "Smith", Age = 26, Department = "HR", Salary = 26000 },
                new Employee { ID = 7, FirstName = "David", LastName = "Anderson", Age = 40, Department = "Finance", Salary = 47000 },
                new Employee { ID = 8, FirstName = "Sarah", LastName = "Thomas", Age = 29, Department = "IT", Salary = 39000 },
                new Employee { ID = 9, FirstName = "Daniel", LastName = "Moore", Age = 33, Department = "Sales", Salary = 31000 },
                new Employee { ID = 10, FirstName = "Laura", LastName = "Martin", Age = 38, Department = "Marketing", Salary = 43000 },
                new Employee { ID = 11, FirstName = "Kevin", LastName = "Brown", Age = 31, Department = "IT", Salary = 38000},
                new Employee { ID = 12, FirstName = "John", LastName = "Hall", Age = 42, Department = "Sales", Salary = 36000},
                 new Employee { ID = 13, FirstName = "Alice", LastName = "King", Age = 28, Department = "QA", Salary = 15000},
            };
        }
    }
}
