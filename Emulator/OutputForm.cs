using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Emulator
{
    public partial class OutputForm : Form
    {
        public OutputForm()
        {
            InitializeComponent();
        }

        private void OutputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.MdiFormClosing)
            {
                e.Cancel = true;
                this.Visible = MainForm.Instance.окноВыводаToolStripMenuItem.Checked = false;
            }
        }

        public void ShotInputField()
        {
            this.textBox1.Text = "";
            this.textBox1.Visible = true;
            this.richTextBox1.Height -= (this.textBox1.Height + 5);
            this.Focus();
            this.textBox1.Focus();
        }

        public void HideInputField()
        {
            this.textBox1.Visible = false;
            this.richTextBox1.Height += (this.textBox1.Height + 5);
        }
    }
}
