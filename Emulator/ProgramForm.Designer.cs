namespace Emulator
{
    partial class ProgramForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ButtonRestart = new System.Windows.Forms.Button();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 50);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(309, 440);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // ButtonRestart
            // 
            this.ButtonRestart.Image = global::Emulator.Properties.Resources._1454418698_update;
            this.ButtonRestart.Location = new System.Drawing.Point(12, 12);
            this.ButtonRestart.Name = "ButtonRestart";
            this.ButtonRestart.Size = new System.Drawing.Size(32, 32);
            this.ButtonRestart.TabIndex = 4;
            this.ButtonRestart.UseVisualStyleBackColor = true;
            // 
            // ButtonBack
            // 
            this.ButtonBack.Image = global::Emulator.Properties.Resources._1454418848_resultset_first;
            this.ButtonBack.Location = new System.Drawing.Point(50, 12);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(32, 32);
            this.ButtonBack.TabIndex = 3;
            this.ButtonBack.UseVisualStyleBackColor = true;
            // 
            // ButtonNext
            // 
            this.ButtonNext.Image = global::Emulator.Properties.Resources._1454418842_resultset_last;
            this.ButtonNext.Location = new System.Drawing.Point(128, 12);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(32, 32);
            this.ButtonNext.TabIndex = 2;
            this.ButtonNext.UseVisualStyleBackColor = true;
            // 
            // ButtonStart
            // 
            this.ButtonStart.Image = global::Emulator.Properties.Resources._1454418802_resultset_next;
            this.ButtonStart.Location = new System.Drawing.Point(88, 12);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(32, 32);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.MouseEnter += new System.EventHandler(this.ButtonStart_MouseEnter);
            // 
            // ProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(333, 502);
            this.Controls.Add(this.ButtonRestart);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ProgramForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Emulator8086";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgramForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonBack;
        private System.Windows.Forms.Button ButtonRestart;
        public System.Windows.Forms.RichTextBox richTextBox1;

    }
}