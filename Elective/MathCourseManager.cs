using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Elective
{
    public class MathCourseManager : CourseManager
    {
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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
                    logger.Trace($"Return name of course : {name}");
                    return name;
                }
                private set
                {
                    if (value == null)
                        throw new ArgumentNullException();
                    logger.Trace($"Set name of course : {value}");
                    name = value;
                }
            }

            public bool IsFinish
            {
                get
                {
                    logger.Trace($"Course is finished? {isFinish}");
                    return isFinish;
                }
                private set
                {
                    logger.Trace($"Course is finished? {value}");
                    isFinish = value;
                }
            }
            #endregion

            #region Constructors
            public MathCourse(int number, string name, IMentor mentor)
            {
                if (number < 0)
                    throw new AggregateException();
                if (name == null || mentor == null)
                    throw new ArgumentNullException();

                logger.Trace($"Create: course - {name}; number of students - {number}");
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
                logger.Trace($"Notufying beggining of {Name}");
                for (int i = 0; i < numberOfStudents; i++)
                {
                    logger.Trace($"Notify {group[i].Name}");
                    done = group[i].ObserveCourse(this);
                    logger.Trace($"Send homework of {group[i].Name} to teacher");
                    mark = NotifyHometask(done);
                    SetMark(archives[i], mark);
                }
                logger.Trace($"All homeroks is done. Finish the course");
                IsFinish = true;
            }

            public void SetMark(IArchive archive, string mark)
            {
                if (archive == null || mark == null)
                    throw new ArgumentNullException();
                logger.Trace($"Set mark({mark}) in archive");
                archive.SaveInfo(mark);
            }

            public string NotifyHometask(bool done)
            {
                if (teacher == null)
                    throw new ArgumentNullException();
                logger.Trace("Notify hometask");
                return teacher.CheckHomework(done);
            }

            public void ObserveStudents(IStudent student, IArchive studentArchive)
            {
                if (student == null || studentArchive == null)
                    throw new ArgumentNullException();
                logger.Trace("Looking after registration of students");
                if (group.Count < numberOfStudents)
                {
                    logger.Trace($"Add{student.Name} in group");
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
            logger.Trace($"(in ICourse CreateCourse)");
            return new MathCourse(number, name, mentor);
        }
        #endregion
    }
}
