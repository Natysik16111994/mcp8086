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
        public static MainForm Instance = null;
        /*public static RegistersForm registersForm = null;
        public static ProgramForm programForm = null;*/
        //private DebugForm debug;
        public RegistersForm registersForm;
        public ProgramForm programForm;
        public OutputForm outputForm;

        private Processor processor = null;
        private Assembler assembler;

        private string _file = "";

        public MainForm()
        {
            InitializeComponent();

            processor = new Processor();
            assembler = processor.GetAssembler();

            registersForm = new RegistersForm(processor);
            programForm = new ProgramForm(processor);
            outputForm = new OutputForm();

            MainForm.Instance = this;
            

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

            //Processor processor = new Processor();
            // 65535

//            processor.AX.Value.Binary = "1011000010001000";
            /*processor.AX.Value.Binary = "0000001000000100";
            processor.BX.Value.Binary = "0100010000000101";*/
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
            /*Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.BX.Value.Binary);*/
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
            //processor.Shld(processor.AX, processor.BX, 3);
            //processor.Rcl(processor.AX, 1);
            //Console.WriteLine(processor.AX.Value.Binary);
            //Console.WriteLine(processor.AX.Value.Decimal);
            //processor.Sbb(processor.AX, "13", 10);
            //Console.WriteLine(processor.AX.Value.Decimal);
            //Console.WriteLine(processor.BX.Value.Decimal);
           // Console.WriteLine(processor.DX.Value.Binary);
            
            //processor.stack.Push(processor.AX);
           // Console.WriteLine("SP: " + processor.SP.Value.Binary);
           /* processor.stack.Push(processor.BX);
            Console.WriteLine("SP: " + processor.SP.Value.Binary);
            processor.BX.Value.Binary = "0000000000000000";
            processor.stack.Pop(processor.BX);
            Console.WriteLine(processor.BX.Value.Binary);
            Console.WriteLine("SP: " + processor.SP.Value.Binary);*/

           /* processor.stack.Pusha();
            Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.CX.Value.Binary);
            Console.WriteLine(processor.DX.Value.Binary);
            Console.WriteLine(processor.BX.Value.Binary);
            Console.WriteLine(processor.SP.Value.Binary);
            Console.WriteLine(processor.BP.Value.Binary);
            Console.WriteLine(processor.SI.Value.Binary);
            Console.WriteLine(processor.DI.Value.Binary);
            Console.WriteLine("SP: " + processor.SP.Value.Binary);

            processor.stack.Popa();
            Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.CX.Value.Binary);
            Console.WriteLine(processor.DX.Value.Binary);
            Console.WriteLine(processor.BX.Value.Binary);
            Console.WriteLine(processor.SP.Value.Binary);
            Console.WriteLine(processor.BP.Value.Binary);
            Console.WriteLine(processor.SI.Value.Binary);
            Console.WriteLine(processor.DI.Value.Binary);
            Console.WriteLine("SP: " + processor.SP.Value.Binary);*/
           // processor.Div("10",10);
           // processor.Mul(processor.BX, false, false);
           /* processor.Mul("725", 10);
            Console.WriteLine(processor.AX.Value.Binary);
            Console.WriteLine(processor.DX.Value.Binary);*/

            /*
            Console.WriteLine(string.Format("ZF: {0}", processor.IsFlag(Register.Flags.ZF)));
            Console.WriteLine(string.Format("CF: {0}", processor.IsFlag(Register.Flags.CF)));
            Console.WriteLine(string.Format("OF: {0}", processor.IsFlag(Register.Flags.OF)));*/

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

            // Assembler
            //assembler = new Assembler(processor);
            //Assembler assembler = processor.assembler;
            //assembler.LoadAsmFromFile("add.asm");
            /*while (!assembler.ProgramEnd)
            {
                assembler.ExecuteInstruction();
                registersForm.UpdateView();
            }
            Console.WriteLine(processor.AX.Decimal);*/
            //registersForm.UpdateView();
            /*
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();
            assembler.ExecuteInstruction();*/
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
                //program.richTextBox1.Text = File.ReadAllText(open.FileName, Encoding.Default);
            }
        }
        
        // Сохранить
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                //File.WriteAllText(save.FileName, program.richTextBox1.Text, Encoding.Default); 
            }
        }
        
        // Создать
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registersForm.UpdateView();
            //program.richTextBox1.Clear(); 
        }
        
        // Отменить
        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //program.richTextBox1.Undo();
        }

        // Повторить
        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //program.richTextBox1.Redo();
        }
        
        // Вырезать
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programForm.richTextBox1.Cut();
        }
        
        // Копировать
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programForm.richTextBox1.Copy();
        }

        // Вставить
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programForm.richTextBox1.Paste();
        }


        private void окноРегистровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registersForm.Visible = окноРегистровToolStripMenuItem.Checked;
        }

        private void окноПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programForm.Visible = окноПрограммыToolStripMenuItem.Checked;
        }

        private void окноВыводаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outputForm.Visible = окноВыводаToolStripMenuItem.Checked;
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Выводим и располагаем формы
            programForm.MdiParent = registersForm.MdiParent = outputForm.MdiParent = this;

            programForm.Left = 0;
            programForm.Top = 0;
            programForm.Size = new System.Drawing.Size(300, 470);
            programForm.Show();
            programForm.richTextBox1.Lines = File.ReadAllLines("add.asm");

            registersForm.Show();
            registersForm.Left = programForm.Right;
            registersForm.Top = 0;

            outputForm.Left = programForm.Right;
            outputForm.Top = registersForm.Bottom;
            outputForm.Size = new Size(registersForm.Width, programForm.Height - outputForm.Top);
            outputForm.Show();
        }

        private void toolStripButton_Execute_Click(object sender, EventArgs e)
        {
            programForm.ExecuteProgram();
        }

        private void toolStripButton_Step_Click(object sender, EventArgs e)
        {
            programForm.ExecuteProgramStep();
        }

        private void toolStripButton_Stop_Click(object sender, EventArgs e)
        {
            programForm.StopProgram();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tag = int.Parse(((ToolStripMenuItem)sender).Tag.ToString());
            if (tag == 0) programForm.richTextBox1.Undo();
            else if (tag == 1) programForm.richTextBox1.Redo();
            else if (tag == 2) programForm.richTextBox1.Cut();
            else if (tag == 3) programForm.richTextBox1.Copy();
            else if (tag == 4) programForm.richTextBox1.Paste();
        }
    }
}
