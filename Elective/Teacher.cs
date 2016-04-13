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

        public void OpenCourse(CourseManager manager, int numberOfStudent)
        {
            course = manager.CreateCourse(numberOfStudent, this);
        }

        public string CheckHomework(bool done)
        {
            Console.WriteLine("Check homework");
            if (done == true)
                return $"{nameof(course).ToString()} is passed";
            else
                return $"{nameof(course).ToString()} isn't passed";
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
