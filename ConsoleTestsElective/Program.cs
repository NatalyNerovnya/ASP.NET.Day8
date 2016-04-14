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
            prof.OpenCourse(new MathCourseManager(),2,"Math Course");
            Student jim = new Student("Jimmy");
            jim.RegisterOnCourse(prof.Course);
            Student jill = new Student("Jill", prof.Course);
            prof.CloseCourse();
            prof.OpenCourse(new MathCourseManager(), 2, "Math Course. New chapters");
            jim.RegisterOnCourse(prof.Course);
            jill.RegisterOnCourse(prof.Course);
            prof.OpenCourse(new MathCourseManager(), 3, "Math Course. New chapters 2");
            jill.RegisterOnCourse(prof.Course);
            jim.RegisterOnCourse(prof.Course);
            Student jack = new Student("Jack", prof.Course);



        }
    }
}
