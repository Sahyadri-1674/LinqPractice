using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
    // Comparer to compare employees by ID (important!)
    class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.ID.GetHashCode();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 10, 3, 67, 45, 23, 100, 63, 38, 98, 68, 123, 340, 4, 40, 78 };

            // LINQ SYNTAX
            // from <alias> in < collection | array > [<clauses>] select <alias>

            var brr = from i in arr select i;

            //foreach (var i in arr)
            //{
            //   Console.WriteLine(i);
            //}

            //var brr1 = from i in arr where i > 40 select i;
            //var brr2 = from i in arr where i > 40 orderby i descending select i;
            
            //foreach (var i in brr1)
            //{
            //    Console.Write(i+" ");
            //}
            //Console.WriteLine();
            //foreach (var i in brr2)
            //{
            //    Console.Write(i + " ");
            //}

            // Linq to Sql - its a query language that is introduced in .net 3.5 framework for working with relational database MS SQL Server only.
            // Linq to sql is not only about querying the data but also allows us to perform Insert, Update and Delete operations also and we call them as CRUD operations.
            // CRUD: Create (Insert), Read (Select), Update, Delete

            // Note: we can also call stored procedures by using linq to sql.

            // Already there is a lang known as SQL using which we can interact with Sql Server with the help of ADO.Net.

            // in ADO.net Runtime Syntac Checking of Sql Statements ---> Database Engine does it not the c# compiler bcz it is written in strings which compiler doesn't care.
            // ADO.net --> It is not type safe.
            // No intellisense Support.
            // Debugging of Sql Statements is not possible.
            // Code is a combination of Object Oriented and Relational.

            // LINQ queries are checked in Compile Time by c# compiler. 
            // It is Type Safe.
            // Intellisense support is Available.
            // Debugging of Linq Sql is possible.
            // Pure Object Oriented Code.
            // ( We don't have Table --> its Class
            //              Columns -> Property
            //              Rows or Records -> Instance 
            //              Stored Procedures -> Methods     

            /*
             To work with linq to sql we need to convert all the relational objects of database into objet oriented types and this process is known ORM(Object Relational Mapping)
             
             */

            List<Employee> employees = DataManager.GetData();
            //foreach (Employee employee in employees)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}

            // To Print all emp.
            //employees.ForEach(employee => Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary));

            // Get all the names only distinct ones.
            //var names = employees.Select(e => e.FirstName).Distinct();

            //foreach(string name in names)
            //{
            //    Console.WriteLine(name);
            //}


            //employees =  employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();

            //foreach (Employee employee in employees)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}

            //employees = employees.OrderByDescending(e => e.FirstName).ThenBy(e => e.LastName).ToList();


            // WHERE condition returns a collection
            // employees = employees.Where(e => e.Age > 28 && e.Salary > 30000).ToList();

            //foreach (Employee employee in employees)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}

            //var FirstEmp = employees.First(e => e.FirstName == "Emily"); //It will throw exception if not found.
            //Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", FirstEmp.ID, FirstEmp.FirstName, FirstEmp.LastName, FirstEmp.Age, FirstEmp.Department, FirstEmp.Salary);

            //var FirstEmpOrDefault = employees.FirstOrDefault(e => e.FirstName == "Emily1");

            //if (FirstEmpOrDefault != null)
            //{
            //    Console.WriteLine("FirstEmpOrDefault ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", FirstEmpOrDefault.ID, FirstEmpOrDefault.FirstName, FirstEmpOrDefault.LastName, FirstEmpOrDefault.Age, FirstEmpOrDefault.Department, FirstEmpOrDefault.Salary);
            //}
            //else
            //    Console.WriteLine("Not found");

            //var LastEmp = employees.Last(e => e.FirstName == "Alice");
            //Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", LastEmp.ID, LastEmp.FirstName, LastEmp.LastName, LastEmp.Age, LastEmp.Department, LastEmp.Salary);

            //var LastEmpOrDefault = employees.LastOrDefault(e => e.FirstName == "Robert1");
            //if (LastEmpOrDefault != null)
            //{
            //    Console.WriteLine("LastEmpOrDefault ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", LastEmpOrDefault.ID, LastEmpOrDefault.FirstName, LastEmpOrDefault.LastName, LastEmpOrDefault.Age, LastEmpOrDefault.Department, LastEmpOrDefault.Salary);
            //}
            //else
            //    Console.WriteLine("Not Found");

            //var Single = employees.Single(e => e.FirstName == "Alice");
            //Console.WriteLine("Single ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", LastEmpOrDefault.ID, Single.FirstName, Single.LastName, Single.Age, Single.Department, Single.Salary);

            // var SingleOrDefault = employees.SingleOrDefault(e => e.FirstName == "Alice");
            // Console.WriteLine("SingleOrDefault ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", SingleOrDefault.ID, SingleOrDefault.FirstName, SingleOrDefault.LastName, SingleOrDefault.Age, SingleOrDefault.Department, SingleOrDefault.Salary);

            //var First2Employees = employees.Take(2);

            //foreach (var employee in First2Employees)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}

            //var SkipFirst2Employees = employees.Skip(2);
            //foreach (var employee in SkipFirst2Employees)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}

            //var DistinctEmployee = employees.Distinct();
            //foreach (var employee in DistinctEmployee)
            //{
            //    Console.WriteLine("ID:{0}, FirstName:{1},LastName: {2}, Age: {3}, Department: {4}, Salary: {5}", employee.ID, employee.FirstName, employee.LastName, employee.Age, employee.Department, employee.Salary);
            //}


            //// First set: Employees from IT and HR
            //var set1 = employees.Where(e => e.Department == "IT" || e.Department == "HR").ToList();

            //// Second set: Employees from HR and Finance
            //var set2 = employees.Where(e => e.Department == "HR" || e.Department == "Finance").ToList();

            //// Union → IT, HR, Finance (duplicates removed if comparer provided)
            //var unionResult = set1.Union(set2, new EmployeeComparer()).ToList();

            //// Intersect → Employees common in both sets (only HR here)
            //var intersectResult = set1.Intersect(set2, new EmployeeComparer()).ToList();

            //// Except → Employees in set1 but not in set2 (only IT here)
            //var exceptResult = set1.Except(set2, new EmployeeComparer()).ToList();

            //Console.WriteLine("---- UNION (IT + HR + Finance) ----");
            //foreach (var emp in unionResult)
            //    PrintEmployee(emp);

            //Console.WriteLine("\n---- INTERSECT (Common: HR) ----");
            //foreach (var emp in intersectResult)
            //    PrintEmployee(emp);

            //Console.WriteLine("\n---- EXCEPT (IT only) ----");
            //foreach (var emp in exceptResult)
            //    PrintEmployee(emp);


            //// Employees older than 30
            //var olderEmployees = employees.Where(e => e.Age > 30);

            //foreach (var emp in olderEmployees)
            //{
            //    Console.WriteLine($"{emp.FirstName} {emp.LastName} - Age: {emp.Age}");
            //}

            //ArrayList items = new ArrayList{ 1,"hello",2,3.5,"world",4};

            //// Get only integers
            //var numbers = items.OfType<int>();

            //foreach (var n in numbers)
            //{
            //    Console.WriteLine(n);
            //}

            //JoinsPractice.ExecuteJoin();
            //JoinsPractice.BasicUnderstanding();

            GroupByPractice.Execute();

        }



        static void PrintEmployee(Employee e)
        {
            Console.WriteLine($"ID:{e.ID}, FirstName:{e.FirstName}, Dept:{e.Department}");
        }

    }
    
}
