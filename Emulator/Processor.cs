using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{   
    // Класс процессор
    class Processor
    {
        public Register AX, BX, CX, DX, SI, DI, BP, SP, CS, DS, ES, SS, IP, Flags;

        public Processor()
        {
            AX = new Register();
            BX = new Register();
            CX = new Register();
            DX = new Register();
            SI = new Register();
            DI = new Register();
            BP = new Register();
            SP = new Register();
            CS = new Register();
            DS = new Register();
            ES = new Register();
            SS = new Register();
            IP = new Register();
            Flags = new Register();
        }

        /** ADD **/
        public void Add(Register a, Register b)
        {
            a.Value += b.Value;
            a.UpdateFlags(this.Flags);
        }

        public void Add(Register a, string d, int b)
        {
            a.Value += BinaryNumber.GetDecimal(d, b);
            a.UpdateFlags(this.Flags);
        }

        /** ADC **/
        public void Adc(Register a, Register b)
        {
            a.Value += b.Value;
            if (a.Value.CarryFlag) a.Value += 1;
            a.UpdateFlags(this.Flags);
        }

        public void Adc(Register a, string n, int b)
        {
            a.Value += BinaryNumber.GetDecimal(n, b);
            if (a.Value.CarryFlag) a.Value += 1;
            a.UpdateFlags(this.Flags);
        }

        /** AND **/
        public void And(Register a, Register b)
        {
            for (int i = 0; i < 16; i++) a.Value.Number[i] = a.Value.Number[i] && b.Value.Number[i];
            a.UpdateFlags(this.Flags);
            this.Flags.SetFlag(false, Register.Flags.CF, Register.Flags.OF);
        }

        public void And(Register a, string n, int b)
        { 
            bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            for (int i = 0; i < 16; i++) a.Value.Number[i] = a.Value.Number[i] && c[i];
            a.UpdateFlags(this.Flags);
            this.Flags.SetFlag(false, Register.Flags.CF, Register.Flags.OF);
        }

        /** BSF **/
        public void Bsf(Register dest, Register src)
        {
            int pos = -1;
            for (int i = src.Value.Number.Length - 1; i >= 0; i--)
            {
                if (src.Value.Number[i])
                {
                    pos = src.Value.Number.Length - i - 1;
                    break;
                }
            }
            dest.Value.Decimal = (pos == -1 ? 0 : pos);
            this.Flags.SetFlag((pos == -1), Register.Flags.ZF);
        }

        public void Bsf(Register dest, string n, int b)
        {
            bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            int pos = -1;
            for (int i = c.Length - 1; i >= 0; i--)
            {
                if (c[i])
                {
                    pos = c.Length - i - 1;
                    break;
                }
            }
            dest.Value.Decimal = (pos == -1 ? 0 : pos);
            this.Flags.SetFlag((pos == -1), Register.Flags.ZF); 
        }

        public void Mov(Register a, Register b)
        {
            a.Value.Decimal = b.Value.Decimal;
        }

        public void Mov(Register a, string d, int b)
        {
            a.Value.Decimal = BinaryNumber.GetDecimal(d, b);
        }

        public bool IsFlag(Register.Flags f)
        {
            return this.Flags.GetFlag(f);
        }
    }
}
