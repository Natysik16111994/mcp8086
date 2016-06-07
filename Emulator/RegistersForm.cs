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
            SetText(labelAH, GetRegisterValue(_processor.AX, Register.Types.High), showChanges);
            SetText(labelAL, GetRegisterValue(_processor.AX, Register.Types.Low), showChanges);
            SetText(labelBH, GetRegisterValue(_processor.BX, Register.Types.High), showChanges);
            SetText(labelBL, GetRegisterValue(_processor.BX, Register.Types.Low), showChanges);
            SetText(labelCH, GetRegisterValue(_processor.CX, Register.Types.High), showChanges);
            SetText(labelCL, GetRegisterValue(_processor.CX, Register.Types.Low), showChanges);
            SetText(labelDH, GetRegisterValue(_processor.DX, Register.Types.High), showChanges);
            SetText(labelDL, GetRegisterValue(_processor.DX, Register.Types.Low), showChanges);
            SetText(labelSI, GetRegisterValue(_processor.SI, Register.Types.None), showChanges);
            SetText(labelDI, GetRegisterValue(_processor.DI, Register.Types.None), showChanges);
            SetText(labelBP, GetRegisterValue(_processor.BP, Register.Types.None), showChanges);
            SetText(labelSP, GetRegisterValue(_processor.SP, Register.Types.None), showChanges);
            SetText(labelCS, GetRegisterValue(_processor.CS, Register.Types.None), showChanges);
            SetText(labelDS, GetRegisterValue(_processor.DS, Register.Types.None), showChanges);
            SetText(labelES, GetRegisterValue(_processor.ES, Register.Types.None), showChanges);
            SetText(labelSS, GetRegisterValue(_processor.SS, Register.Types.None), showChanges);
            SetText(labelIP, GetRegisterValue(_processor.IP, Register.Types.None), showChanges);
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
        /// Возвращает значение регистра в зависимости от выбранного типа выводаs
        /// </summary>
        /// <param name="reg">Регистр</param>
        /// <param name="type">Тип регистра</param>
        /// <returns></returns>
        private string GetRegisterValue(Register reg, Register.Types type)
        {
            bool hex = MainForm.Instance.OutputHex();
            if (hex)
            {
                if (type == Register.Types.High) return reg.HighHex;
                else if (type == Register.Types.Low) return reg.LowHex;
                return reg.Hex;
            }

            string s = reg.Decimal.ToString();
            int len = 5;
            if (type == Register.Types.High)
            {
                s = reg.HighDecimal.ToString();
                len = 3;
            }
            else if (type == Register.Types.Low)
            {
                s = reg.LowDecimal.ToString();
                len = 3;
            }
            while (s.Length < len) s = "0" + s;
            return s;
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

        // Возникает при открытии подсказки
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
        }

        private void labelAH_MouseHover(object sender, EventArgs e)
        {
            const string body = "Двоичное: {0}\nБез знака: {1}\nСо знаком: {2}\nШестнадцатеричное: {3}";
            Label label = sender as Label;
            string tag = label.Tag.ToString();
            if (tag.Length != 0)
            {
                Register reg = _processor.GetRegisterByName(tag);
                toolTip1.ToolTipTitle = tag.ToUpper();
                toolTip1.Show(string.Format(body, reg.Value.Binary, reg.Decimal.ToString(), reg.SignDecimal.ToString(), reg.Hex), label);
            }
        }

        private void labelAH_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(sender as Label);
            toolTip1.ToolTipTitle = "";
        }
    }
}
