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

        /// <summary>
        /// Разбивает число на два массива по 8 бит
        /// </summary>
        /// <param name="n">Десятичное число</param>
        /// <returns>Массив по 8 бит</returns>
        public int[] SplitBy2(int n)
        {
            bool[] m = BinaryNumber.GetBinary(n);
            bool[] m1 = new bool[8]; 
            bool[] m2 = new bool[8];
            int[] s = new int[2];
            for (int i = 0; i < constNumber; i++)
            {
                if (i < 8) m1[i] = m[i];
                else m2[i - 8] = m[i];
            }
            s[0] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m1), 2); // GetBin8
            s[1] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m2), 2);
            return s;
        }

        public void Push(int n)
        {
            int[] d = SplitBy2(n);
            for (int i = 0; i < 2; i++) list.Add(d[i]);
            SP.Value.Decimal = list.Count;
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
            Push(_processor.AX.Decimal);
            Push(_processor.CX.Decimal);
            Push(_processor.DX.Decimal);
            Push(_processor.BX.Decimal);
            Push(_processor.SP.Decimal);
            Push(_processor.BP.Decimal);
            Push(_processor.SI.Decimal);
            Push(_processor.DI.Decimal);
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

        // В стек записывается значение FLAGS  // Нужно будет проверить
        // PUSHF
        public void Pushf()
        {
            Push(_processor.Flags.Decimal);
        }

        // POPF
        public void Popf()
        {
            Pop(_processor.Flags);
        }
       
    }
}
