using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public abstract class ArchiveManager
    {
        public string Name { get; private set; }

        public ArchiveManager(string name)
        {
            if(name == null)
                throw new ArgumentNullException();
            Name = name;
        }
        public abstract IArchive CreateArchive();
    }
}
