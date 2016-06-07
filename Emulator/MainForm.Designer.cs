namespace Emulator
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вернутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.программаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполнитьПрограммуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.пошаговоеВыполнениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.остановитьПрограммуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окноПрограммыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окноРегистровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окноВыводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.типВыводаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шестнадцатеричнаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.десятиричнаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Open = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Execute = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Step = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Info = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem,
            this.программаToolStripMenuItem,
            this.видToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.toolStripMenuItem2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(231, 6);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(231, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отменаToolStripMenuItem,
            this.вернутьToolStripMenuItem,
            this.toolStripMenuItem4,
            this.вырезатьToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.вставитьToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // отменаToolStripMenuItem
            // 
            this.отменаToolStripMenuItem.Name = "отменаToolStripMenuItem";
            this.отменаToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.отменаToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.отменаToolStripMenuItem.Tag = "0";
            this.отменаToolStripMenuItem.Text = "Отмена";
            this.отменаToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // вернутьToolStripMenuItem
            // 
            this.вернутьToolStripMenuItem.Name = "вернутьToolStripMenuItem";
            this.вернутьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
            this.вернутьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.вернутьToolStripMenuItem.Tag = "1";
            this.вернутьToolStripMenuItem.Text = "Вернуть";
            this.вернутьToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(178, 6);
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.вырезатьToolStripMenuItem.Tag = "2";
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            this.вырезатьToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.копироватьToolStripMenuItem.Tag = "3";
            this.копироватьToolStripMenuItem.Text = "Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.вставитьToolStripMenuItem.Tag = "4";
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.отменаToolStripMenuItem_Click);
            // 
            // программаToolStripMenuItem
            // 
            this.программаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выполнитьПрограммуToolStripMenuItem,
            this.toolStripMenuItem3,
            this.пошаговоеВыполнениеToolStripMenuItem,
            this.остановитьПрограммуToolStripMenuItem});
            this.программаToolStripMenuItem.Name = "программаToolStripMenuItem";
            this.программаToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.программаToolStripMenuItem.Text = "Программа";
            // 
            // выполнитьПрограммуToolStripMenuItem
            // 
            this.выполнитьПрограммуToolStripMenuItem.Name = "выполнитьПрограммуToolStripMenuItem";
            this.выполнитьПрограммуToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.выполнитьПрограммуToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.выполнитьПрограммуToolStripMenuItem.Text = "Выполнить программу";
            this.выполнитьПрограммуToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton_Execute_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(252, 6);
            // 
            // пошаговоеВыполнениеToolStripMenuItem
            // 
            this.пошаговоеВыполнениеToolStripMenuItem.Name = "пошаговоеВыполнениеToolStripMenuItem";
            this.пошаговоеВыполнениеToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.пошаговоеВыполнениеToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.пошаговоеВыполнениеToolStripMenuItem.Text = "Пошаговое выполнение";
            this.пошаговоеВыполнениеToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton_Step_Click);
            // 
            // остановитьПрограммуToolStripMenuItem
            // 
            this.остановитьПрограммуToolStripMenuItem.Enabled = false;
            this.остановитьПрограммуToolStripMenuItem.Name = "остановитьПрограммуToolStripMenuItem";
            this.остановитьПрограммуToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.остановитьПрограммуToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.остановитьПрограммуToolStripMenuItem.Text = "Остановить программу";
            this.остановитьПрограммуToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton_Stop_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.окноПрограммыToolStripMenuItem,
            this.окноРегистровToolStripMenuItem,
            this.окноВыводаToolStripMenuItem,
            this.toolStripMenuItem5,
            this.типВыводаToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // окноПрограммыToolStripMenuItem
            // 
            this.окноПрограммыToolStripMenuItem.Checked = true;
            this.окноПрограммыToolStripMenuItem.CheckOnClick = true;
            this.окноПрограммыToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.окноПрограммыToolStripMenuItem.Name = "окноПрограммыToolStripMenuItem";
            this.окноПрограммыToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.окноПрограммыToolStripMenuItem.Text = "Окно программы";
            this.окноПрограммыToolStripMenuItem.Click += new System.EventHandler(this.окноПрограммыToolStripMenuItem_Click);
            // 
            // окноРегистровToolStripMenuItem
            // 
            this.окноРегистровToolStripMenuItem.Checked = true;
            this.окноРегистровToolStripMenuItem.CheckOnClick = true;
            this.окноРегистровToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.окноРегистровToolStripMenuItem.Name = "окноРегистровToolStripMenuItem";
            this.окноРегистровToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.окноРегистровToolStripMenuItem.Text = "Окно регистров";
            this.окноРегистровToolStripMenuItem.Click += new System.EventHandler(this.окноРегистровToolStripMenuItem_Click);
            // 
            // окноВыводаToolStripMenuItem
            // 
            this.окноВыводаToolStripMenuItem.Checked = true;
            this.окноВыводаToolStripMenuItem.CheckOnClick = true;
            this.окноВыводаToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.окноВыводаToolStripMenuItem.Name = "окноВыводаToolStripMenuItem";
            this.окноВыводаToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.окноВыводаToolStripMenuItem.Text = "Окно вывода";
            this.окноВыводаToolStripMenuItem.Click += new System.EventHandler(this.окноВыводаToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(175, 6);
            // 
            // типВыводаToolStripMenuItem
            // 
            this.типВыводаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.шестнадцатеричнаяToolStripMenuItem,
            this.десятиричнаяToolStripMenuItem});
            this.типВыводаToolStripMenuItem.Name = "типВыводаToolStripMenuItem";
            this.типВыводаToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.типВыводаToolStripMenuItem.Text = "Основание вывода";
            // 
            // шестнадцатеричнаяToolStripMenuItem
            // 
            this.шестнадцатеричнаяToolStripMenuItem.Checked = true;
            this.шестнадцатеричнаяToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.шестнадцатеричнаяToolStripMenuItem.Name = "шестнадцатеричнаяToolStripMenuItem";
            this.шестнадцатеричнаяToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.шестнадцатеричнаяToolStripMenuItem.Tag = "1";
            this.шестнадцатеричнаяToolStripMenuItem.Text = "Шестнадцатеричная";
            this.шестнадцатеричнаяToolStripMenuItem.Click += new System.EventHandler(this.десятиричнаяToolStripMenuItem_Click);
            // 
            // десятиричнаяToolStripMenuItem
            // 
            this.десятиричнаяToolStripMenuItem.Name = "десятиричнаяToolStripMenuItem";
            this.десятиричнаяToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.десятиричнаяToolStripMenuItem.Tag = "2";
            this.десятиричнаяToolStripMenuItem.Text = "Десятичная";
            this.десятиричнаяToolStripMenuItem.Click += new System.EventHandler(this.десятиричнаяToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton_Open,
            this.toolStripButton_Save,
            this.toolStripSeparator1,
            this.toolStripButton_Execute,
            this.toolStripButton_Step,
            this.toolStripButton_Stop,
            this.toolStripSeparator2,
            this.toolStripButton_Info});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1077, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Emulator.Properties.Resources._1461522452_document_16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton_New";
            this.toolStripButton1.ToolTipText = "Новый файл";
            this.toolStripButton1.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // toolStripButton_Open
            // 
            this.toolStripButton_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Open.Image = global::Emulator.Properties.Resources._1461522515_folder_16;
            this.toolStripButton_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Open.Name = "toolStripButton_Open";
            this.toolStripButton_Open.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Open.Text = "Открыть";
            this.toolStripButton_Open.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripButton_Save
            // 
            this.toolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Save.Image = global::Emulator.Properties.Resources._1461522528_save_16;
            this.toolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Save.Name = "toolStripButton_Save";
            this.toolStripButton_Save.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Save.Text = "Сохранить";
            this.toolStripButton_Save.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Execute
            // 
            this.toolStripButton_Execute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Execute.Image = global::Emulator.Properties.Resources._1454418802_resultset_next;
            this.toolStripButton_Execute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Execute.Name = "toolStripButton_Execute";
            this.toolStripButton_Execute.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Execute.Text = "toolStripButton2";
            this.toolStripButton_Execute.ToolTipText = "Выполнить программу";
            this.toolStripButton_Execute.Click += new System.EventHandler(this.toolStripButton_Execute_Click);
            // 
            // toolStripButton_Step
            // 
            this.toolStripButton_Step.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Step.Image = global::Emulator.Properties.Resources._1454418842_resultset_last;
            this.toolStripButton_Step.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Step.Name = "toolStripButton_Step";
            this.toolStripButton_Step.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Step.Text = "Пошаговое выполнение";
            this.toolStripButton_Step.Click += new System.EventHandler(this.toolStripButton_Step_Click);
            // 
            // toolStripButton_Stop
            // 
            this.toolStripButton_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Stop.Enabled = false;
            this.toolStripButton_Stop.Image = global::Emulator.Properties.Resources.shape_square_16;
            this.toolStripButton_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Stop.Name = "toolStripButton_Stop";
            this.toolStripButton_Stop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Stop.Text = "Остановить выполнение";
            this.toolStripButton_Stop.Click += new System.EventHandler(this.toolStripButton_Stop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_Info
            // 
            this.toolStripButton_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Info.Image = global::Emulator.Properties.Resources._1461522691_info_16;
            this.toolStripButton_Info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Info.Name = "toolStripButton_Info";
            this.toolStripButton_Info.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_Info.Text = "toolStripButton2";
            this.toolStripButton_Info.ToolTipText = "Справка";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Файлы ассемблера (*.asm)|*.asm|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Файлы ассемблера (*.asm)|*.asm|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 519);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emulator8086";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton toolStripButton_Execute;
        public System.Windows.Forms.ToolStripButton toolStripButton_Step;
        public System.Windows.Forms.ToolStripButton toolStripButton_Stop;
        private System.Windows.Forms.ToolStripMenuItem программаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem окноРегистровToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem окноПрограммыToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem окноВыводаToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_Open;
        private System.Windows.Forms.ToolStripButton toolStripButton_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton_Info;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вернутьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem вырезатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.ToolStripMenuItem выполнитьПрограммуToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem пошаговоеВыполнениеToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem остановитьПрограммуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem типВыводаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шестнадцатеричнаяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem десятиричнаяToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
    }
}

