using System;
using System.Collections.Generic;
using System.Text;
namespace Emulator
{   
    using RT = Register.Types;

    // Класс процессор
    public class Processor
    {
        public Register AX, BX, CX, DX, SI, DI, BP, SP, CS, DS, ES, SS, IP, Flags;
        public Stack stack;
        private Assembler _assembler;
        private Register _privateRegister;

        // Конструктор
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

            stack = new Stack(this);
            _assembler = new Assembler(this);
            _privateRegister = new Register();
        }

        /// <summary>
        /// Возвращает экземпляр ассемблера
        /// </summary>
        /// <returns></returns>
        public Assembler GetAssembler()
        {
            return _assembler;
        }
        
        /// <summary>
        /// Возвращает регистр по имени
        /// </summary>
        /// <param name="name">Имя регистра</param>
        /// <returns>Возвращает регистр если есть, в ином случае null.</returns>
        public Register GetRegisterByName(string name)
        {
            if (name.Length != 2) return null;
            name = name.ToLower(); // переводит в нижний регистр
            if (name.Substring(1, 1) == "l" || name.Substring(1, 1) == "h") name = name.Substring(0, 1) + "x";
            if (name == "ax") return this.AX;
            else if (name == "bx") return this.BX;
            else if (name == "cx") return this.CX;
            else if (name == "dx") return this.DX;
            else if (name == "si") return this.SI;
            else if (name == "di") return this.DI;
            else if (name == "bp") return this.BP;
            else if (name == "sp") return this.SP;
            else if (name == "cs") return this.CS;
            else if (name == "ds") return this.DS;
            else if (name == "es") return this.ES;
            else if (name == "ss") return this.SS;
            else if (name == "ip") return this.IP;
            return null;
        }

        /// <summary>
        /// Сбрасывает состояние регистров
        /// </summary>
        public void ResetRegisters()
        {
            AX.Decimal = BX.Decimal = CX.Decimal = DX.Decimal = 0;
            SI.Decimal = DI.Decimal = 0;
            BP.Decimal = SP.Decimal = 0;
            CS.Decimal = DS.Decimal = ES.Decimal = SS.Decimal = 0;
            IP.Decimal = 0;
            Flags.Decimal = 0;
        }

        /** ADD **/
        public void Add(Register a, object b, RT at, RT bt)
        {
            SetRegisterValue(a, at, GetRegisterValue(a, at) + GetValueFromObject(b, bt));
            a.UpdateFlags(this.Flags);
        }

        /** ADC **/
        public void Adc(Register a, object b, RT at, RT bt)
        {
            SetRegisterValue(a, at, GetRegisterValue(a, at) + GetValueFromObject(b, bt));
            if (a.Value.CarryFlag) a.Decimal += 1;
            a.UpdateFlags(this.Flags);
        }

        /** AND **/
        public void And(Register a, object b, RT at, RT bt)
        {
            bool[] reg1 = BinaryNumber.GetBinary(GetRegisterValue(a, at));
            bool[] reg2 = BinaryNumber.GetBinary(GetValueFromObject(b, bt));
            for (int i = 0; i < 16; i++) reg1[i] &= reg2[i];
            SetRegisterValue(a, at, BinaryNumber.GetDecimal(reg1));
            a.UpdateFlags(this.Flags);
            this.Flags.SetFlag(false, Register.Flags.CF, Register.Flags.OF);
        }

