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
        /// <summary>
        /// Manager for file archive
        /// </summary>
        #region NLog field
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        #endregion
        #region Field(internal class)
        private class FileArchive : IArchive
        {
            /// <summary>
            /// File Archive
            /// </summary>
            #region Fields

            private readonly string path = Environment.CurrentDirectory;
            
            private static int unnameStudent = 1;
            #endregion

            #region Property
            string IArchive.Path { get { return path; } }
            #endregion

            #region Constructor
            /// <summary>
            /// Creation of File archive
            /// </summary>
            /// <param name="name">name of the file</param>
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
            /// <summary>
            /// Save data in archive
            /// </summary>
            /// <param name="str">Data to save</param>
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
        /// <summary>
        /// Creation of File archive
        /// </summary>
        /// <param name="name">name of the file</param>
        public override IArchive CreateArchive(string name = "")
        {
            logger.Trace($"(in IArchive CreateArchive)");
            return new FileArchive(name);
        }
        #endregion
    }
}
