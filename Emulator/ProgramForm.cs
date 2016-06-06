using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
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

        /*
mov ax, 16000
start:
dec ax
jnz start
         */


        /// <summary>
        /// Выполнить программу
        /// </summary>
        public void ExecuteProgram()
        {
            if (LoadAsmFromRichText(false))
            {
                MainForm.Instance.выполнитьПрограммуToolStripMenuItem.Enabled = MainForm.Instance.toolStripButton_Execute.Enabled =
                MainForm.Instance.toolStripButton_Step.Enabled = MainForm.Instance.пошаговоеВыполнениеToolStripMenuItem.Enabled = false;
                MainForm.Instance.остановитьПрограммуToolStripMenuItem.Enabled = MainForm.Instance.toolStripButton_Stop.Enabled = true;
                richTextBox1.ReadOnly = true;
                MainForm.Instance.Update();

                _processor.ResetRegisters();
                this.backgroundWorker1.RunWorkerAsync();
                MainForm.Instance.WriteConsole("Начато выполнение программы.");
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
                if (LoadAsmFromRichText(true))
                {
                    _processor.ResetRegisters();
                    richTextBox1.ReadOnly = MainForm.Instance.toolStripButton_Stop.Enabled =
                        MainForm.Instance.остановитьПрограммуToolStripMenuItem.Enabled = true;
                    MainForm.Instance.toolStripButton_Execute.Enabled = MainForm.Instance.выполнитьПрограммуToolStripMenuItem.Enabled = false;
                    this.UpdateView();
                    MainForm.Instance.WriteConsole("Начато пошаговое выполнение программы.");
                    _processor.Flags.SetFlag(true, Register.Flags.TF);
                    this.UpdateView(true);
                }
            }
            else
            {
                asm.ExecuteInstruction();
                this.UpdateView(true);
                if (asm.ProgramEnd)
                {
                    MainForm.Instance.WriteConsole("Завершено пошаговое выполнение.");
                }
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
        /// <param name="currentLine">Индекс подсвечиваемой строки</param>
        private void UpdateView(bool showChanges = false)
        {
            int line = asm.GetCurrentLine();
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.DeselectAll();
            if (line != -1)
            {
                if (linesInfo.ContainsKey(line))
                {
                    richTextBox1.Select(linesInfo[line].start, linesInfo[line].length);
                    richTextBox1.SelectionColor = Color.White;
                    richTextBox1.SelectionBackColor = Color.Brown;
                    richTextBox1.DeselectAll();
                }
            }
            else
            {
                richTextBox1.ReadOnly = MainForm.Instance.toolStripButton_Stop.Enabled =
                    MainForm.Instance.остановитьПрограммуToolStripMenuItem.Enabled = false;
                MainForm.Instance.toolStripButton_Execute.Enabled = MainForm.Instance.выполнитьПрограммуToolStripMenuItem.Enabled = true;
            }
            this.Update();
            MainForm.Instance.registersForm.UpdateView(showChanges);
        }
        
        /// <summary>
        /// Загружает код ассемблера из поля
        /// </summary>
        private bool LoadAsmFromRichText(bool loadLineInfo = false)
        {
            if (asm.LoadAsm(richTextBox1.Lines))
            {
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
                return true;
            }
            return false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int progress = 0;
            if (asm.ProgramReady)
            {
                while (!asm.ProgramEnd)
                {
                    progress += (progress == 0 ? 1 : -1);
                    worker.ReportProgress(progress);
                    asm.ExecuteInstruction();
                    Thread.Sleep(1);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainForm.Instance.registersForm.UpdateView(true);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MainForm.Instance.выполнитьПрограммуToolStripMenuItem.Enabled = MainForm.Instance.toolStripButton_Execute.Enabled =
            MainForm.Instance.toolStripButton_Step.Enabled = MainForm.Instance.пошаговоеВыполнениеToolStripMenuItem.Enabled = true;
            MainForm.Instance.остановитьПрограммуToolStripMenuItem.Enabled = MainForm.Instance.toolStripButton_Stop.Enabled = false;
            richTextBox1.ReadOnly = false;
            MainForm.Instance.WriteConsole("Выполнение программы завершено.");
            this.UpdateView();
        }
    }
}
