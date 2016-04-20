using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public abstract class CourseManager
    {
        public string Name { get; private set; }
        public int NumberOfStudents { get; private set; }
        public IMentor Mentor { get; private set; }

        public CourseManager(string name, int number, IMentor mentor)
        {
            if (number <= 0)
                throw new ArgumentException();
            if (name == null || mentor == null)
                throw new ArgumentNullException();
            Name = name;
            NumberOfStudents = number;
            Mentor = mentor;
        }
        public abstract ICourse CreateCourse();
    }
}
