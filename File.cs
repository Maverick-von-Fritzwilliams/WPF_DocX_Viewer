using System;
using System.IO;
using System.Text;
using NLog;
using Spire.Doc;
using Spire.Doc.Documents;

namespace WPF_DOCX_Viewer
{ 
    class File
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        private string name;
        private string autor;
        private string path;
        private string size;
        private DateTime change_time;
        private string text;
        private string properties;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public string Autor
        {
            get { return autor; }
            set
            {
                autor = value;
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }
        public string Size
        {
            get { return size; }
            set
            {
                size = value;
            }
        }
        public DateTime Change_Time
        {
            get { return change_time; }
            set
            {
                change_time = value;
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
            }
        }
        public string Properties
        {
            get { return properties; }
            set
            {
                properties = value;
            }
        }
        public static string Size_ToStr(long size) //Converting long to string and bytes to Kb/Mb
        {
            float size_float;
            if (size >= 1000)
            {
                size_float = (float)Math.Round((float)size / 1000,1);
                if (size_float >= 1000)
                {
                    size_float = (float)Math.Round(size_float / 1000, 1);
                    return size_float.ToString() + "MB";
                }
                else
                    return size_float.ToString() + "KB";
            }
            else
                return size.ToString() + "B";
        }

        public File(string path)
        {
            try
            {
                Path = path;
                logger.Debug($"Loading and extracting data from document:{Path}");

                FileInfo file_info = new FileInfo(@path);
                Name = file_info.Name;
                long size = file_info.Length;
                Size = Size_ToStr(size);

                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(@path);
                logger.Debug("File loaded");
                Autor = document.BuiltinDocumentProperties.Author;
                Change_Time = document.BuiltinDocumentProperties.LastSaveDate;

                StringBuilder text = new StringBuilder();
                foreach (Section section in document.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                        text.AppendLine(paragraph.Text);
                }
                Text = text.ToString();

                Properties = "Path: " + Path + "\nAuthor: " + Autor + "\nSize: " + Size + "\nChange date:" + Change_Time.ToString();
                logger.Info($"Text and properties extracted from file({Path})");
            }
            catch(ArgumentNullException)
            {
                logger.Error("Nullable file path");
            }
            catch(ArgumentException)
            {
                logger.Error($"Invalid file path:{Path}");
            }
            catch(UnauthorizedAccessException)
            {
                logger.Error($"Restricted file access:{Path}");
            }
        }
    }
}