        /** BSF **/
        public void Bsf(Register dest, object src)
        {
            bool[] c = BinaryNumber.GetBinary(GetValueFromObject(src));
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
        public void Bsr(Register dest, object src)
        {
            bool[] c = BinaryNumber.GetBinary(GetValueFromObject(src));
            int pos = 0;
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
        public void Cmp(object a, object b, RT at, RT bt)
        {
            int av = GetValueFromObject(a, at);
            int bv = GetValueFromObject(b, bt);
            _privateRegister.Decimal = Math.Abs(av - bv);
            _privateRegister.UpdateFlags(this.Flags);
        }

        /** CWD **/
        public void Cwd()
        {
            for (int i = 0; i < DX.Value.Number.Length; i++) DX.Value.Number[i] = AX.Value.Number[0];    
        }

        /** DEC **/
        public void Dec(Register a, RT at)
        {
            SetRegisterValue(a, at, GetRegisterValue(a, at) - 1);
            a.UpdateFlags(this.Flags);
        }

        /** DIV **/
        public void Div(object delit, RT delitT)
        {
            int d = GetValueFromObject(delit, delitT);
            if (d > 255) // делим DX:AX
            {
                int sum = BinaryNumber.GetDecimal(DX.Value.Binary + AX.Value.Binary, 2);
                AX.Decimal = sum / d;
                DX.Decimal = sum % d;
            }
            else // делим AX
            {
                AX.Decimal = AX.Decimal / d;
            }
            AX.UpdateFlags(this.Flags);
        }

        /** IDIV **/ //******************************************************
        public void Idiv(object delit, RT delitT)
        {
        }
        
        /** ENTER **/ //*******************************************
        public void Enter()
        {

        }

        /** IMUL **/ //******************************************
        public void Imul(object m, RT mt)
        {
        }

        /** INC **/
        public void Inc(Register a, RT at)
        {
            SetRegisterValue(a, at, GetValueFromObject(a, at) + 1);
        }

        /** INT **/
        public void Int(object a)
        {
        }

        /** J(COND) **/ //*****************************************
        /** JZ/JE **/
        public void JZ(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.ZF)) _assembler.JumpOnLabel((string)a);
        }

        /** JNZ **/
        public void JNZ(object a)
        {
            if(!this.Flags.GetFlag(Register.Flags.ZF)) _assembler.JumpOnLabel((string)a);
        }

