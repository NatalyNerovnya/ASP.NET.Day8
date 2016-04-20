using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Elective
{
    /// <summary>
    /// Manager for math course
    /// </summary>
    public class MathCourseManager : CourseManager
    {
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Field(internal class)
        /// <summary>
        /// Math course
        /// </summary>
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
            /// <summary>
            /// Create math course
            /// </summary>
            /// <param name="number">Max number of students in the group</param>
            /// <param name="name">Name of the course</param>
            /// <param name="mentor">Reference on mentor</param>
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
            /// <summary>
            /// Notify about beggining. Do and check hometask
            /// </summary>
            public void NotifyBeginOfCourse()
            {
                bool done;
                string mark;
                logger.Trace($"Notufying beggining of {Name}");
                for (int i = 0; i < numberOfStudents; i++)
                {
                    logger.Trace($"Notify {group[i].Name}");
                    done = group[i].ObserveCourse();
                    logger.Trace($"Send homework of {group[i].Name} to teacher");
                    mark = NotifyHometask(done);
                    SetMark(archives[i], mark);
                }
                logger.Trace($"All homeroks is done. Finish the course");
                IsFinish = true;
            }
            /// <summary>
            /// Send mark in archive
            /// </summary>
            /// <param name="archive">Student archive</param>
            /// <param name="mark">MArk</param>
            public void SetMark(IArchive archive, string mark)
            {
                if (archive == null || mark == null)
                    throw new ArgumentNullException();
                logger.Trace($"Set mark({mark}) in archive");
                archive.SaveInfo(mark);
            }
            /// <summary>
            /// Notify teacher about unchecked homework
            /// </summary>
            /// <param name="done">Homework</param>
            /// <returns>Mark</returns>
            public string NotifyHometask(bool done)
            {
                if (teacher == null)
                    throw new ArgumentNullException();
                logger.Trace("Notify hometask");
                return teacher.CheckHomework(done);
            }
            /// <summary>
            /// Observe registration of srudents
            /// </summary>
            /// <param name="student">Student</param>
            /// <param name="studentArchive">Students' archive</param>
            public void ObserveStudents(IStudent student, IArchive studentArchive)
            {
                if (student == null || studentArchive == null)
                    throw new ArgumentNullException();
                if (IsStudentInGroup(student))
                    return;
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

            #region Private Methods

            private bool IsStudentInGroup(IStudent student)
            {
                logger.Trace($"{student.Name} is already in group");
                for (int i = 0; i < group.Count; i++)
                {
                    if (ReferenceEquals(student, group[i]))
                        return true;
                }
                return false;
            } 
#endregion
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create instance of Math course manager
        /// </summary>
        /// <param name="number">Max number of students in the group</param>
        /// <param name="name">Name of the course</param>
        /// <param name="mentor">Reference on mentor</param>
        public MathCourseManager(int number, string name, IMentor mentor):base(name, number,mentor)
        {}
        #endregion

        #region CourseManager Implementation
        /// <summary>
        /// Create math course
        /// </summary>
        public override ICourse CreateCourse()
        {
           logger.Trace($"(in ICourse CreateCourse)");
            return new MathCourse(NumberOfStudents, Name, Mentor);
        }
        #endregion
    }
}
