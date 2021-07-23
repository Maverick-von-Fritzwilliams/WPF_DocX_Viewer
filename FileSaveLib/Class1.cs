using System.Text;
using System.IO;

namespace FileSaveLib
{
    public class FileSaveLib
    {
        public static readonly string file_list_path = @System.AppDomain.CurrentDomain.BaseDirectory + @"\File_List_Test.txt";
        public static void SaveFileList(string[] paths)
        {
            Stream file_stream = new FileStream(@file_list_path, FileMode.Create, FileAccess.Write);
            try
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    string strpath = paths[i] + "\n";
                    byte[] byte_path = Encoding.UTF8.GetBytes(strpath);
                    file_stream.Write(byte_path, 0, byte_path.Length);
                }
            }
            finally
            {
                file_stream.Close();
            }
        }
    }
}