using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Net_Test
{
    public partial class Form2 : Form
    {

        const int DELETE = 0;
        const int MOVE = 1;
        const int SORT = 2;
        const int DUMMY_FILES = 3;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
            cbAction.SelectedIndex = 0;
        }

        private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAction.SelectedIndex == DELETE) {
                txtToDir.Text = txtFromDir.Text;
                txtFromDir.Text = "";
                lblToDir.Text = "Dir: ";
                lblFromDir.Visible = false;
                txtFromDir.Visible = false;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Delete";
            } else if (cbAction.SelectedIndex == MOVE) {
                txtFromDir.Text = txtToDir.Text;
                txtToDir.Text = "";
                lblFromDir.Text = "From Dir: ";
                lblToDir.Text = "To Dir: ";
                lblFromDir.Visible = true;
                txtFromDir.Visible = true;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Move";
            } else if (cbAction.SelectedIndex == SORT) {
                txtToDir.Text = txtFromDir.Text;
                txtFromDir.Text = "";
                lblToDir.Text = "Dir: ";
                lblFromDir.Visible = false;
                txtFromDir.Visible = false;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Sort";
            } else if (cbAction.SelectedIndex == DUMMY_FILES) {
                txtFromDir.Text = txtToDir.Text;
                txtToDir.Text = "";
                lblToDir.Text = "# of Files: ";
                lblFromDir.Text = "Dir: ";
                lblFromDir.Visible = true;
                txtFromDir.Visible = true;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Create Files";
            }
        }

        private void btnAction_Click(object sender, EventArgs e) {

            switch (cbAction.SelectedIndex) {
                case DELETE: Delete(); break;
                case MOVE: MoveFiles(); break;
                case SORT: Sort(); break;
                case DUMMY_FILES: CreateDummyFiles(); break;
            }
        }

        private void Delete() {
            String path = txtToDir.Text;

            if (path == "")
            {
                MessageBox.Show("The directory must be filled in.");
                return;
            }

            if (path.Substring(path.Length - 1) == " ")
            {
                MessageBox.Show("You put an extra space at the end. Please try again.");
                return;
            }

            if (!(path.Substring(path.Length - 1) == "\\"))
            {
                MessageBox.Show("A Directory must end with a \\. Please try again.");
                return;
            }

            if (!Directory.Exists(path))
            {
                MessageBox.Show(path + " doesn't exist.");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);

            Form3 progressDialog = new Form3();
            int numOfFiles = di.GetFiles().Length;
            int numOfDirs = di.GetDirectories().Length;
            int failedFiles = 0;
            int failedDir = 0;

            Thread backgroundThread = new Thread(new ThreadStart(() => {

                progressDialog.UpdateMax(numOfFiles + numOfDirs);
                int n = 0;

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch {
                        failedFiles++;
                    }

                    n++;
                    progressDialog.UpdateProgress(n);

                }

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception e)
                    {
                        failedDir++;
                        MessageBox.Show(e + "");
                    }

                    n++;
                    progressDialog.UpdateProgress(n);
                }
                progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));
            }));
            DialogResult dialogResult = MessageBox.Show("You are about to delete all files and directories in " + path + ". Are you sure you want to continue?", "Delete Approval", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                backgroundThread.Start();
                progressDialog.ShowDialog();
                MessageBox.Show(failedFiles + " files couldn't be deleted.\n" + failedDir + " directories couldn't be deleted.");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void MoveFiles() {

            string sourceDir = txtFromDir.Text;
            string destinationDir = txtToDir.Text;

            if (sourceDir == "" || destinationDir == "")
            {
                MessageBox.Show("You must have both directories filled in.");
                return;
            }

            if (sourceDir.Substring(sourceDir.Length - 1) == " ")
            {
                MessageBox.Show("You put an extra space at the end. Please try again.");
                return;
            }

            if (!(sourceDir.Substring(sourceDir.Length - 1) == "\\"))
            {
                MessageBox.Show("A Directory must end with a \\. Please try again.");
                return;
            }

            if (destinationDir.Substring(destinationDir.Length - 1) == " ")
            {
                MessageBox.Show("You put an extra space at the end. Please try again.");
                return;
            }

            if (!(destinationDir.Substring(destinationDir.Length - 1) == "\\"))
            {
                MessageBox.Show("A Directory must end with a \\. Please try again.");
                return;
            }

            if (!Directory.Exists(sourceDir))
            {
                MessageBox.Show("Source directory " + sourceDir + " does not exist.");
                return;
            }
            else if (!Directory.Exists(destinationDir))
            {
                MessageBox.Show("Destination directory " + destinationDir + " does not exist.");
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(sourceDir);

            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] directories = dir.GetDirectories();

            Form3 progressDialog = new Form3();
            progressDialog.UpdateMax(files.Length + directories.Length);

            Thread backgroundThread = new Thread(new ThreadStart(() =>
        {
            int n = 0;
            foreach (FileInfo file in files)
            {
                if (file.Name.Length > 0)
                {
                    // you can delete file here if you want (destination file)
                    if (File.Exists(destinationDir + file.Name))
                    {

                        File.Delete(destinationDir + file.Name);
                    }

                    // then copy the file here
                    file.MoveTo(destinationDir + file.Name);
                }
                n++;
                progressDialog.UpdateProgress(n);
            }

            foreach (DirectoryInfo directory in directories)
            {
                if (directory.Name.Length > 0)
                {
                    // you can delete file here if you want (destination file)
                    if (Directory.Exists(destinationDir + directory.Name))
                    {

                        Directory.Delete(destinationDir + directory.Name);
                    }

                    // then copy the file here
                    Directory.Move(sourceDir + directory.Name, destinationDir + directory.Name);
                }
                n++;
                progressDialog.UpdateProgress(n);
            }

            progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));
        }
    ));
            DialogResult dialogResult = MessageBox.Show("If the files/directories you are moving already exist in the desination directory, they will be overwritten. By clicking yes, you are allowing them to be overwritten.", "Overwrite Approval", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                backgroundThread.Start();
                progressDialog.ShowDialog();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void Sort() {
            string sourceDir = txtToDir.Text;

            if (sourceDir == "")
            {
                MessageBox.Show("The directory must be filled in.");
                return;
            }

            if (sourceDir.Substring(sourceDir.Length - 1) == " ")
            {
                MessageBox.Show("You put an extra space at the end. Please try again.");
                return;
            }

            if (!(sourceDir.Substring(sourceDir.Length - 1) == "\\"))
            {
                MessageBox.Show("A Directory must end with a \\. Please try again.");
                return;
            }

            if (!Directory.Exists(sourceDir))
            {
                MessageBox.Show("Source directory " + sourceDir + " does not exist.");
                return;
            }

            String[] picTypes = {".jpg", ".png", ".bmp", ".tiff", ".jpeg", ".gif"};
            String[] vidTypes = {".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv"};
            String[] docTypes = {".pdf", ".txt", ".xml", ".rtf", ".html", ".doc", ".docx", ".xls", ".xlsx"};
            String[] audTypes = { ".mp3", ".wav", ".wma", ".aac", ".flac", ".ogg", ".aiff", ".alac" };

            Directory.CreateDirectory(sourceDir + "Pictures");
            Directory.CreateDirectory(sourceDir + "Videos");
            Directory.CreateDirectory(sourceDir + "Documents");
            Directory.CreateDirectory(sourceDir + "Audio");

            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            FileInfo[] files = dir.GetFiles();

            int num_of_files = files.Length;
            int filesToSort = 0;
            int filesSorted = 0;
            Form3 progressDialog = new Form3();
            progressDialog.UpdateMax(num_of_files);

            Thread backgroundThread = new Thread(new ThreadStart(() => {

                List<FileInfo> picturesToSort = dir.GetFiles("*" + picTypes[0]).ToList();
                for(int i = 1; i < picTypes.Length; i++)
                {
                    FileInfo[] filesToAdd = dir.GetFiles("*" + picTypes[i]);
                    foreach(FileInfo file in filesToAdd)
                    {
                        picturesToSort.Add(file);
                        filesToSort++;
                    }
                }

                List<FileInfo> videosToSort = dir.GetFiles("*" + vidTypes[0]).ToList();
                for (int i = 1; i < vidTypes.Length; i++)
                {
                    FileInfo[] filesToAdd = dir.GetFiles("*" + vidTypes[i]);
                    foreach (FileInfo file in filesToAdd)
                    {
                        videosToSort.Add(file);
                        filesToSort++;
                    }
                }

                List<FileInfo> documentsToSort = dir.GetFiles("*" + docTypes[0]).ToList();
                for (int i = 1; i < docTypes.Length; i++)
                {
                    FileInfo[] filesToAdd = dir.GetFiles("*" + docTypes[i]);
                    foreach (FileInfo file in filesToAdd)
                    {
                        documentsToSort.Add(file);
                        filesToSort++;
                    }
                }

                List<FileInfo> audioToSort = dir.GetFiles("*" + audTypes[0]).ToList();
                for (int i = 1; i < audTypes.Length; i++)
                {
                    FileInfo[] filesToAdd = dir.GetFiles("*" + audTypes[i]);
                    foreach (FileInfo file in filesToAdd)
                    {
                        audioToSort.Add(file);
                        filesToSort++;
                    }
                }

                foreach(FileInfo picToSort in picturesToSort)
                {
                    if(File.Exists(sourceDir + "Pictures\\" + picToSort.ToString()))
                    {
                        File.Delete(sourceDir + "Pictures\\" + picToSort.ToString());
                    }
                    File.Move(dir + picToSort.ToString(), sourceDir + "Pictures\\" + picToSort.ToString());
                    filesSorted++;
                    progressDialog.UpdateProgress(filesSorted);
                }

                foreach (FileInfo vidToSort in videosToSort)
                {
                    if(File.Exists(sourceDir + "Videos\\" + vidToSort.ToString()))
                    {
                        File.Delete(sourceDir + "Videos\\" + vidToSort.ToString());
                    }
                    File.Move(dir + vidToSort.ToString(), sourceDir + "Videos\\" + vidToSort.ToString());
                    filesSorted++;
                    progressDialog.UpdateProgress(filesSorted);
                }

                foreach (FileInfo docToSort in documentsToSort)
                {
                    if(File.Exists(sourceDir + "Documents\\" + docToSort.ToString()))
                    {
                        File.Delete(sourceDir + "Documents\\" + docToSort.ToString());
                    }
                    File.Move(dir + docToSort.ToString(), sourceDir + "Documents\\" + docToSort.ToString());
                    filesSorted++;
                    progressDialog.UpdateProgress(filesSorted);
                }

                foreach (FileInfo audToSort in audioToSort)
                {
                    if(File.Exists(sourceDir + "Audio\\" + audToSort.ToString()))
                    {
                        File.Delete(sourceDir + "Audio\\" + audToSort.ToString());
                    }
                    File.Move(dir + audToSort.ToString(), sourceDir + "Audio\\" + audToSort.ToString());
                    filesSorted++;
                    progressDialog.UpdateProgress(filesSorted);
                }

                progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));
            }));

            backgroundThread.Start();
            progressDialog.ShowDialog();
        }

        private void CreateDummyFiles() {
            String path = txtFromDir.Text;

            if (path == "")
            {
                MessageBox.Show("The directory must be filled in.");
                return;
            }

            if (path.Substring(path.Length - 1) == " ")
            {
                MessageBox.Show("You put an extra space at the end. Please try again.");
                return;
            }

            if (!(path.Substring(path.Length - 1) == "\\"))
            {
                MessageBox.Show("A Directory must end with a \\. Please try again.");
                return;
            }

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Source directory " + path + " does not exist.");
                return;
            }

            Form3 progressDialog = new Form3();
            int numOfFiles = 0;
            try
            {
                numOfFiles = Convert.ToInt32(txtToDir.Text);
            }
            catch
            {
                MessageBox.Show("Invalid number. Please try again.");
                return;
            }
            Thread backgroundThread = new Thread(
        new ThreadStart(() =>
        {
            Random rnd = new Random();
            int num;
            String Ext = ".txt";
            int n = 0;
            progressDialog.UpdateMax(numOfFiles);
            for (int i = 1; i < numOfFiles + 1; i++)
            {

                num = rnd.Next(4);

                if (num == 0)
                {
                    Ext = ".txt";
                }
                else if (num == 1)
                {
                    Ext = ".mp4";
                }
                else if (num == 2)
                {
                    Ext = ".bmp";
                }
                else if (num == 3)
                {
                    Ext = ".pdf";
                }

                if (!File.Exists(path + i.ToString() + Ext))
                {
                    var file = File.Create(path + i.ToString() + Ext);
                    file.Close();
                }

                n++;
                progressDialog.UpdateProgress(n);
            }
            // No need to reset the progress since we are closing the dialog
            progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));
        }
    ));
            backgroundThread.Start();
            progressDialog.ShowDialog();
        }
    }
}