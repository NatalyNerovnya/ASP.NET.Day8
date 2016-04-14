using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class Student : IStudent
    {
        #region Fields
        private string name;
        private IArchive archive;
        #endregion

        #region Property
        public string Name
        {
            get { return name; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException();
                name = value;
            }
        }
        #endregion

        #region Constructors

        public Student(string name)
        {
            if(name == null)
                throw new ArgumentNullException();
            Name = name;
        }

        public Student(string name, ICourse course):this(name)
        {
            CheckCourse(course);
            RegisterOnCourse(course);
        }

        public Student(string name, IMentor mentor):this(name)
        {
            if(mentor == null)
                throw new ArgumentNullException();
            CheckCourse(mentor.Course);
            RegisterOnCourse(mentor.Course);
        }
        #endregion

        #region IStudent Implementation
        public bool ObserveCourse(ICourse course)
        {
            CheckCourse(course);
            return ((IStudent)this).DoHomework();
        }

        public void RegisterOnCourse(ICourse course)
        {
            CheckCourse(course);
            if (!IsArchiveExist())
            {
                ArchiveManager manager = new FileArchiveManager();
                archive = manager.CreateArchive(this.Name);
            }
            course.ObserveStudents(this, archive);
        }

        bool IStudent.DoHomework()
        {
            //Какая-то логика, в каких случаях студент делает домашку. В моем случаи все студенты молодцы
            Console.WriteLine($"{Name}.DoHomework");
            return true;
        }
        #endregion

        #region Private Methods
        private void CheckCourse(ICourse course)
        {
            if (course == null)
                throw new ArgumentNullException();
        }
        private bool IsArchiveExist()
        {
            return ReferenceEquals(archive, null) ? false : true;
        }
#endregion
    }
}
