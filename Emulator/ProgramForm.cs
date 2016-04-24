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
        struct LineInfo
        {
            public int start, length;
            public LineInfo(int st, int len)
            {
                start = st;
                length = len;
            }
        }

        private Processor _processor;
        private Assembler asm;
        private Dictionary<int, LineInfo> linesInfo;

        public ProgramForm(Processor proc)
        {
            InitializeComponent();

            _processor = proc;
            asm = _processor.GetAssembler();
            linesInfo = new Dictionary<int, LineInfo>();
        }

        private void ProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != CloseReason.MdiFormClosing)
            {
                e.Cancel = true;
                this.Visible = MainForm.Instance.окноПрограммыToolStripMenuItem.Checked = false;
            }
        }

        /// <summary>
        /// Выполнить программу
        /// </summary>
        public void ExecuteProgram()
        {
            LoadAsmFromRichText();
            if (asm.ProgramReady)
            {
                while (!asm.ProgramEnd)
                {
                    asm.ExecuteInstruction();
                    MainForm.Instance.registersForm.UpdateView();
                }
            }
        }

        /// <summary>
        /// Выполнить шаг программы
        /// </summary>
        public void ExecuteProgramStep()
        {
            // Если программа не готова, загрузить
            if (!asm.ProgramExecuting)
            {
                LoadAsmFromRichText(true);
                _processor.ResetRegisters();
                richTextBox1.ReadOnly = MainForm.Instance.toolStripButton_Stop.Enabled = true;
                this.UpdateView();
            }
            else
            {
                asm.ExecuteInstruction();
                this.UpdateView(true);
            }
        }

        /// <summary>
        /// Остановить выполнение программы
        /// </summary>
        public void StopProgram()
        {
            asm.ProgramEnd = true;
            this.UpdateView();
        }

        /// <summary>
        /// Обновляет вид
        /// </summary>
        /// <param name="showChanges">Флаг, указывает подсвечивать ли изменяемые значения</param>
        private void UpdateView(bool showChanges = false)
        {
            int line = asm.GetCurrentLine();
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.DeselectAll();
            if (line != -1)
            {
                richTextBox1.Select(linesInfo[line].start, linesInfo[line].length);
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.SelectionBackColor = Color.Brown;
                richTextBox1.DeselectAll();
            }
            else
            {
                richTextBox1.ReadOnly = MainForm.Instance.toolStripButton_Stop.Enabled = false;
            }
            MainForm.Instance.registersForm.UpdateView(showChanges);
        }
        
        /// <summary>
        /// Загружает код ассемблера из поля
        /// </summary>
        private void LoadAsmFromRichText(bool loadLineInfo = false)
        {
            asm.LoadAsm(richTextBox1.Lines);
            if (loadLineInfo)
            {
                linesInfo.Clear();
                int start = 0, length = 0;
                for (int i = 0; i < richTextBox1.Lines.Length; i++)
                {
                    if (i != 0) start = start + length + 1;
                    length = richTextBox1.Lines[i].Length;
                    linesInfo.Add(i, new LineInfo(start, length));
                }
            }
        }
    }
}
