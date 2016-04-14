using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class Teacher : IMentor
    {
        private ICourse course;

        public ICourse Course { get { return course;} }

        public void OpenCourse(CourseManager manager, int numberOfStudent, string name)
        {
            course = manager.CreateCourse(numberOfStudent,name, this);
        }

        public string CheckHomework(bool done)
        {
            Console.WriteLine("Check homework");
            if (done == true)
                return $"Course \"{Course.Name}\" is passed.";
            else
                return $"Course \"{Course.Name}\" isn't passed.";
        }

        public ICourse CloseCourse()
        {
            if (course.IsFinish == true)
                return null;
            else
            {
                return course;
            }
        }
    }
}
