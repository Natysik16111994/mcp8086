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
        private RegistersForm registersForm;
        private ProgramForm program;
        private DebugForm debug;
        private OutputForm output;

        public MainForm()
        {
            InitializeComponent();

            // Выводим и располагаем формы
            registersForm = new RegistersForm();
            registersForm.MdiParent = this;
            registersForm.Left = 0;
            registersForm.Top = 0;
            registersForm.Show();

            /*program = new ProgramForm();
            program.MdiParent = this;
            program.Location = this.Location;
            program.Left += this.Size.Width;
            program.Top += this.Size.Height;
            program.Show();*/

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
            R.SetFlag(Register.Flags.CF, true); b 
            Console.WriteLine(R.GetFlag(Register.Flags.AF));
            Console.WriteLine(R.GetFlag(Register.Flags.DF));*/

            Processor processor = new Processor();
            // 65535

            processor.AX.Value.Binary = "1011000010001000";
            processor.BX.Value.Binary = "0100010000000101";
            //processor.Mov(processor.BX, processor.AX);
            //processor.Bsf(processor.AX, processor.BX);
            //Console.WriteLine(processor.BX.Value.Binary);
            //Console.WriteLine(processor.AX.Value.Decimal);
            //processor.Bts(processor.AX, 13);
            //processor.Lahf();
           // processor.Clc();
           // processor.Cmc();
           // processor.Cbw(processor.AX);
            //processor.Cwd();
            Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.BX.Value.Binary);
            //processor.Movsx(processor.BX processor.AX);
            //Console.WriteLine(processor.BX.Value.Binary);
            //Console.WriteLine(processor.AX.Value.Decimal);
            //processor.Dec(processor.AX);
            //processor.Neg(processor.BX);   
            //processor.Or(processor.AX, processor.BX);
            //processor.Or(processor.AX,"101001001",2);
            //processor.Sar(processor.AX, 1);
            //processor.Shr(processor.AX, 3);
           // processor.Test(processor.AX, processor.BX);
            //processor.Xchg(processor.AX, processor.BX);
            processor.Shld(processor.AX, processor.BX, 3);
            //processor.Rcl(processor.AX, 1);
            Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.AX.Value.Decimal);
            //processor.Sbb(processor.AX, "13", 10);
            //Console.WriteLine(processor.AX.Value.Decimal);
            //Console.WriteLine(processor.BX.Value.Decimal);
           // Console.WriteLine(processor.DX.Value.Binary);
            
            Console.WriteLine(string.Format("ZF: {0}", processor.IsFlag(Register.Flags.ZF)));
            Console.WriteLine(string.Format("CF: {0}", processor.IsFlag(Register.Flags.CF)));
            Console.WriteLine(string.Format("OF: {0}", processor.IsFlag(Register.Flags.OF)));


            /*BinaryNumber a = new BinaryNumber(65365);
            BinaryNumber b = new BinaryNumber(2);
            Console.WriteLine(a.Binary);
            Console.WriteLine(b.Binary);
            a *= b;
            Console.WriteLine();
            Console.WriteLine(a.Binary);
            Console.WriteLine(BinaryNumber.GetBinaryString(a.HighPartMul));
            Console.WriteLine(a.Decimal);

            Console.WriteLine(string.Format("CF: {0}\nOF: {1}", a.CarryFlag, a.OverflowFlag));*/
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