        /** JC **/
        public void JC(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.CF)) _assembler.JumpOnLabel((string)a);
        }

        /** JNC **/
        public void JNC(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.CF)) _assembler.JumpOnLabel((string)a);
        }

        /** JP **/
        public void JP(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.PF)) _assembler.JumpOnLabel((string)a);
        }

        /** JNP **/
        public void JNP(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.PF)) _assembler.JumpOnLabel((string)a);
        }

        /** JS **/
        public void JS(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.SF)) _assembler.JumpOnLabel((string)a);
        }

        /** JNS **/
        public void JNS(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.SF)) _assembler.JumpOnLabel((string)a);
        }

        /** JO **/
        public void JO(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.OF)) _assembler.JumpOnLabel((string)a);
        }

        /** JNO **/
        public void JNO(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.OF)) _assembler.JumpOnLabel((string)a);
        }

        /** JA **/
        public void JA(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.CF) && !this.Flags.GetFlag(Register.Flags.ZF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JNA **/
        public void JNA(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.CF) && this.Flags.GetFlag(Register.Flags.ZF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JG **/
        public void JG(object a)
        {
            if (!this.Flags.GetFlag(Register.Flags.ZF) &&
                this.Flags.GetFlag(Register.Flags.SF) == this.Flags.GetFlag(Register.Flags.OF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JGE **/
        public void JGE(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.SF) == this.Flags.GetFlag(Register.Flags.OF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JL **/
        public void JL(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.SF) != this.Flags.GetFlag(Register.Flags.OF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JLE **/
        public void JLE(object a)
        {
            if (this.Flags.GetFlag(Register.Flags.ZF) &&
                this.Flags.GetFlag(Register.Flags.SF) != this.Flags.GetFlag(Register.Flags.OF))
                _assembler.JumpOnLabel((string)a);
        }

        /** JCXZ **/
        public void JCXZ(object a)
        {
            if (this.CX.Decimal == 0)
                _assembler.JumpOnLabel((string)a);
        }

        /** JMP **/ //*****************************************
        public void Jmp(object a)
        {
            _assembler.JumpOnLabel((string)a);
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
        public void Loop(object a)
        {
            CX.Decimal--;
            if (CX.Decimal != 0)
                _assembler.JumpOnLabel((string)a);
        }

        /** LOOPNZ **/ //*****************************************(после RET)
        public void Loopnz(object a)
        {
            CX.Decimal--;
            if (CX.Decimal != 0 && !this.Flags.GetFlag(Register.Flags.ZF))
                _assembler.JumpOnLabel((string)a);
        }

        /** LOOPZ **/ //*****************************************(после RET)
        public void Loopz(object a)
        {
            CX.Decimal--;
            if (CX.Decimal != 0 && this.Flags.GetFlag(Register.Flags.ZF))
                _assembler.JumpOnLabel((string)a);
        }

        public void Movsb()
        {   
        }

        public void Movsw()
        {
        }

        /** MUL **/
        public void Mul(object a, RT at)
        {
            int m = GetValueFromObject(a, at);
            if (m > 255) // (DX:AX)
            {
                int ax = AX.Decimal;
                AX.SetZero(); DX.SetZero();
                bool[] bin = BinaryNumber.GetBinary32(m * ax);
                for (int i = 0; i < AX.Value.Number.Length; i++)
                {
                    DX.Set(i, bin[i]);
                    AX.Set(i, bin[i + 16]);
                }
                AX.UpdateFlags(this.Flags);
                if (DX.Value.Decimal == 0) this.Flags.SetFlag(false, Register.Flags.OF, Register.Flags.CF);
            }
            else 
            {
                AX.Decimal = AX.LowDecimal * m;
                AX.UpdateFlags(this.Flags);
            }
        }
          
        /** NEG **/
        public void Neg(Register a, RT at)
        {
            int d = GetRegisterValue(a, at);
            bool[] bin = BinaryNumber.GetBinary(d);
            for (int i = 0; i < bin.Length; i++) bin[i] = !bin[i];
            SetRegisterValue(a, at, BinaryNumber.GetDecimal(bin) + 1);
            a.UpdateFlags(this.Flags);
        }

        /** NOP **/
        //*****************************************
        public void Nop()
        {
        }

        /** NOT **/
        public void Not(Register a, RT at)
        {
            bool[] bin = BinaryNumber.GetBinary(GetRegisterValue(a, at));
            for (int i = 0; i < bin.Length; i++) bin[i] = !bin[i];
            SetRegisterValue(a, at, BinaryNumber.GetDecimal(bin));
            //a.UpdateFlags(this.Flags);
        }

        /** OR **/
        public void Or(Register a, object b, RT at, RT bt)
        {
            bool[] bin1 = BinaryNumber.GetBinary(GetRegisterValue(a, at));
            bool[] bin2 = BinaryNumber.GetBinary(GetValueFromObject(b, bt));
            for (var i = 0; i < bin1.Length; i++) bin1[i] |= bin2[i];
            SetRegisterValue(a, at, BinaryNumber.GetDecimal(bin1));
            a.UpdateFlags(this.Flags);
        }

        /** POP **/
        public void Pop(Register a)
        {
            stack.Pop(a);
        }

        /** POPA **/
        public void Popa()
        {
            stack.Popa();
        }

        /** POPF **/
        public void Popf()
        {
            stack.Popf();
        }

        /** PUSH **/
        public void Push(object a, RT at)
        {
            stack.Push(GetValueFromObject(a, at));
        }

        /** PUSHA **/
        public void Pusha(object a, RT at)
        {
            stack.Pusha();
        }

        /** PUSHF **/
        public void Pushf(object a, RT at)
        {
            stack.Pushf();
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
             _assembler.Ret();
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
         public void Sahf()
         {
             for (int i = 0; i < AX.Value.Number.Length / 2; i++)
             {
                 if ( i != 1 && i != 3 && i != 5) this.Flags.Value.Number[i] = AX.Value.Number[i];
             }
         }

         /** SAL **/
         public void Sal(Register a, int n, RT at)
         {
             bool[] bin = BinaryNumber.GetBinaryA(GetRegisterValue(a, at), at);
             bool c = bin[0];
             for (int i = 0; i < bin.Length - 1; i++) bin[i] = bin[i + 1];
             bin[bin.Length - 1] = false;
             SetRegisterValue(a, at, bin);
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((c == bin[0]), Register.Flags.OF);
         }

         /** SAR **/
         public void Sar(Register a, int n, RT at)
         {
             bool[] bin = BinaryNumber.GetBinaryA(GetRegisterValue(a, at), at);
             bool s = bin[0]; bool c = bin[bin.Length - 1];
             for (int i = bin.Length - 1; i > 0; i--) bin[i] = bin[i - 1];
             bin[0] = s; bin[bin.Length - 1] = false;
             SetRegisterValue(a, at, bin);
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag((s == bin[0]), Register.Flags.OF);
         }

         /** SBB **/
         public void Sbb(Register a, object b, RT at, RT bt)
         {
             SetRegisterValue(a, at, GetValueFromObject(b, bt) + (this.Flags.GetFlag(Register.Flags.CF) ? 1 : 0));
             a.UpdateFlags(this.Flags);
         }

         /** SHR **/
         public void Shr(Register a, int b, RT at)
         {
             bool[] bin = BinaryNumber.GetBinaryA(GetRegisterValue(a, at), at);
             bool c = bin[bin.Length - 1];
             for (int i = bin.Length - 1; i > 0; i--) bin[i] = bin[i - 1];
             bin[0] = false;
             SetRegisterValue(a, at, bin);
             a.UpdateFlags(this.Flags);
             this.Flags.SetFlag(c, Register.Flags.CF);
             this.Flags.SetFlag(!c, Register.Flags.OF);
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
         public void Sub(Register a, object b, RT at, RT bt)
         {
             SetRegisterValue(a, at, GetRegisterValue(a, at) - GetValueFromObject(b, bt));
             a.UpdateFlags(this.Flags);
         }

         /** TEST **/
         public void Test(object a, object b, RT at, RT bt)
         {
             bool[] bin1 = BinaryNumber.GetBinary(GetValueFromObject(a, at));
             bool[] bin2 = BinaryNumber.GetBinary(GetValueFromObject(b, bt));
             for (int i = 0; i < bin1.Length; i++)
             {
                 _privateRegister.Value.Number[i] = bin1[i] && bin2[i];
             }
             _privateRegister.UpdateFlags(this.Flags);
             this.Flags.SetFlag(false, Register.Flags.CF, Register.Flags.OF);
         }

         /** XCHG **/
         public void Xchg(Register a, Register b, RT at, RT bt)
         {
             int val = GetRegisterValue(b, bt);
             SetRegisterValue(b, bt, GetRegisterValue(a, at));
             SetRegisterValue(a, at, val);
         }

         /** XOR **/
         public void Xor(Register a, object b, RT at, RT bt)
         {
             bool[] bin1 = BinaryNumber.GetBinary(GetRegisterValue(a, at));
             bool[] bin2 = BinaryNumber.GetBinary(GetValueFromObject(b, bt));
             for (var i = 0; i < bin1.Length; i++) bin1[i] ^= bin2[i];
             SetRegisterValue(a, at, BinaryNumber.GetDecimal(bin1));
             a.UpdateFlags(this.Flags);
         }

         //-----------------------------------------------------

         /** MOV **/
         public void Mov(Register a, object val, RT at, RT bt)
         {
             SetRegisterValue(a, at, GetValueFromObject(val, bt));
         }

        //---------------------------------------------------------
        // Извлекает значение из объекта
        public int GetValueFromObject(object obj, RT type = RT.None)
        {
            if(obj is Register)
            {
                Register a = (Register)obj;
                if(type == RT.None) return a.Decimal;
                else return (type == RT.High ? a.HighDecimal : a.LowDecimal);
            }
            return (int)obj;
        }

        //---------------------------------------------------------
        // Устанавливает значение в регистр
        public void SetRegisterValue(Register a, Register.Types type, int n)
        {
            if (type == Register.Types.None) a.Decimal = n;
            else if (type == Register.Types.High) a.HighDecimal = n;
            else if (type == Register.Types.Low) a.LowDecimal = n;
        }

        public void SetRegisterValue(Register a, Register.Types type, bool[] bin)
        {
            SetRegisterValue(a, type, BinaryNumber.GetDecimal(bin));
        }

        // Возвращает значение из регистра
        public int GetRegisterValue(Register a, Register.Types type = RT.None)
        {
            return (type == Register.Types.None ? a.Decimal : (type == Register.Types.High ? a.HighDecimal : a.LowDecimal));
        }

        // Возвращает длину
        public int GetBinLength(RT type)
        {
            return type == RT.None ? 16 : 8;
        }


        public bool IsFlag(Register.Flags f)
        {
            return this.Flags.GetFlag(f);         
        }
    }
}
