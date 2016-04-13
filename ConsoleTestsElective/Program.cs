using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elective;

namespace ConsoleTestsElective
{
    class Program
    {
        static void Main(string[] args)
        {
            Teacher prof = new Teacher();
            prof.OpenCourse(new MathCourseManager(), 2);
            Student jim = new Student("Jim");
            jim.RegisterOnCourse(prof.Course);
            Student jill = new Student("Jill");
            jim.RegisterOnCourse(prof.Course);


        }
    }
}
