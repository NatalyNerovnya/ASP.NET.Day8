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
        #region Field(internal class)
        private class FileArchive : IArchive
        {
            #region Fields
            private readonly string path =
                @"C:\Users\1\Documents\Visual Studio 2015\Projects\ASP.NET.2.Nerovnya.Day8\Archives";

            private static int unnameStudent = 1;
            #endregion

            #region Property
            string IArchive.Path { get { return path; } }
            #endregion

            #region Constructor
            public FileArchive(string name)
            {
                if (name == "" || name == null)
                    name = $"Student{unnameStudent++}";

                path += $@"\{name}.txt";
                SaveInfo(name);
            }
            #endregion

            #region IArchive implementation
            public void SaveInfo(string str)
            {
                if (str == null)
                    throw new ArgumentNullException();
                StreamWriter write;
                FileInfo file = new FileInfo(path);
                write = file.AppendText();
                write.WriteLine(str);
                write.Close();
            }
            #endregion

        }
        #endregion

        #region ArchiveManager implementation
        public override IArchive CreateArchive(string name = "")
        {
            return new FileArchive(name);
        }
        #endregion
    }
}
