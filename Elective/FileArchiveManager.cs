using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace Elective
{
    public class FileArchiveManager : ArchiveManager
    {
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion
        #region Field(internal class)
        private class FileArchive : IArchive
        {
            #region Fields

            private readonly string path = Environment.CurrentDirectory;
            
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
                logger.Info($"Creating archive with path : {path}");
                SaveInfo(name);
            }
            #endregion

            #region IArchive implementation
            public void SaveInfo(string str)
            {
                if (str == null)
                    throw new ArgumentNullException();
                logger.Trace($"Write \"{str}\" in archive");
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
            logger.Trace($"(in IArchive CreateArchive)");
            return new FileArchive(name);
        }
        #endregion
    }
}
