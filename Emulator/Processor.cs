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
            dest.UpdateFlags(this.Flags);
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
            dest.UpdateFlags(this.Flags);
            this.Flags.SetFlag((pos == -1), Register.Flags.ZF); 
        }

        //-------------------------------------------------------------------
        /** BSR **/
        public void Bsr(Register dest, Register src)
        {
            int pos = -1;
            for (int i = 0; i < src.Value.Number.Length; i++)
            {
                if (src.Value.Number[i])
                {
                    pos = src.Value.Number.Length - i - 1;
                    break;
                }
            }
            dest.Value.Decimal = (pos == -1 ? 0 : pos);
            dest.UpdateFlags(this.Flags);
            this.Flags.SetFlag((pos == -1), Register.Flags.ZF);
        }

        public void Bsr(Register dest, string n, int b)
        {
            bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            int pos = -1;
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i])
                {
                    pos = c.Length - i - 1;
                    break;
                }
            }
            dest.Value.Decimal = (pos == -1 ? 0 : pos);
            dest.UpdateFlags(this.Flags);
            this.Flags.SetFlag((pos == -1), Register.Flags.ZF);
        }

        /** BT **/
        public void Bt(Register src, int index)
        {
            int i = src.Value.Number.Length - index - 1;
            bool b = src.Value.Number[i];
            src.UpdateFlags(this.Flags);
            this.Flags.SetFlag(b, Register.Flags.CF);
        }

        /** BTC **/
        public void Btc(Register src, int index)
        {
            int i = src.Value.Number.Length - index - 1;
            bool b = src.Value.Number[i];
            src.Value.Number[i] = (src.Value.Number[i] == true ? false : true);
            this.Flags.SetFlag(b, Register.Flags.CF);
        }

        /** BTR **/
        public void Btr(Register src, int index)
        {
            int i = src.Value.Number.Length - index - 1;
            bool b = src.Value.Number[i];
            src.Value.Number[i] = false;
            this.Flags.SetFlag(b, Register.Flags.CF);
        }

        /** BTS **/
        public void Bts(Register src, int index)
        {
            int i = src.Value.Number.Length - index - 1;
            bool b = src.Value.Number[i];
            src.Value.Number[i] = true;
            src.UpdateFlags(this.Flags);
            this.Flags.SetFlag(b, Register.Flags.CF);
        }

        /** CALL **/ //****************************************************?
        public void Call()
        { 
            
        }

        /** CBW ?**/
        public void Cbw(Register a)
        {   
            int c = 0; bool m = false;
            c = a.Value.Number.Length / 2;
            m = a.Value.Number[c];
            for (int i = 0; i < c; i++) a.Value.Number[i] = m;
        }

        /** CLC **/
        public void Clc()
        {
            this.Flags.SetFlag(false, Register.Flags.CF);
        }

        /** CLD **/
        public void Cld()
        {
            this.Flags.SetFlag(false, Register.Flags.DF);
        }

        /** CLI **/
        public void Cli()
        {
            this.Flags.SetFlag(false, Register.Flags.IF);
        }

        /** CMC **/
        public void Cmc()
        {
            this.Flags.SetFlag(!this.Flags.GetFlag(Register.Flags.CF), Register.Flags.CF);
        }

        /** CMP **/ 
        public void Cmp(Register dest, Register src)
        {
            int b = dest.Value.Decimal;
            dest.Value.Decimal = Math.Abs(dest.Value.Decimal - src.Value.Decimal);
            dest.UpdateFlags(this.Flags);
            dest.Value.Decimal = b;
        }

        /** CWD **/
        public void Cwd()
        {
            for (int i = 0; i < DX.Value.Number.Length; i++) DX.Value.Number[i] = AX.Value.Number[0];    
        }

        /** DEC **/
        public void Dec(Register a)
        {
            a.Value.Decimal = a.Value.Decimal - 1;
            a.UpdateFlags(this.Flags);
        }

        /** DIV **/ //************************************************** 
        public void Div(Register delim, Register delit)
        {
          
        }

        /** IDIV **/ //**********************************************
        public void Idiv(Register delim, Register delit)
        {

        }

        /** ENTER **/ //*******************************************
        public void Enter(Register delim, Register delit)
        {

        }

        /** IMUL **/ //******************************************
        public void Imul(Register a)
        {
           
        }
        
        public void Imul(Register a, Register b)
        {
            
        }

        /** INC **/
        public void Inc(Register a)
        {
            a.Value.Decimal = a.Value.Decimal + 1;
            a.UpdateFlags(this.Flags);
        }

        /** J(COND) **/ //*****************************************
        public void Jcond(Register delim, Register delit)
        {

        }

        /** JMP **/ //*****************************************
        public void Jmp(Register delim, Register delit)
        {
            
        }

        /** LAHF **/
        public void Lahf()
        {
            AX.Value.Number[0] = this.Flags.GetFlag(Register.Flags.SF);
            AX.Value.Number[1] = this.Flags.GetFlag(Register.Flags.ZF);
            AX.Value.Number[2] = false;
            AX.Value.Number[3] = this.Flags.GetFlag(Register.Flags.AF);
            AX.Value.Number[4] = false;
            AX.Value.Number[5] = this.Flags.GetFlag(Register.Flags.PF);
            AX.Value.Number[6] = true;
            AX.Value.Number[7] = this.Flags.GetFlag(Register.Flags.CF);
        }

        /** LEAVE **/
        //*****************************************
        public void Leave()
        {

        }

        /** LOOP **/ //*****************************************(после RET)
        public void Loop()
        {

        }

        /** LOOPNZ **/ //*****************************************(после RET)
        public void Loopnz()
        {

        }

        /** LOOPZ **/ //*****************************************(после RET)
        public void Loopz()
        {

        }

        /** MOVSX **/ //????????????????????????????
        public void Movsx(Register dest, Register src)
        {
            for (int i = 0; i < dest.Value.Number.Length / 2; i++) dest.Value.Number[i] = src.Value.Number[0];
            for (int i = dest.Value.Number.Length/2; i < dest.Value.Number.Length; i++) dest.Value.Number[i] = src.Value.Number[i];
            dest.UpdateFlags(this.Flags);
        }


        /** MOVZX **/ //????????????????????????????
        public void Movzx(Register dest, Register src)
        {
            for (int i = 0; i < dest.Value.Number.Length / 2; i++) dest.Value.Number[i] = false;
            for (int i = dest.Value.Number.Length / 2; i < dest.Value.Number.Length; i++) dest.Value.Number[i] = src.Value.Number[i];
            dest.UpdateFlags(this.Flags);
        }

        /** MUL **/ //*****************************************
        public void Mul()
        {

        }

        /** NEG **/ // флаги
        public void Neg(Register a)
        {
            for (int i = 0; i < a.Value.Number.Length; i++) a.Value.Number[i] = !a.Value.Number[i];
            a.Value.Decimal += 1;
            a.UpdateFlags(this.Flags);
            if (a.Value.Decimal == 0) this.Flags.SetFlag(false, Register.Flags.CF);
            this.Flags.SetFlag(true, Register.Flags.CF);
        }

        /** NOP **/ //*****************************************
        public void Nop()
        {

        }

        /** NOT **/
        public void Not(Register a)
        {
            for (int i = 0; i < a.Value.Number.Length; i++) a.Value.Number[i] = !a.Value.Number[i];
        }

        /** OR **/
        public void Or(Register dest, Register src)
        {
            for (int i = 0; i < dest.Value.Number.Length; i++)
            {
                if (dest.Value.Number[i] == true || src.Value.Number[i] == true) dest.Value.Number[i] = true;
                else dest.Value.Number[i] = false;
            }
            dest.UpdateFlags(this.Flags);
            this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);
                
        }

        public void Or(Register dest, string n, int b)
        {
            bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            for (int i = 0; i < dest.Value.Number.Length; i++)
            {
                if (dest.Value.Number[i] == true || c[i] == true) dest.Value.Number[i] = true;
                else dest.Value.Number[i] = false;
            }
            dest.UpdateFlags(this.Flags);
            this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);

        }

        /** POP **/ //*****************************************
        public void Pop()
        {

        }

        /** POPA **/ //*****************************************
        public void Popa()
        {

        }

        /** POPF **/  //*****************************************
        public void Popf()
        {

        }

        /** PUSH **/ //*****************************************
        public void Push()
        {

        }

        /** PUSHA **/ //*****************************************
        public void Pusha()
        {

        }

        /** PUSHF **/ //*****************************************
        public void Pushf()
        {

        }

        /** RCL **/ 
        public void Rcl(Register a, int n)
        {
            bool c = false;
            int m = a.Value.Number.Length;
            bool z = a.Value.Number[0]; 
            for (int j = 0; j < n; j++)
                for (int i = 1; i < m; i++)
                {
                    c = a.Value.Number[0];
                    a.Value.Number[i - 1] = a.Value.Number[i];
                }           
            a.Value.Number[m - 1] = this.Flags.GetFlag(Register.Flags.CF);
            a.UpdateFlags(this.Flags);
            this.Flags.SetFlag(c, Register.Flags.CF);
            this.Flags.SetFlag((a.Value.Number[0] != z), Register.Flags.OF);
        }

         /** RCR **/
         public void Rcr(Register a, int n)
         {
             bool c = false;
             bool z = a.Value.Number[0];
             int m = a.Value.Number.Length;
             for (int j = 0; j < n; j++)
                 for (int i = (m - 2); i >= 0; i--)
                 {
                     c = a.Value.Number[m - 1];
                     a.Value.Number[i + 1] = a.Value.Number[i];
                 } 
             a.Value.Number[0] = this.Flags.GetFlag(Register.Flags.CF);
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((a.Value.Number[0] != z), Register.Flags.OF);
         }

         /** RET **/ //*****************************************
         public void Ret()
         {

         }

         /** ROL **/ 
         public void Rol(Register a, int n)
         {
             bool c = false;
             bool z = a.Value.Number[0];
             int m = a.Value.Number.Length;
             for (int j = 0; j < n; j++)
                 for (int i = 1; i < m; i++)
                 {
                     c = a.Value.Number[0];
                     a.Value.Number[i - 1] = a.Value.Number[i];
                 }
             a.Value.Number[m - 1] = c;
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((a.Value.Number[0] != z), Register.Flags.OF);
         }

         /** ROR **/
         public void Ror(Register a, int n)
         {
             bool c = false;
             bool z = a.Value.Number[0];
             int m = a.Value.Number.Length;
             for (int j = 0; j < n; j++)
                 for (int i = (m - 2); i >= 0; i--)
                 {
                     c = a.Value.Number[m - 1];
                     a.Value.Number[i + 1] = a.Value.Number[i];
                 } 
             a.Value.Number[0] = c;
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((a.Value.Number[0] != z), Register.Flags.OF);
         }

         /** SAHF **/ 
         public void Shahf()
         {
             for (int i = 0; i < AX.Value.Number.Length / 2; i++)
             {
                 if ( i != 1 && i != 3 && i != 5) this.Flags.Value.Number[i] = AX.Value.Number[i];
             }
         }

         /** SAL **/
         public void Sal(Register a, int n)
         {
             bool c = false;
             bool z = a.Value.Number[0];
             int m = a.Value.Number.Length;
             for (int j = 0; j < n; j++)
             {        
                 for (int i = 1; i < m; i++)
                 {
                     c = a.Value.Number[0];
                     a.Value.Number[i - 1] = a.Value.Number[i];
                 } 
             }
             a.Value.Number[m - 1] = false;
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((a.Value.Number[0] != z), Register.Flags.OF);
         }

         /** SAR **/
         public void Sar(Register a, int n)
         {
             int m = a.Value.Number.Length;
             bool b = a.Value.Number[0];
             bool c = false;
             for (int j = 0; j < n; j++)
             {
                 for (int i = (m - 2); i >= 0; i--) 
                 {
                     c = a.Value.Number[m - 1];
                     a.Value.Number[i + 1] = a.Value.Number[i];
                 }
                 a.Value.Number[0] = b;
             }        
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             if (n == 1) this.Flags.SetFlag(false, Register.Flags.OF);
         }

         /** SBB **/
         public void Sbb(Register dest, Register src)
         {
             dest.Value.Decimal = dest.Value.Decimal - src.Value.Decimal - ((this.Flags.GetFlag(Register.Flags.CF)) ? 1 : 0);
             dest.UpdateFlags(this.Flags);
         }

         public void Sbb(Register dest, string n, int b)
         {
             bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b)); 
             dest.Value.Decimal = dest.Value.Decimal - BinaryNumber.GetDecimal(c) - ((this.Flags.GetFlag(Register.Flags.CF)) ? 1 : 0);
             dest.UpdateFlags(this.Flags);
         }

         /** SHL **/
         public void Shl(Register a, int n)
         {
             Sal(a, n);
         }

         /** SHLD **/
         public void Shld(Register dest, Register src, int n)
         {
             int m = dest.Value.Number.Length;
             {
                 bool c = false;
                 bool z = dest.Value.Number[0];
                 for (int j = 0; j < n; j++)
                 {
                     for (int i = 1; i < m; i++)
                     {
                         c = dest.Value.Number[0];
                         dest.Value.Number[i - 1] = dest.Value.Number[i];
                     }
                     dest.Value.Number[m - 1] = src.Value.Number[m - j - 1];
                 }
                 dest.UpdateFlags(this.Flags);
                 this.Flags.SetFlag(c, Register.Flags.CF);
                 this.Flags.SetFlag((dest.Value.Number[0] != z && n == 1), Register.Flags.OF);
             }
         }

         /** SHR **/
         public void Shr(Register a, int n)
         {
             int m = a.Value.Number.Length;
             bool c = false;
             for (int j = 0; j < n; j++)
             {
                 for (int i = (m - 2); i >= 0; i--)
                 {
                     c = a.Value.Number[0];
                     a.Value.Number[i + 1] = a.Value.Number[i];
                 } 
                 a.Value.Number[0] = false;
             }   
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             if (n > 1) this.Flags.SetFlag(false, Register.Flags.OF);
         }

         /** SHRD **/
         public void Shrd(Register dest, Register src, int n)
         {
             int m = dest.Value.Number.Length;
             {
                 bool c = false;
                 bool z = dest.Value.Number[0];
                 for (int j = 0; j < n; j++)
                 {
                     for (int i = (m - 2); i >= 0; i--)
                     {
                         c = dest.Value.Number[m - 1];
                         dest.Value.Number[i + 1] = dest.Value.Number[i];
                     }
                     dest.Value.Number[0] = src.Value.Number[m - j - 1];
                 }
                 dest.UpdateFlags(this.Flags);
                 this.Flags.SetFlag(c, Register.Flags.CF);
                 this.Flags.SetFlag((dest.Value.Number[0] != z), Register.Flags.OF);
             }
         }

         /** STC **/
         public void Stc()
         {
             this.Flags.SetFlag(true, Register.Flags.CF);
         }

         /** STD **/
         public void Std()
         {
             this.Flags.SetFlag(true, Register.Flags.DF);
         }

         /** STI **/
         public void Sti()
         {
             this.Flags.SetFlag(true, Register.Flags.IF);
         }

         /** SUB **/
         public void Sub(Register a, Register b)
         {
             a.Value.Decimal -= b.Value.Decimal;
             a.UpdateFlags(this.Flags);
         }

         public void Sub(Register a, string n, int b)
         {
             bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
             a.Value.Decimal -= BinaryNumber.GetDecimal(c);
             a.UpdateFlags(this.Flags);
         }

         /** TEST **/
         public void Test(Register dest, Register src)
         {
             for (int i = 0; i < dest.Value.Number.Length; i++)
             {
                 if (dest.Value.Number[i] == true & src.Value.Number[i] == true) dest.Value.Number[i] = true;
                 else dest.Value.Number[i] = false;
             }
             dest.UpdateFlags(this.Flags);
             this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);

         }

         public void Test(Register dest, string n, int b)
         {
             bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
             for (int i = 0; i < dest.Value.Number.Length; i++)
             {
                 if (dest.Value.Number[i] == true & c[i] == true) dest.Value.Number[i] = true;
                 else dest.Value.Number[i] = false;
             }
             dest.UpdateFlags(this.Flags);
             this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);

         }

         /** XCHG **/
         public void Xchg(Register a, Register b)
         {
             bool[] c = new bool[16];
             for (int i = (a.Value.Number.Length - 1); i >= 0; i--)
             {
                 c[i] = a.Value.Number[i];
                 a.Value.Number[i] = b.Value.Number[i];
                 b.Value.Number[i] = c[i];
             }
         }

         /** XOR **/
         public void Xor(Register dest, Register src)
         {
             for (int i = 0; i < dest.Value.Number.Length; i++)
             {
                 if (dest.Value.Number[i] == src.Value.Number[i]) dest.Value.Number[i] = true;
                 else dest.Value.Number[i] = false;
             }
             dest.UpdateFlags(this.Flags);
             this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);
         }

         public void Xor(Register dest, string n, int b)
         {
             bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
             for (int i = 0; i < dest.Value.Number.Length; i++)
             {
                 if (dest.Value.Number[i] == c[i]) dest.Value.Number[i] = true;
                 else dest.Value.Number[i] = false;
             }
             dest.UpdateFlags(this.Flags);
             this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);

         }
         //-----------------------------------------------------

         /** MOV **/
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
