using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public class Student : IStudent
    {
        private string name;
        private IArchive archive;

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

        public Student(string name)
        {
            Name = name;
        }

        public Student(string name, ICourse course):this(name)
        {
            RegisterOnCourse(course);
        }

        public bool ObserveCourse(ICourse course)
        {
            return DoHomework();
        }

        public bool DoHomework()
        {
            //Какая-то логика на то, в каких случаях студент делает домашку
            Console.WriteLine($"{Name}.DoHomework");
            return true;
        }

        public void RegisterOnCourse(ICourse course)
        {
            if (!IsArchiveExist())
            {
                ArchiveManager manager = new FileArchiveManager();
                archive = manager.CreateArchive(this.Name);
            }
            course.ObserveStudents(this, archive);
        }

        private bool IsArchiveExist()
        {
            return ReferenceEquals(archive, null) ? false : true;
        }
    }
}
