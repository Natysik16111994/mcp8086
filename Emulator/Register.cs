using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    // Класс Регистр
    class Register
    {
        private bool[] Bits;

        public bool[] Value
        {
            get
            {
                return this.Bits;
            }
            set
            {
                this.Bits = value;
            }
        }

        // Флаги
        public enum Flags
        {
            CF = 0,
            OF = 1,
            ZF = 2,
            SF = 3,
            PF = 4,
            AF = 5,
            DF = 6,
            IF = 7,
            TF = 8
        };

        // Метод Регистр. Обнуляем все 16 бит
        public Register()
        {
            Bits = new bool[16]; 
            FillBits(false);
        }
        // Заполняем биты (разряды)
        public void FillBits(bool b)
        {
            for (int i = 0; i < 16; i++)
                Bits[i] = b;
        }
         
        // Из булевых значений переводим в стринговые "0" и "1"
        override public string ToString()
        {
            string a = "";
            for (int i = 0; i < 16; i++)
            {
                if (Bits[i]) a += "1";
                else a += "0";
            }
            return a;
        }

        public bool[] GetBits(int start, int length)
        {
            bool[] m = new bool[length];
            for (int i = start; i < start + length; i ++) m[start - i] = Bits[i];
            return m;
        }
        
        // Разделяем 16 битный регистр на два других по 8 бит 
        // Записываем старший регистр (high)
       /* public string GetH()
        {
            return Number.BinToHex(GetBits(0, 8));
        }
        // Записываем младший регистр (low)
        public string GetL()
        {
            return Number.BinToHex(GetBits(7, 8));
        }

        // Записываем полное значение регистра
        public string GetAll()
        {
            return GetH() + GetL();
        }*/

        public bool Get(int i)
        {
            return Bits[i];
        }

        // Сдвиг влево на 1 бит 
        public void Shl()
        {
            for (int i = 0; i < 15; i++) Bits[i] = Bits[i + 1];
            Bits[15] = false;
        }
        // Сдвиг вправо на 1 бит
        public void Shr()
        {
            for (int i = 15; i > 0; i--) Bits[i] = Bits[i - 1];
            Bits[0] = false;
        }

        // Значения битов (разрядов)
        public void Set(int a, bool b)
        {
            Bits[a] = b;
        }

        public void Set(string a)
        {
            string s = "", b;
            for (int i = 0; i < a.Length; i++)
            {
                b = a.Substring(i, 1);
               // s += Number.HexToBin(b);
            }
            Console.WriteLine("---");
            Console.WriteLine(s);
            for (int i = 0; i < 16; i++)
            {
                if (i >= s.Length)
                {
                    Bits[i] = false;
                    continue;
                }
                b = s.Substring(i, 1);
                Bits[15 - i] = b == "1";
            }
        }

        public void Set(bool[] b)
        {
            if (b.Length != 16) return;
            this.Bits = b;
        }
        
        // Значения флагов 
        public bool GetFlag(Flags f)
        {
            return Bits[(int)f];
        }

        public void SetFlag(Flags f, bool state)
        {
           Bits[(int) f] = state;
        }
    }
}