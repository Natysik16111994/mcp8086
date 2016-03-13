using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class BinaryNumber
    {
        public const int MaxUnsignedDecimal = 65535;
        public bool OverflowFlag = false;
        public bool CarryFlag = false;

        private bool[] number = BinaryNumber.Zero;

        // Конструктор
        public BinaryNumber(int dec)
        {
            this.Decimal = dec;
        }

        // Конструктор
        public BinaryNumber(string n, int b)
        {
            this.Decimal = GetDecimal(n, b);
        }

        // Установка или получение десятичного числа
        public int Decimal
        {
            set
            {
                this.number = BinaryNumber.GetBinary(value);
            }
            get
            {
                int d = 0;
                for (int i = 0; i < 16; i++) if (this.number[i]) d += Convert.ToInt32(Math.Pow(2, 16 - i - 1));
                return d;
            }
        }

        // Установка или получение двоичного числа
        public string Binary
        {
            get
            {
                return BinaryNumber.GetBinaryString(this.number);
            }
        }

        // Возвращает нуль
        public static bool[] Zero
        {
            get
            {
                bool[] zero = new bool[16];
                for (int i = 0; i < 16; i++) zero[i] = false;
                return zero;
            }
        }

        // Перегрузка оператора сложения (int)
        public static BinaryNumber operator+(BinaryNumber a, int b)
        {
            bool[] an = a.number;
            bool[] bn = BinaryNumber.GetBinary(b);
            bool[] result = BinaryNumber.Zero;
            
            // флаги переноса и переполнения
            bool outOfRange = false;
            bool signBitCarry = false;
            a.OverflowFlag = a.CarryFlag = false;

            // начинаем сложение
            int hasBit = 0;
            for (int i = 15; i >= 0; i--)
            {
                if (i == 0 && hasBit > 0) signBitCarry = true;
                if (!an[i] && !bn[i])
                {
                    if (hasBit > 0)
                    {
                        result[i] = true;
                        hasBit--;
                    }
                }
                else if (an[i] && bn[i])
                {
                    result[i] = (hasBit > 0);
                    hasBit += (hasBit > 0) ? 0 : 1;
                }
                else
                {
                    result[i] = (hasBit == 0);
                }
            }
            if (hasBit > 0) outOfRange = true;
            if (outOfRange && signBitCarry) a.CarryFlag = true;
            else if (outOfRange && !signBitCarry) a.OverflowFlag = a.CarryFlag = true;
            else if (!outOfRange && signBitCarry) a.OverflowFlag = true;
            a.number = result;
            return a;
        }

        // Оператор сложения (BinaryNumber)
        public static BinaryNumber operator +(BinaryNumber a, BinaryNumber b)
        {
            a += b.Decimal;
            return a;
        }

        public static BinaryNumber operator -(BinaryNumber a, int b)
        {
            bool[] an = a.number;
            bool[] bn = BinaryNumber.GetBinary(b);
            bool[] result = BinaryNumber.Zero;

            // флаги переполнения и переноса
            a.OverflowFlag = a.CarryFlag = false;
            bool getToSignBit = false;
            bool getFromSignBit = false;

            for (int i = 15; i >= 0; i--)
            {
                if(!(an[i] ^ bn[i]))
                {
                    result[i] = false;
                }
                else if (!an[i] && bn[i])
                {
                    for (int j = i - 1; j >= -1; j--)
                    {
                        if (j == -1)
                        {
                            getToSignBit = true;
                            for (int k = 0; k < i; k++) an[k] = true;
                            break;
                        }
                        else
                        {
                            if (an[j])
                            {
                                if (j == 0) getFromSignBit = true;
                                an[j] = false;
                                for (int k = j + 1; k < i; k++) an[k] = true;
                                break;
                            }
                        }
                    }
                    result[i] = false;
                }
                else if (an[i] & !bn[i])
                {
                    result[i] = true;
                }
            }
            if (getToSignBit && getFromSignBit) a.CarryFlag = true;
            else if (getToSignBit && !getFromSignBit) a.CarryFlag = a.OverflowFlag = true;
            else if (!getToSignBit && getFromSignBit) a.OverflowFlag = true;
            a.number = result;
            return a;
        }

        public static BinaryNumber operator -(BinaryNumber a, BinaryNumber b)
        {
            return a -= b.Decimal;
        }

        private static string GetBinaryString(bool[] bin)
        {
            string s = "";
            for (int i = 0; i < 16; i++) s += bin[i] ? "1" : "0";
            return s;
        }

        // Преобразует десятичное число в двоичное
        private static bool[] GetBinary(int dec)
        {
            bool[] bin = BinaryNumber.Zero;
            int n = dec > BinaryNumber.MaxUnsignedDecimal ? 0 : dec;
            int index = 15;
            while (n > 0)
            {
                bin[index] = n % 2 == 1;
                n /= 2;
                index--;
            }
            return bin;
        }

        // Перевод числа в десятичное
        private static int GetDecimal(string n, int b)
        {
            const string table = "ABCDEF";
            int m, d = 0;
            for (int i = 0; i < n.Length; i++)
            {
                if(!int.TryParse(n.Substring(i, 1), out m))
                {
                    m = 10 + table.IndexOf(n.Substring(i, 1).ToUpper());
                }
                d += m * Convert.ToInt32(Math.Pow(b, n.Length - i - 1));
            }
            return d;
        }
    }
}
