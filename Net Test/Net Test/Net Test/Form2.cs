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
                lblToDir.Text = "Dir: ";
                lblFromDir.Visible = false;
                txtFromDir.Visible = false;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Delete";
            } else if (cbAction.SelectedIndex == MOVE) {
                lblFromDir.Text = "From Dir: ";
                lblToDir.Text = "To Dir: ";
                lblFromDir.Visible = true;
                txtFromDir.Visible = true;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Move";
            } else if (cbAction.SelectedIndex == SORT) {
                lblToDir.Text = "Dir: ";
                lblFromDir.Visible = false;
                txtFromDir.Visible = false;
                lblToDir.Visible = true;
                txtToDir.Visible = true;
                btnAction.Text = "Sort";
            } else if (cbAction.SelectedIndex == DUMMY_FILES) {
                lblToDir.Text = "Dir: ";
                lblFromDir.Visible = false;
                txtFromDir.Visible = false;
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

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void Move() {

        }

        private void Sort() {

        }

        private void CreateDummyFiles() {

        }
    }
}
