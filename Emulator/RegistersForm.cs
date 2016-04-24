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

        public void UpdateView()
        {
            labelAH.Text = _processor.AX.HighHex;
            labelAL.Text = _processor.AX.LowHex;
            labelBH.Text = _processor.BX.HighHex;
            labelBL.Text = _processor.BX.LowHex;
            labelCH.Text = _processor.CX.HighHex;
            labelCL.Text = _processor.CX.LowHex;
            labelDH.Text = _processor.DX.HighHex;
            labelDL.Text = _processor.DX.LowHex;
            labelSI.Text = _processor.SI.Hex;
            labelDI.Text = _processor.DI.Hex;
            labelBP.Text = _processor.BP.Hex;
            labelSP.Text = _processor.SP.Hex;
            labelCS.Text = _processor.CS.Hex;
            labelDS.Text = _processor.DS.Hex;
            labelES.Text = _processor.ES.Hex;
            labelSS.Text = _processor.SS.Hex;
            labelIP.Text = _processor.IP.Hex;
            labelFlagOF.Text = _processor.Flags.GetFlag(Register.Flags.OF) ? "1" : "0";
            labelFlagDF.Text = _processor.Flags.GetFlag(Register.Flags.DF) ? "1" : "0";
            labelFlagIF.Text = _processor.Flags.GetFlag(Register.Flags.IF) ? "1" : "0";
            labelFlagTF.Text = _processor.Flags.GetFlag(Register.Flags.TF) ? "1" : "0";
            labelFlagSF.Text = _processor.Flags.GetFlag(Register.Flags.SF) ? "1" : "0";
            labelFlagZF.Text = _processor.Flags.GetFlag(Register.Flags.ZF) ? "1" : "0";
            labelFlagAF.Text = _processor.Flags.GetFlag(Register.Flags.AF) ? "1" : "0";
            labelFlagPF.Text = _processor.Flags.GetFlag(Register.Flags.PF) ? "1" : "0";
            labelFlagCF.Text = _processor.Flags.GetFlag(Register.Flags.CF) ? "1" : "0";
        }
    }
}
