using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class MathCourseManager : CourseManager
    {
        #region Field(internal class)
        private class MathCourse : ICourse
        {
            #region Fields
            private List<IStudent> group;
            private List<IArchive> archives;
            private int numberOfStudents;
            private IMentor teacher;
            private bool isFinish = false;
            private string name;
            #endregion

            #region Properties
            public string Name
            {
                get
                {
                    if (name == null)
                        throw new ArgumentNullException();
                    return name;
                }
                private set
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    name = value;
                }
            }

            public bool IsFinish
            {
                get { return isFinish; }
                private set { isFinish = value; }
            }
            #endregion

            #region Constructors
            public MathCourse(int number, string name, IMentor mentor)
            {
                if (number < 0)
                    throw new AggregateException();
                if (name == null || mentor == null)
                    throw new ArgumentNullException();

                teacher = mentor;
                numberOfStudents = number;
                Name = name;
                group = new List<IStudent>();
                archives = new List<IArchive>();
            }
            #endregion

            #region ICourse Implementation
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
            }

            public void SetMark(IArchive archive, string mark)
            {
                if (archive == null || mark == null)
                    throw new ArgumentNullException();
                archive.SaveInfo(mark);
            }

            public string NotifyHometask(bool done)
            {
                if (teacher == null)
                    throw new ArgumentNullException();
                return teacher.CheckHomework(done);
            }

            public void ObserveStudents(IStudent student, IArchive studentArchive)
            {
                if (student == null || studentArchive == null)
                    throw new ArgumentNullException();

                if (group.Count < numberOfStudents)
                {
                    group.Add(student);
                    archives.Add(studentArchive);
                }
                if (group.Count == numberOfStudents)
                    NotifyBeginOfCourse();
            }
            #endregion
        }
        #endregion

        #region CourseManager Implementation
        public override ICourse CreateCourse(int number, string name, IMentor mentor)
        {
            if(number < 0)
                throw new AggregateException();
            if(name == null || mentor == null)
                throw new ArgumentNullException();

            return new MathCourse(number, name, mentor);
        }
        #endregion
    }
}
