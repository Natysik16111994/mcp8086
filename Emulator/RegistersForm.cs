using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Emulator
{
    public partial class RegistersForm : Form
    {
        private Processor _processor = null;

        public RegistersForm(Processor proc)
        {
            InitializeComponent();
            _processor = proc;
        }

        /// <summary>
        /// Обновляет вид
        /// </summary>
        /// <param name="showChanges">Флаг, указывает подсвечивать ли изменяемые значения</param>
        public void UpdateView(bool showChanges = false)
        {
            SetText(labelAH, _processor.AX.HighHex, showChanges);
            SetText(labelAL, _processor.AX.LowHex, showChanges);
            SetText(labelBH, _processor.BX.HighHex, showChanges);
            SetText(labelBL, _processor.BX.LowHex, showChanges);
            SetText(labelCH, _processor.CX.HighHex, showChanges);
            SetText(labelCL, _processor.CX.LowHex, showChanges);
            SetText(labelDH, _processor.DX.HighHex, showChanges);
            SetText(labelDL, _processor.DX.LowHex, showChanges);
            SetText(labelSI, _processor.SI.Hex, showChanges);
            SetText(labelDI, _processor.DI.Hex, showChanges);
            SetText(labelBP, _processor.BP.Hex, showChanges);
            SetText(labelSP, _processor.SP.Hex, showChanges);
            SetText(labelCS, _processor.CS.Hex, showChanges);
            SetText(labelDS, _processor.DS.Hex, showChanges);
            SetText(labelES, _processor.ES.Hex, showChanges);
            SetText(labelSS, _processor.SS.Hex, showChanges);
            SetText(labelIP, _processor.IP.Hex, showChanges);
            SetText(labelFlagOF, _processor.Flags.GetFlag(Register.Flags.OF) ? "1" : "0", showChanges);
            SetText(labelFlagDF, _processor.Flags.GetFlag(Register.Flags.DF) ? "1" : "0", showChanges);
            SetText(labelFlagIF, _processor.Flags.GetFlag(Register.Flags.IF) ? "1" : "0", showChanges);
            SetText(labelFlagTF, _processor.Flags.GetFlag(Register.Flags.TF) ? "1" : "0", showChanges);
            SetText(labelFlagSF, _processor.Flags.GetFlag(Register.Flags.SF) ? "1" : "0", showChanges);
            SetText(labelFlagZF, _processor.Flags.GetFlag(Register.Flags.ZF) ? "1" : "0", showChanges);
            SetText(labelFlagAF, _processor.Flags.GetFlag(Register.Flags.AF) ? "1" : "0", showChanges);
            SetText(labelFlagPF, _processor.Flags.GetFlag(Register.Flags.PF) ? "1" : "0", showChanges);
            SetText(labelFlagCF, _processor.Flags.GetFlag(Register.Flags.CF) ? "1" : "0", showChanges);
            this.Update();
        }

        /// <summary>
        /// Устанавливает текст лэйбела, если текст новый, то подсвечивает его
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="text"></param>
        /// <param name="showChanges"></param>
        public void SetText(Label obj, string text, bool showChanges = false)
        {
            if (!showChanges)
            {
                obj.ForeColor = Color.Black;
            }
            else
            {
                obj.ForeColor = (obj.Text != text ? Color.Red : Color.Black);
            }
            obj.Text = text;
        }

        private void RegistersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.MdiFormClosing)
            {
                e.Cancel = true;
                this.Visible = MainForm.Instance.окноРегистровToolStripMenuItem.Checked = false;
            }
        }
    }
}
