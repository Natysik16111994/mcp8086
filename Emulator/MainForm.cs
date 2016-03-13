using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Emulator
{
    public partial class MainForm : Form
    {
        private ProgramForm program;
        private DebugForm debug;
        private OutputForm output;

        public MainForm()
        {
            InitializeComponent();

            // Выводим и располагаем формы
            program = new ProgramForm();
            program.MdiParent = this;
            program.Location = this.Location;
            program.Left += this.Size.Width;
            program.Top += this.Size.Height;
            program.Show();

            /*
            debug = new DebugForm();
            debug.Location = this.Location;
            debug.Top += this.Size.Height;
            debug.Show();

            output = new OutputForm();
            output.Location = this.Location;
            output.Left += this.Size.Width;
            output.Show();*/

            // Записываем значения регистров
            /*AX.Set(0, true); AX.Set(3, true); AX.Set(10, true); AX.Set(13, true);
            Console.WriteLine(AX.ToString());
            Console.WriteLine(AX.GetH());
            Console.WriteLine(AX.GetL());
            AX.Shr();
            Console.WriteLine(AX.ToString());
            Console.WriteLine(AX.GetH());
            Console.WriteLine(AX.GetL());*/

            /*
            labelCF.Text = R.GetFlag(Register.Flags.CF);
            labelOF.Text = R.GetFlag(Register.Flags.OF);
            labelZF.Text = R.GetFlag(Register.Flags.ZF);
            labelSF.Text = R.GetFlag(Register.Flags.SF);
            labelPF.Text = R.GetFlag(Register.Flags.PF);
            labelAF.Text = R.GetFlag(Register.Flags.AF);
            labelDF.Text = R.GetFlag(Register.Flags.DF);
            labelIF.Text = R.GetFlag(Register.Flags.IF);
            labelTF.Text = R.GetFlag(Register.Flags.TF);*/
            
            /*R.SetFlag(Register.Flags.AF, true);
            R.SetFlag(Register.Flags.CF, true);
            Console.WriteLine(R.GetFlag(Register.Flags.AF));
            Console.WriteLine(R.GetFlag(Register.Flags.DF));*/

            Processor processor = new Processor();

            Number a = new Number(20);
            Console.WriteLine(a.Hex);
            Console.WriteLine(a.Oct);
            Console.WriteLine(a.Bin);

            a.Div("10", 16);
            Console.WriteLine(a.Hex);
        }

        // Кнопки в меню
        // Выход
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt files  (*.txt)|*.txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                program.richTextBox1.Text = File.ReadAllText(open.FileName, Encoding.Default);
            }
        }
        
        // Сохранить
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save.FileName, program.richTextBox1.Text, Encoding.Default); 
            }
        }
        
        // Создать
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Clear(); 
        }
        
        // Отменить
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Undo();
        }

        // Повторить
        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Redo();
        }
        
        // Вырезать
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Cut();
        }
        
        // Копировать
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Copy();
        }

        // Вставить
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            program.richTextBox1.Paste();
        }

    }
}
