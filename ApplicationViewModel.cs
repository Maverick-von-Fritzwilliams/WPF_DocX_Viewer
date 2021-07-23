using System;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;
using NLog;

namespace WPF_DOCX_Viewer
{
    class ApplicationViewModel : IFilesDropped
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private File selectedFile;
        public ObservableCollection<File> File_list { get; set; }
        private readonly string file_list_path = @System.AppDomain.CurrentDomain.BaseDirectory + @"\File_List.txt";
        public File SelectedFile
        {
            get
            {
                return selectedFile;
            }
            set
            {
                selectedFile = value;
            }
        }
        public void SaveFileList() //Saving list of paths to documents to txt document
        {
            Stream file_stream = new FileStream(@file_list_path, FileMode.Create, FileAccess.Write);
            try
            {
                for (int i = 0; i < File_list.Count; i++)
                {
                    string strpath = File_list[i].Path + "\n";
                    byte[] byte_path = Encoding.UTF8.GetBytes(strpath);
                    file_stream.Write(byte_path, 0, byte_path.Length);
                }
                logger.Debug("File list saved");
            }
            finally
            {
                file_stream.Close();
            }
        }
        public ApplicationViewModel() //Loading paths to documents ftom txt document
        {
            File_list = new ObservableCollection<File>();
            Stream file_stream = new FileStream(@file_list_path, FileMode.OpenOrCreate, FileAccess.Read);
            file_stream.Close();
            string[] allpath = System.IO.File.ReadAllLines(@file_list_path);
            for(int i=0;i<allpath.Length;i++)
            {
                FileInfo file_info = new FileInfo(allpath[i]);
                if (file_info.Exists)
                    File_list.Add(new File(allpath[i]));
                else
                    logger.Warn($"Loaded file does not exists:{allpath[i]}");
            }
        }
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        File file = obj as File;
                        if (file != null)
                        {
                            File_list.Remove(file);
                            logger.Debug($"File removed from list:{file.Path}");
                            SaveFileList();
                        }
                    },
                    (obj) => File_list.Count > 0));
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Filter = "Microsoft Word Document(*.docx)|*.docx|Microsoft Word 97-2003 Document(*.doc)|*.doc|Text file(*.txt)|*.txt";
                        fileDialog.Multiselect = true;
                        fileDialog.CheckFileExists = true;
                        fileDialog.ShowDialog();
                        string[] files = fileDialog.FileNames;
                        for (int i = 0; i < files.Length; i++)
                        {
                            var file = new File(files[i]);
                            File_list.Insert(0, file);
                            SelectedFile = file;
                            logger.Debug($"File added to list:{file.Path}");
                        }
                        SaveFileList();
                    }));
            }
        }

        public void OnFilesDropped(string[] filepaths)
        {
            for (int i = 0; i < filepaths.Length; i++)
            {
                if (filepaths[i][filepaths[i].Length - 5] == '.' && filepaths[i][filepaths[i].Length - 4] == 'd' && filepaths[i][filepaths[i].Length - 3] == 'o' && filepaths[i][filepaths[i].Length - 2] == 'c' && filepaths[i][filepaths[i].Length - 1] == 'x'
                    || filepaths[i][filepaths[i].Length - 4] == '.' && filepaths[i][filepaths[i].Length - 3] == 'd' && filepaths[i][filepaths[i].Length - 2] == 'o' && filepaths[i][filepaths[i].Length - 1] == 'c'
                    || filepaths[i][filepaths[i].Length - 4] == '.' && filepaths[i][filepaths[i].Length - 3] == 't' && filepaths[i][filepaths[i].Length - 2] == 'x' && filepaths[i][filepaths[i].Length - 1] == 't')
                {
                    var file = new File(filepaths[i]);
                    File_list.Insert(0, file);
                    SelectedFile = file;
                    logger.Debug($"Dragged file added to list:{file.Path}");
                }
                else
                {
                    logger.Warn($"Dragged file does not supports{filepaths[i]}");
                }
            }
            SaveFileList();
        }
    }
}
