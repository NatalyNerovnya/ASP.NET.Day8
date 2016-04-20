using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Elective
{
    /// <summary>
    /// Class for describing Teacher:IMentor
    /// </summary>
    public class Teacher : IMentor
    {
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

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
                logger.Trace($"Return course {course.Name}");
                return course;
            }
        }

        #endregion

        #region IMentor Implementation
        /// <summary>
        /// Create a course
        /// </summary>
        /// <param name="manager">Course maanager</param>
        public void OpenCourse(CourseManager manager)
        {
            if(manager == null)
                throw new ArgumentNullException();
            if(manager.NumberOfStudents < 0 || manager.NumberOfStudents > maxNumberOfStudents)
                throw new AggregateException();
            logger.Trace($"Open course {manager.Name} with group of {manager.NumberOfStudents} students");
            course = manager.CreateCourse();
        }
        /// <summary>
        /// Close course
        /// </summary>
        /// <returns>Course, if it stil hadn't finished</returns>
        public ICourse CloseCourse()
        {
            if (course.IsFinish == true)
            {
                logger.Info($"Closing {course.Name} course");
                return null;
            }
            else
            {
                logger.Info($"Trying to close not finished {course.Name}");
                return course;
            }
        }

        string IMentor.CheckHomework(bool done)
        {
            logger.Trace($"Mentor check homework");
            if (done == true)
            {
                logger.Info("Course is passed");
                return $"Course \"{Course.Name}\" is passed.";
            }
            else
            {
                logger.Info("Course is passed");
                return $"Course \"{Course.Name}\" isn't passed.";
            }
        }
#endregion

    }
}
