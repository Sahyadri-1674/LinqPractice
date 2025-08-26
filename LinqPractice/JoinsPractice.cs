using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
    class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }  // Foreign key (like SQL)
    }

    class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
    internal class JoinsPractice
    {
        public static void ExecuteJoin()
        {
            var students = new List<Student>
            {
                new Student { StudentId = 1, Name = "John", CourseId = 1 },
                new Student { StudentId = 2, Name = "Alice", CourseId = 2 },
                new Student { StudentId = 3, Name = "David", CourseId = 1 },
                new Student { StudentId = 4, Name = "Sarah", CourseId = 3 },
                new Student { StudentId = 5, Name = "Tom", CourseId = 5 }
            };

            var courses = new List<Course>
            {
                new Course { CourseId = 1, CourseName = "Mathematics" },
                new Course { CourseId = 2, CourseName = "Physics" },
                new Course { CourseId = 3, CourseName = "Chemistry" },
                new Course { CourseId = 4, CourseName = "Computer Science"}
            };

            var innerJoin = from s in students
                            join c in courses
                            on s.CourseId equals c.CourseId
                            select new
                            { 
                                StudentName = s.Name,
                                CourseName = c.CourseName
                            };

            Console.WriteLine("-------INNER JOIN ------");
            foreach(var item in innerJoin)
                Console.WriteLine($"{item.StudentName} - {item.CourseName}");

            var innerJoinExtensionMethod = students.Join(courses,
                                            s => s.CourseId, 
                                            c => c.CourseId,
                                            (s,c) => new {StudentName = s.Name, CourseName = c.CourseName});

            Console.WriteLine("-------INNER JOIN USING EXTENSION METHOD------");
            foreach (var item in innerJoinExtensionMethod)
                Console.WriteLine($"{item.StudentName} - {item.CourseName}");

            Console.WriteLine("-----------------GROUP JOIN------------------");
            var GroupJoin = from c in courses
                            join s in students
                            on c.CourseId equals s.CourseId into groupedStudents
                            select new
                            {
                                c.CourseId,
                                c.CourseName,
                                Students = groupedStudents
                            };

            foreach(var item in GroupJoin)
            {
                Console.WriteLine($"----{item.CourseName}");
                foreach (var student in item.Students)
                    Console.WriteLine($"{student.Name}");
            }

            Console.WriteLine("-----------------GROUP JOIN 2------------------");
            var GroupJoin2 = from s in students
                            join c in courses
                            on s.CourseId equals c.CourseId into groupedCourses
                            select new
                            {
                                s.CourseId,
                                s.Name,
                                Courses = groupedCourses
                            };

            foreach (var item in GroupJoin2)
            {
                Console.WriteLine($"----{item.Name}");
                foreach (var student in item.Courses)
                    Console.WriteLine($"{student.CourseName}");
            }
            var GroupJoinMethod = courses.GroupJoin(students, c => c.CourseId, s => s.CourseId,
                                  (course, groupedStudents) => new
                                  {
                                      CourseId = course.CourseId,
                                      CourseName = course.CourseName,
                                      Students = groupedStudents
                                  }
                                );
            Console.WriteLine("--------------GROUP JOIN USING METHOD-----------------");
            foreach (var item in GroupJoinMethod)
            {
                Console.WriteLine($"----{item.CourseName}");
                foreach (var student in item.Students)
                    Console.WriteLine($"{student.Name}");
            }

            var leftJoin = from s in students
                           join c in courses
                           on s.CourseId equals c.CourseId into studentCourses
                           from c in studentCourses.DefaultIfEmpty()
                           select new
                           {
                               StudentName = s.Name,
                               CourseName = c != null ? c.CourseName : "N/A"
                           };
            /* We are grouping the courses (inner sequence) for each student (outer sequence).
               i.e. Outer = Student, Group = Matching Courses.
               After into studentCourses, studentCourses is a collection of courses (per student).
            */
            Console.WriteLine("\n---- LEFT JOIN ----");
            foreach (var item in leftJoin)
                Console.WriteLine($"{item.StudentName} - {item.CourseName}");

            var leftJoin2 = from c in courses
                            join s in students
                            on c.CourseId equals s.CourseId into coursesStudent
                            from s in coursesStudent.DefaultIfEmpty()
                            select new {
                                StudentName = s != null ? s.Name : "N/A",
                                CourseName = c.CourseName // you can directly write c.CourseName bcz name is same as property name
                            };


            /*
             ⚖️ Compare this:
                If you started with courses as outer, you’d group students per course.
                Since you started with students (in leftJoin not leftJoin2), you grouped courses per student.
             */

            Console.WriteLine("\n---- LEFT JOIN 2----");
            foreach (var item in leftJoin2)
                Console.WriteLine($"{item.CourseName} - {item.StudentName}");


            
            Console.WriteLine("---------------Left Join Using Extension Method------------");
            var leftJoinExtension = students.GroupJoin(
                                    courses,                 // inner collection
                                    s => s.CourseId,         // outer key selector
                                    c => c.CourseId,         // inner key selector
                                    (s, courseGroup) => new { s, courseGroup } // group courses per student
                                    )
                                    .SelectMany(
                                        x => x.courseGroup.DefaultIfEmpty(), // expand group, insert null if empty
                                        (x, c) => new {
                                                  StudentName = x.s.Name,
                                                  CourseName = c != null ? c.CourseName : "N/A"
                                        }
                                    );

            foreach (var item in leftJoinExtension)
            {
                Console.WriteLine($"{item.StudentName} - {item.CourseName}");
            }

            Console.WriteLine("\n---- CROSS JOIN ----");

            var crossJoin = from s in students
                            from c in courses
                            select new
                            {
                                StudentName = s.Name,
                                CourseName = c.CourseName
                            };

            
            foreach (var item in crossJoin)
                Console.WriteLine($"{item.StudentName} - {item.CourseName}");
        }

        public static void BasicUnderstanding()
        {
            var numbers = new List<int> { 1, 2, 3 };

            // For each number, create a list of that number and its square
            var result2 = numbers.Select(n => new List<int> { n, n * n });
            /*
             numbers.Select(...) returns an IEnumerable<TResult> where TResult is the type returned by the lambda.

            Your lambda returns List<int>.
            Therefore: IEnumerable<List<int>> result2
             */
            foreach (var item in result2)
            {
                foreach(var i in item)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
            /*
             Step by step:
             1 → [1, 1]
             2 → [2, 4]
             3 → [3, 9]
             */

            var result = numbers.SelectMany(n => new List<int> { n, n * n });
            foreach (var n in result)
            {
                Console.WriteLine(n);
            }

            var names = new List<string> { "Alice", "Bob" };
            char[] chArr = names[0].ToCharArray();
            Console.Write("ChArr ");
            Console.WriteLine(chArr);

            /* 
                Console.WriteLine(char[]) → prints as string.
                Console.WriteLine(object[]) or int[] → prints the type name (e.g., System.Int32[]).
            */

            //foreach (var ch in chArr)
            //{
            //    Console.WriteLine(ch);
            //    Console.WriteLine(ch);
            //}
            // For each name, select its characters
            var charList = names.Select(name => name.ToCharArray()); /* Select always returns IEnumerable<T>, not List<T>.*/
            Console.WriteLine("Type of charList: {0}",charList.GetType());
            foreach(var item in charList)
            {
                Console.WriteLine(item);
            }
            var characters = names.SelectMany(name => name.ToCharArray());

            foreach (var c in characters)
            {
                Console.Write(c + " ");
            }

        }
    }
}
