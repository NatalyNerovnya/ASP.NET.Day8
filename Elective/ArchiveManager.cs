using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public abstract class ArchiveManager
    {
        public abstract IArchive CreateArchive(string name);
    }
}
