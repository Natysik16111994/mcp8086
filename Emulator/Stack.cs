using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{   
    // Класс Стек
    class Stack
    {
        public List<int> list;
        private const int constNumber = 16;
        private Register SP;

        public Stack(Register sp)
        {
            list = new List<int>();
            this.SP = sp;
            SP.Value.Decimal = 0;
        }

        // Разбиваем массив на два по 8 бит
        public int[] Razb(string n, int b)
        {
            bool[] m = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            bool[] m1 = new bool[8]; bool[] m2 = new bool[8];
            int[] s = new int[2];
            for (int i = 0; i < constNumber; i++)
                if (i < 8) m1[i] = m[i];
                else  m2[i-8] = m[i];
            s[0] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m1), 2); // GetBin8
            s[1] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m2), 2);
            return s;
        }

        public int[] Razb(Register a)
        {
            bool[] m1 = new bool[8]; bool[] m2 = new bool[8];
            int[] s = new int[2];
            for (int i = 0; i < constNumber; i++)
                if (i < 8) { m1[i] = a.Value.Number[i]; Console.WriteLine("m1: " + m1[i]);}
                else { m2[i-8] = a.Value.Number[i]; Console.WriteLine("m2: " + m2[i-8]); }
            s[0] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m1), 2);
            s[1] = BinaryNumber.GetDecimal(BinaryNumber.GetBinaryString8(m2), 2);
            return s;
        }

        // Команда Push - запись в стек
        public void Push(string n, int b)
        {
            int z = SP.Value.Decimal;
            int[] d = Razb(n, b);
            for (int i = z; i < (z + 2); i++) 
                for (int j = 0; j < 2; j++) list[i] = d[j];
            z = z + 2;
            SP.Value.Decimal = z;
        }

        public void Push(Register a)
        {
            int z = 0; //SP.Value.Decimal;
            int[] d = Razb(a);
            Console.WriteLine("d1: " + d[0]); Console.WriteLine(d[1]);
            for (int i = z; i < (z + 2); i++)
                for (int j = 0; j < 2; j++) list[i] = d[j];
            z = z + 2;
            SP.Value.Decimal = z;
        }

    /* // Команда Pop - взять из стека
        public void Pop(Register a)
        {
            int z = SP.Value.Decimal;
            //int[] d = Razb(a);
           // for (int i = z; i < (z + 2); i++)
                //for (int j = 0; j < 2; j++) list[i] = d[j];
            for (int i = 0; i < constNumber; i++)
                if (i < 8) a.Value.Number[i] = BinaryNumber.GetBinary(list[i]);
            z = z - 2;
            SP.Value.Decimal = z;
        }
       */
    }
}
