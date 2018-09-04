namespace Net_Test
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFromDir = new System.Windows.Forms.Label();
            this.txtFromDir = new System.Windows.Forms.TextBox();
            this.txtToDir = new System.Windows.Forms.TextBox();
            this.lblToDir = new System.Windows.Forms.Label();
            this.btnAction = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbAction
            // 
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Items.AddRange(new object[] {
            "Delete",
            "Move",
            "Sort",
            "Dummy Files"});
            this.cbAction.Location = new System.Drawing.Point(52, 11);
            this.cbAction.Margin = new System.Windows.Forms.Padding(2);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(293, 21);
            this.cbAction.TabIndex = 0;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.cbAction_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Action";
            // 
            // lblFromDir
            // 
            this.lblFromDir.AutoSize = true;
            this.lblFromDir.Location = new System.Drawing.Point(12, 35);
            this.lblFromDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFromDir.Name = "lblFromDir";
            this.lblFromDir.Size = new System.Drawing.Size(52, 13);
            this.lblFromDir.TabIndex = 2;
            this.lblFromDir.Text = "From Dir: ";
            // 
            // txtFromDir
            // 
            this.txtFromDir.Location = new System.Drawing.Point(68, 33);
            this.txtFromDir.Margin = new System.Windows.Forms.Padding(2);
            this.txtFromDir.Name = "txtFromDir";
            this.txtFromDir.Size = new System.Drawing.Size(194, 20);
            this.txtFromDir.TabIndex = 3;
            // 
            // txtToDir
            // 
            this.txtToDir.Location = new System.Drawing.Point(68, 54);
            this.txtToDir.Margin = new System.Windows.Forms.Padding(2);
            this.txtToDir.Name = "txtToDir";
            this.txtToDir.Size = new System.Drawing.Size(194, 20);
            this.txtToDir.TabIndex = 5;
            // 
            // lblToDir
            // 
            this.lblToDir.AutoSize = true;
            this.lblToDir.Location = new System.Drawing.Point(12, 56);
            this.lblToDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblToDir.Name = "lblToDir";
            this.lblToDir.Size = new System.Drawing.Size(42, 13);
            this.lblToDir.TabIndex = 4;
            this.lblToDir.Text = "To Dir: ";
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(266, 56);
            this.btnAction.Margin = new System.Windows.Forms.Padding(2);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(79, 19);
            this.btnAction.TabIndex = 6;
            this.btnAction.Text = "Delete";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // Form2
            // 
            this.AcceptButton = this.btnAction;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 85);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.txtToDir);
            this.Controls.Add(this.lblToDir);
            this.Controls.Add(this.txtFromDir);
            this.Controls.Add(this.lblFromDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Manager";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFromDir;
        private System.Windows.Forms.TextBox txtFromDir;
        private System.Windows.Forms.TextBox txtToDir;
        private System.Windows.Forms.Label lblToDir;
        private System.Windows.Forms.Button btnAction;
    }
}