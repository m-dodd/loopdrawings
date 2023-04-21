using LoopDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoopDrawingDataUI
{
    public partial class frmLoopUI
    {
        private string GetShortPath(string path)
        {
            var splitPaths = Path.GetDirectoryName(path)?.Split(Path.DirectorySeparatorChar);
            if (splitPaths is not null)
            {
                int n = splitPaths.Length;
                if (n > 3)
                {
                    return
                        ".."
                        + Path.DirectorySeparatorChar
                        + string.Join(Path.DirectorySeparatorChar, splitPaths.TakeLast(2))
                        + Path.DirectorySeparatorChar

                        //+ Path.DirectorySeparatorChar
                        //+ Path.GetFileName(Path.GetDirectoryName(path))
                        //+ Path.DirectorySeparatorChar
                        + Path.GetFileName(path);
                }
            }

            return path;
        }

        private string GetFolderName()
        {
            string folderName = string.Empty;
            using (FolderBrowserDialog fld = new())
            {
                if (fld.ShowDialog() == DialogResult.OK)
                {
                    folderName = fld.SelectedPath;
                }
            }
            return folderName;
        }

        private void GetFolderSetLabel(Label label)
        {
            label.Text = GetFolderName();
        }

        private string GetFileName()
        {
            string fileName = string.Empty;
            using (OpenFileDialog openFileDialog1 = new())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;

                }
            }
            return fileName;
        }

        private string SetFileName()
        {
            string fileName = string.Empty;
            using (SaveFileDialog saveFileDialog1 = new())
            {
                saveFileDialog1.DefaultExt = ".json";
                saveFileDialog1.Title = "Save output drawing file json";
                saveFileDialog1.Filter = "json files (*.json)|*.json";
                saveFileDialog1.FilterIndex = 2;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                }
            }
            return fileName;
        }

        private bool FilesAndFoldersValid()
        {
            return
                !string.IsNullOrEmpty(configFileName)
                && !string.IsNullOrEmpty(templatePath)
                && !string.IsNullOrEmpty(outputResultPath)
                && !string.IsNullOrEmpty(outputDrawingPath)
                && ExcelHelper.IsExcelFile(excelFileName);
        }

        protected virtual bool IsFileLocked(string fileName)
        {
            return IsFileLocked(new FileInfo(fileName));
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch ( FileNotFoundException)
            {
                return false;
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
