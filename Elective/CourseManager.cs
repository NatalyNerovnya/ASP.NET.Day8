using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public abstract class CourseManager
    {
        public abstract ICourse CreateCourse(int number,string name, IMentor mentor);
    }
}
