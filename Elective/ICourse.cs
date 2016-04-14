using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public interface ICourse
    {
        string Name { get; }
        bool IsFinish { get; }
        void ObserveStudents(IStudent student, IArchive archive);
        void NotifyBeginOfCourse();
        string NotifyHometask(bool done);
        void SetMark(IArchive archive, string mark);
    }
}
