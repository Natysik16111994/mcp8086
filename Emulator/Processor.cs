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

        public void Add(Register a, Register b)
        {
            a.Value += b.Value;
            a.UpdateFlags(Flags);
        }

        public void Add(Register a, string d, int b)
        {
            a.Value.Dec += Number.ConvertToDec(d, b);
        }

        public void Mov(Register a, Register b)
        {
            a.Value.Dec = b.Value.Dec;
        }

        public void Mov(Register a, string d, int b)
        {
            a.Value.Dec = Number.ConvertToDec(d, b);
        }

        public bool IsOverflowFlag()
        {
            return Flags.GetFlag(Register.Flags.OF);
        }

        public bool IsCarryFlag()
        {
            return Flags.GetFlag(Register.Flags.CF);
        }
    }
}
