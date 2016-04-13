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
                //создай файл и сделай проверку на повторение файлов
                path += $@"\{name}.txt";
                File.AppendAllText(path, $"{name}\n");

            }

            public void SaveInfo(string mark)
            {
                File.AppendAllText(path, mark);
            }
        }

        public override IArchive CreateArchive(string name)
        {
            return new FileArchive(name);
        }
    }
}
