﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public interface IStudent
    {
        void RegisterOnCourse(ICourse course);
        bool ObserveCourse(ICourse course);
        bool DoHomework();
    }
}
