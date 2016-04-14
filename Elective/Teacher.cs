using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class Teacher : IMentor
    {
        #region Fields
        private ICourse course;
        private static readonly int maxNumberOfStudents = 30;
        #endregion

        #region Property

        public ICourse Course
        {
            get
            {
                if(course == null)
                    throw new ArgumentNullException();
                return course;
            }
        }

        #endregion

        #region IMentor Implementation

        public void OpenCourse(CourseManager manager, int numberOfStudent, string name = "Unname")
        {
            if(manager == null)
                throw new ArgumentNullException();
            if(numberOfStudent < 0 || numberOfStudent > maxNumberOfStudents)
                throw new AggregateException();
            course = manager.CreateCourse(numberOfStudent,name, this);
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

        string IMentor.CheckHomework(bool done)
        {
            if (done == true)
                return $"Course \"{Course.Name}\" is passed.";
            else
                return $"Course \"{Course.Name}\" isn't passed.";
        }
#endregion

    }
}
