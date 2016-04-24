using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{   
    // Класс Стек
    public class Stack
    {
        public List<int> list;
        private const int constNumber = 16;
        private Processor _processor;
        private Register SP;

        public Stack(Processor proc)
        {
            list = new List<int>();
            this._processor = proc;
            this.SP = this._processor.SP;
        }

        // Разбиваем массив на два по 8 бит
        public int[] Razb(string n, int b)
        {
            bool[] m = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            bool[] m1 = new bool[8]; bool[] m2 = new bool[8];
            int[] s = new int[2];
            for (int i = 0; i < constNumber; i++)
                if (i < 8) m1[i] = m[i];
                else  m2[i - 8] = m[i];
            s[0] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m1), 2); // GetBin8
            s[1] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m2), 2);
            return s;
        }

        public int[] Razb(Register a)
        {
            bool[] m1 = new bool[8]; bool[] m2 = new bool[8];
            int[] s = new int[2];
            for (int i = 0; i < constNumber; i++)
                if (i < 8) m1[i] = a.Value.Number[i];
                else  m2[i - 8] = a.Value.Number[i];
            s[0] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m1), 2);
            s[1] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m2), 2);
            return s;
        }

        // Команда Push - запись в стек
        public void Push(string n, int b)
        {
            int z = list.Count;
            int[] d = Razb(n, b);
            for (int i = 0; i < 2; i++) list.Add(d[i]);
            z = list.Count; 
            SP.Value.Decimal = z;
        }

        public void Push(Register a)
        {
            int z = SP.Value.Decimal;
            int[] d = Razb(a);
            for (int i = 0; i < 2; i++) list.Add(d[i]);
            z = list.Count; 
            SP.Value.Decimal = z;
        }

        // Команда Pop - взять из стека
        public void Pop(Register a)
        {
            bool[] b = new bool[8];
            int z = list.Count;
            for (int i = 0; i < constNumber; i++)
            {
                if (i < 8)
                {
                    b = BinaryNumber.GetBinary(list[z - 2]);
                    a.Value.Number[i] = b[14 - i];
                }
                else
                {
                    b = BinaryNumber.GetBinary(list[z - 1]);
                    a.Value.Number[i] = b[i]; 
                }
             }
            list.RemoveAt(z - 1); list.RemoveAt(z - 2);
            z = list.Count; 
            SP.Value.Decimal = z;
        }

        // PUSHA   // SP меняется, т.к. это указатель и выводим через MainForm после всех преобразоний(команд). Но в стеке он сохранен верно.
        public void Pusha()
        {
            Push(_processor.AX);
            Push(_processor.CX);
            Push(_processor.DX);
            Push(_processor.BX);
            Push(_processor.SP);
            Push(_processor.BP);
            Push(_processor.SI);
            Push(_processor.DI);
        }

        // POPA
        public void Popa()
        {
            Pop(_processor.DI);
            Pop(_processor.SI);
            Pop(_processor.BP);
            Pop(_processor.SP);
            Pop(_processor.BX);
            Pop(_processor.DX);
            Pop(_processor.CX);
            Pop(_processor.AX);   
        }
    // PUSHF
    // POPF
       
    }
}
