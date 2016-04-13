using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class MathCourseManager : CourseManager
    {
        private class MathCourse : ICourse
        {
            private List<IStudent> group;
            private List<IArchive> archives; 
            private int numberOfStudents;
            private IMentor teacher;
            private bool isFinish = false;

            public bool IsFinish
            {
                get { return isFinish;}
                private set { isFinish = value; }
            }

            public MathCourse(int number, IMentor mentor)
            {
                teacher = mentor;
                numberOfStudents = number;
                group = new List<IStudent>();
                archives = new List<IArchive>();
            }

            public void NotifyBeginOfCourse()
            {
                bool done;
                string mark;
                for (int i = 0; i < numberOfStudents; i++)
                {
                   done = group[i].ObserveCourse(this);
                    mark = NotifyHometask(done);
                    SetMark(archives[i], mark);
                }
                IsFinish = true;
                Console.WriteLine("Finish course, but not close");
            }

            public void SetMark(IArchive archive, string mark)
            {
                archive.SaveInfo(mark);
            }

            public string NotifyHometask(bool done)
            {
                return teacher.CheckHomework(done);
            }


            public void ObserveStudents(IStudent student, IArchive studentArchive)
            {
                if (group.Count < numberOfStudents)
                {
                    group.Add(student);
                    archives.Add(studentArchive);
                }
                    if (group.Count == numberOfStudents)
                    NotifyBeginOfCourse();
            }
        }

        public override ICourse CreateCourse(int number, IMentor mentor)
        {
            return new MathCourse(number, mentor);
        }
    }
}
