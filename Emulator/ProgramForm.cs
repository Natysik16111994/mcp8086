using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Emulator
{
    public partial class ProgramForm : Form
    {
        public ProgramForm()
        {
            InitializeComponent();

            toolTip.SetToolTip(ButtonStart, "Старт программы");
            toolTip.SetToolTip(ButtonNext, "Шаг вперед");
            toolTip.SetToolTip(ButtonBack, "Шаг назад");
            toolTip.SetToolTip(ButtonRestart, "Перезапуск");
        }

        private void ProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ButtonStart_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
