using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Elective
{
    /// <summary>
    /// Class for describing ordinary student
    /// </summary>
    public class Student : IStudent
    {
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
#endregion

        #region Fields
        private string name;
        private IArchive archive;
        #endregion


        #region Property
        public string Name
        {
            get
            {
                if(name == null)
                    throw new ArgumentNullException();
                logger.Trace($"Return name : {name}");
                return name;
            }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException();
                logger.Trace($"Set name : {value}");
                name = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="name">Name of student</param>
        public Student(string name)
        {
            if(name == null)
                throw new ArgumentNullException();
            logger.Trace("Create student");
            logger.Info($"Student name is {name}");
            Name = name;
        }
        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="name">Name of student</param>
        /// <param name="course">Ref on course</param>
        public Student(string name, ICourse course):this(name)
        {
            logger.Trace("Create student");
            logger.Info($"Student name is {name}. Course : {course.Name}");
            CheckCourse(course);
            RegisterOnCourse(course);
        }
        /// <summary>
        /// Create student
        /// </summary>
        /// <param name="name">Name of student</param>
        /// <param name="mentor">Ref on mentor, who has a course</param>
        public Student(string name, IMentor mentor):this(name)
        {
            if(mentor == null)
                throw new ArgumentNullException();
            logger.Trace("Create student");
            logger.Info($"Student name is {name}. Mentors' course : {mentor.Course.Name}");
            CheckCourse(mentor.Course);
            RegisterOnCourse(mentor.Course);
        }
        #endregion

        #region IStudent Implementation
        /// <summary>
        /// Observe beggining of course
        /// </summary>
        /// <returns>True, if homework is done</returns>
        public bool ObserveCourse()
        {
            logger.Trace($"{Name} is waiting for begining of course");
            return ((IStudent)this).DoHomework();
        }
        /// <summary>
        /// Registration on course
        /// </summary>
        /// <param name="course">Reference on course</param>
        public void RegisterOnCourse(ICourse course)
        {
            CheckCourse(course);
            if (!IsArchiveExist())
            {
                ArchiveManager manager = new FileArchiveManager();
                archive = manager.CreateArchive(this.Name);
            }
            logger.Trace($"{Name} is registered on {course.Name}");
            course.ObserveStudents(this, archive);
        }
        /// <summary>
        /// Do homework
        /// </summary>
        /// <returns>True, if homework is done</returns>
        bool IStudent.DoHomework()
        {
            //Какая-то логика, в каких случаях студент делает домашку. В моем случаи все студенты молодцы
            logger.Trace($"{Name} is doing homework");
            logger.Info("Return true - homework is done");
            return true;
        }
        #endregion

        #region Private Methods
        private void CheckCourse(ICourse course)
        {
            logger.Trace($"Check weather {course} is exist");
            if (course == null)
                throw new ArgumentNullException();
        }
        private bool IsArchiveExist()
        {
            logger.Trace($"Check weather archive for {Name} is exist");
            return ReferenceEquals(archive, null) ? false : true;
        }
#endregion
    }
}
