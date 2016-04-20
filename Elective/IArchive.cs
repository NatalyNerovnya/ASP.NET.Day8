using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elective
{
    public interface IArchive
    {
        void SaveInfo(string mark);
        string Path { get; }
    }
}
