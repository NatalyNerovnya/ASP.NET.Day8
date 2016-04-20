using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public interface IMentor
    {
        ICourse Course { get; }
        void OpenCourse(CourseManager manager);
        string CheckHomework(bool done);
        ICourse CloseCourse();
    }
}
