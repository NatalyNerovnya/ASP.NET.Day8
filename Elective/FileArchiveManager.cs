using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Elective
{
    public class FileArchiveManager : ArchiveManager
    {
        private class FileArchive : IArchive
        {
            private readonly string path =
                @"C:\Users\1\Documents\Visual Studio 2015\Projects\ASP.NET.Nerovnya.Day8\Archives";

            public FileArchive(string name)
            {
                path += $@"\{name}.txt";
                SaveInfo(name);
            }

            public void SaveInfo(string str)
            {
                StreamWriter write;
                FileInfo file = new FileInfo(path);
                write = file.AppendText();
                write.WriteLine(str);
                write.Close();
            }
        }

        public override IArchive CreateArchive(string name)
        {
            return new FileArchive(name);
        }
    }
}
