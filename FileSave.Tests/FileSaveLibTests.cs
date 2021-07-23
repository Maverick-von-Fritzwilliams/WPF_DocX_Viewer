using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSaveLib.Tests
{

        [TestClass()]
        public class FileSaveTests
        {
            [TestMethod()]
            public void SaveFileList_startpath_startpath()
            {
                // arrange
                string[] start = { @"C:\testdirectory\testfile.test", @"D:\testfile.test" };

                // act
                FileSaveLib.SaveFileList(start);
                string[] m_actual = System.IO.File.ReadAllLines(@FileSaveLib.file_list_path); ;

                string actual = "", expected = "";
                for (int i = 0; i < start.Length; i++)
                    expected += start[i];
                for (int i = 0; i < m_actual.Length; i++)
                    actual += m_actual[i];

                // assert
                Assert.AreEqual(expected, actual);
            }
        }
}