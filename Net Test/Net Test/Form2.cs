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

        private void btnAction_Click(object sender, EventArgs e)
        {
            switch (cbAction.SelectedIndex) {
                case DELETE: Delete(); break;
                case MOVE: Move(); break;
                case SORT: Sort(); break;
                case DUMMY_FILES: CreateDummyFiles(); break;
            }
        }

        private void Delete() {
            String path = txtToDir.Text;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Directory doesn't exist.");
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);

            int numOfFiles = di.GetFiles().Length;
            int failedFiles = 0;
            int failedDir = 0;

            foreach (FileInfo file in di.GetFiles()) {
                try {
                    file.Delete();
                } catch {
                    failedFiles++;
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories()) {
                try {
                    dir.Delete(true);
                } catch {
                    failedDir++;
                }
            }

            MessageBox.Show(failedFiles + " files couldn't be deleted.\n" + failedDir + " directories couldn't be deleted.");
        }

        private void Move() {

        }

        private void Sort() {

        }

        private void CreateDummyFiles() {
            String path = txtFromDir.Text;

            if (!Directory.Exists(path)) {
                MessageBox.Show("Directory doesn't exsist.");
                return;
            }

            int numOfFiles = 0;
            try {
                numOfFiles = Convert.ToInt32(txtToDir.Text);
            } catch {
                MessageBox.Show("Invalid number. Please try again.");
                return;
            }

            Random rnd = new Random();
            int num;
            String Ext = ".txt";

            for (int i = 1; i < numOfFiles + 1; i++) {

                num = rnd.Next(3);

                if (num == 0) {
                    Ext = ".txt";
                } else if (num == 1) {
                    Ext = ".mp4";
                } else if (num == 2) {
                    Ext = ".bmp";
                } else if (num == 3) {
                    Ext = ".pdf";
                }

                if (!File.Exists(path + i.ToString() + Ext)) {
                    File.Create(path + i.ToString() + Ext);
                }
            }
        }
    }
}
