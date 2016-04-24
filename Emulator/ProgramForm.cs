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
            toolTip.SetToolTip(buttonStart, "Старт программы");
            toolTip.SetToolTip(buttonNext, "Шаг вперед");
            //toolTip.SetToolTip(buttonBack, "Шаг назад");
            toolTip.SetToolTip(buttonStop, "Остановить программу");
            //toolTip.SetToolTip(ButtonRestart, "Перезапуск");

            _processor = proc;
            asm = _processor.GetAssembler();
            linesInfo = new Dictionary<int, LineInfo>();
        }

        private void ProgramForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ButtonStart_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            LoadAsmFromRichText();
            if (asm.ProgramReady)
            {
                while (!asm.ProgramEnd)
                {
                    asm.ExecuteInstruction();
                    MainForm.registersForm.UpdateView();
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            // Если программа не готова, загрузить
            if (!asm.ProgramExecuting)
            {
                LoadAsmFromRichText(true);
                _processor.ResetRegisters();
                richTextBox1.ReadOnly = buttonStop.Enabled = true;
                this.UpdateView();
            }
            else
            {
                asm.ExecuteInstruction();
                this.UpdateView(true);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
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
                richTextBox1.ReadOnly = buttonStop.Enabled = false;
            }
            MainForm.registersForm.UpdateView(showChanges);
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
