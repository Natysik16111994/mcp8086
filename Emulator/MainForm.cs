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
        public RegistersForm registersForm;
        public ProgramForm programForm;
        public OutputForm outputForm;

        private Processor processor = null;
        private Assembler assembler;

        // Информация о текущем файле
        private string _filename = "";
        private bool _fileChanged = false;

        private Queue<string> consoleQueue;

        public MainForm()
        {
            InitializeComponent();

            processor = new Processor();
            assembler = processor.GetAssembler();

            registersForm = new RegistersForm(processor);
            programForm = new ProgramForm(processor);
            outputForm = new OutputForm();
            consoleQueue = new Queue<string>();

            MainForm.Instance = this;
            UpdateTitle();
        }

        public void SetFileChanged(bool state)
        {
            this._fileChanged = state;
        }

        // Кнопки в меню
        // Выход
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Открыть файл
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveFile() != DialogResult.Cancel)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this._filename = openFileDialog1.FileName;
                    programForm.richTextBox1.Lines = File.ReadAllLines(this._filename);
                    programForm.StopProgram();
                    SetFileChanged(false);
                }
            }
            UpdateTitle();
        }
        
        // Сохранить
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
            UpdateTitle();
        }

        // Сохранить как
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
            UpdateTitle();
        }
        
        // Создать
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfirmSaveFile();
            this._filename = "";
            programForm.richTextBox1.Text = "";
            UpdateTitle();
        }

        /// <summary>
        /// Спрашивает, сохранить ли файл
        /// </summary>
        /// <returns>Результат вызова диалога.</returns>
        private DialogResult ConfirmSaveFile()
        {
            if (!this._fileChanged) return DialogResult.No;
            if (programForm.richTextBox1.Text.Length == 0) return DialogResult.No;
            DialogResult dr = MessageBox.Show("Сначала сохранить программу?", this.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) SaveFile();
            return dr;
        }

        /// <summary>
        /// Сохраняет файл, если имя файла уже указано. В ином случае открывает диалог сохранения.
        /// </summary>
        /// <returns>Возвращает true, если файл сохранен; false в ином случае.</returns>
        private bool SaveFile()
        {
            if (this._filename.Length == 0) return SaveFileAs();
            else File.WriteAllLines(this._filename, programForm.richTextBox1.Lines);
            SetFileChanged(false);
            return true;
        }

        /// <summary>
        /// Вызывает диалог сохранения
        /// </summary>
        /// <returns>Возвращает true, если файл сохранен; false в ином случае.</returns>
        private bool SaveFileAs()
        {
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._filename = saveFileDialog1.FileName;
                if (Path.GetExtension(this._filename).Length == 0)
                {
                    if (saveFileDialog1.FilterIndex == 0) this._filename += ".asm";
                    else if (saveFileDialog1.FilterIndex == 1) this._filename += ".txt";
                }
                File.WriteAllLines(this._filename, programForm.richTextBox1.Lines);
                SetFileChanged(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Обновляет заголовок окна
        /// </summary>
        private void UpdateTitle()
        {
            if (this._filename.Length == 0) this.Text = this.ProductName;
            else this.Text = this.ProductName + " [" + this._filename + "]";
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
            //programForm.richTextBox1.Lines = File.ReadAllLines("add.asm");

            registersForm.Left = programForm.Right;
            registersForm.Top = 0;
            registersForm.Show();

            outputForm.Left = programForm.Right;
            outputForm.Top = registersForm.Bottom;
            outputForm.Size = new Size(registersForm.Width, programForm.Height - outputForm.Top);
            outputForm.Show();

            programForm.Focus();
        }

        /// <summary>
        /// Выполнить программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Execute_Click(object sender, EventArgs e)
        {
            programForm.ExecuteProgram();
        }

        /// <summary>
        /// Выполнить шаг программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Step_Click(object sender, EventArgs e)
        {
            programForm.ExecuteProgramStep();
        }

        /// <summary>
        /// Останавливает выполнение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Stop_Click(object sender, EventArgs e)
        {
            programForm.StopProgram();
            this.WriteConsole("Выполнение прервано пользователем.");
        }

        /// <summary>
        /// Выполняет одну из команд меню "Правка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tag = int.Parse(((ToolStripMenuItem)sender).Tag.ToString());
            if (tag == 0) programForm.richTextBox1.Undo();
            else if (tag == 1) programForm.richTextBox1.Redo();
            else if (tag == 2) programForm.richTextBox1.Cut();
            else if (tag == 3) programForm.richTextBox1.Copy();
            else if (tag == 4) programForm.richTextBox1.Paste();
        }

        /// <summary>
        /// Записывает строку в консоль вывода (Асинхронно)
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        public void WriteConsole(string text)
        {
            string time = DateTime.Now.ToLongTimeString();
            consoleQueue.Enqueue(time + "\t" + text + "\n");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._filename = "text.asm";
            programForm.richTextBox1.Lines = File.ReadAllLines(this._filename);
            this.UpdateTitle();
        }

        private void десятиричнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            bool hex = (item.Tag.ToString() == "1");
            шестнадцатеричнаяToolStripMenuItem.Checked = hex;
            десятиричнаяToolStripMenuItem.Checked = !hex;
        }

        /// <summary>
        /// Указывает, производится ли вывод в шестнадцатеричном формате
        /// </summary>
        /// <returns></returns>
        public bool OutputHex()
        {
            return шестнадцатеричнаяToolStripMenuItem.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (consoleQueue.Count > 0)
            {
                outputForm.richTextBox1.AppendText(consoleQueue.Dequeue());
                outputForm.richTextBox1.SelectionStart = outputForm.richTextBox1.Text.Length;
                outputForm.richTextBox1.Focus();
            }
        }
    }
}
