using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPractice
{
    internal class GroupByPractice
    {
        class Student { 
        
            public int Id { get; set; }
            public string Name { get; set; }
            public string Course { get; set; }

            public int Marks { get; set; }
        }


        public static void Execute()
        {
            var students = new List<Student>
            {
                new Student {Id = 1, Name = "John", Course = "Math", Marks = 85 },
                new Student {Id = 2, Name = "Alice", Course = "Physics", Marks = 90 },
                new Student {Id = 3, Name = "David", Course = "Math", Marks = 75 },
                new Student {Id = 4, Name = "Sarah", Course = "Chemistry", Marks = 82 },
                new Student {Id = 5, Name = "Tom", Course = "Physics", Marks = 60 }
            };

            var groupedByCourse = students.GroupBy(s => s.Course);

            foreach (var group in groupedByCourse)
            {
                Console.WriteLine($"Course: {group.Key}");
                foreach(var student in group)
                {
                    Console.WriteLine($"  {student.Name} - {student.Marks}");
                }
            }

            // GroupBy with Aggregation

            var avgMarksByCourse = students.GroupBy(s => s.Course)
                                   .Select(g => new
                                   {
                                       Course = g.Key,
                                       AvgMarks = g.Average(x => x.Marks),
                                       StudentCount = g.Count()
                                   });
            /* Select lets you project each group (g) into a new shape (like an anonymous object). 
             Now, usually you don’t just want groups — you want to compute something from them, like:
            1. Average marks in each group
            2. Count of students per group
            3. Sum of marks per group

            That’s where Select comes in.
            
            Also 
            after the Select, you can only access Course, AvgMarks, and StudentCount.
            The other properties (Name, Marks, Id, …) are no longer carried forward — because you didn’t include them in the projection.
            You are projecting (Select) each group into a new anonymous type with properties {Course, AvgMarks, StudentCount}.

            So the final result (avgMarksByCourse) is an IEnumerable<anonymousType>.
             */
            foreach (var item in avgMarksByCourse)
                Console.WriteLine($"{item.Course}: Avg={item.AvgMarks}, Count={item.StudentCount}");

            var namesByCourse = students.GroupBy(s => s.Course, s => s.Name);
            /*
             Here you are using the overload of GroupBy that takes two selectors:

             The first selector: s => s.Course → this decides the key (TKey = string, course name).

             The second selector: s => s.Name → this decides the element type (TElement = string, student name).

             So now, instead of each group being an IGrouping<string, Student>,
             each group is an IGrouping<string, string>.

             That means:
             g.Key → the course name (string).
             g → a collection of student names (strings), not full student objects.
             */
            foreach (var group in namesByCourse)
            {
                Console.WriteLine($"Course: {group.Key}");
                foreach(var name in group)
                    Console.WriteLine($"  {name}");
            }

            var groupByCourseAndMarks = students.GroupBy(s => new { s.Course, Grade = s.Marks >= 80 ? "High" : "Low" });

            foreach (var group in groupByCourseAndMarks)
            {
                Console.WriteLine($"Course: {group.Key.Course}, Grade: {group.Key.Grade}");
                foreach( var s in group)
                {
                    Console.WriteLine($"   {s.Name} - {s.Marks}");
                }
            }

            var result = students
                        .GroupBy(s => new { s.Course, Grade = s.Marks >= 80 ? "High" : "Low" })
                        .Select(g => new
                        {
                            g.Key.Course,
                            g.Key.Grade,
                            Count = g.Count(),
                            AvgMarks = g.Average(x => x.Marks)
                        });

            foreach(var group in result)
            {
                Console.WriteLine($"Course-{group.Course} Grade-{group.Grade} Count-{group.Count} Average Marks - {group.AvgMarks}");
            }

            Console.WriteLine("-----------GroupBy with Custom Result Selector-------------");
            var summary = students.GroupBy(
                            s => s.Course,                     // key selector
                            s => s.Marks,                      // element selector
                            (course, marks) => new             // result selector
                            {
                               Course = course,
                               MaxMarks = marks.Max(),
                               MinMarks = marks.Min()
                            });

            foreach (var s in summary)
                Console.WriteLine($"{s.Course}: Max={s.MaxMarks}, Min={s.MinMarks}");


        }
    }
}
