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
        public bool PrerFlag = false;

        private bool[] HighMul = BinaryNumber.Zero;
        private bool[] HighDiv = BinaryNumber.Zero;
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
                double d = 0;
                for (int i = 0; i < 16; i++) if (this.number[i]) d += Math.Pow(2, 16 - i - 1);
                return Convert.ToInt32(d);
            }
        }

        // Установка или получение двоичного числа
        public string Binary
        {
            get
            {
                return BinaryNumber.GetBinaryString(this.number);
            }
            set
            {
                this.number = BinaryNumber.Zero;
                int l = 15;
                for (int i = value.Length - 1; i >= 0 && l >= 0; i--)
                {
                    this.number[l] = value.Substring(i, 1) == "1";
                    l--;
                }
            }
        }

        // Высокая часть для умножения
        public bool[] HighPartMul
        {
            get
            {
                return this.HighMul;
            }
        }

        // Высокая часть для деления
        public bool[] HighPartDiv
        {
            set
            {
                this.HighDiv = value;
            }
        }

        public bool[] Number
        {
            get
            {
                return this.number;
            }
        }        

        // Очищает заданный массив
        public static void MakeZero(bool[] pointer)
        {
            for (int i = 0; i < pointer.Length; i++) pointer[i] = false;
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

        // Оператор вычитания
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

        // Оператор вычитания
        public static BinaryNumber operator -(BinaryNumber a, BinaryNumber b)
        {
            return a -= b.Decimal;
        }

        // Оператор умножения
        public static BinaryNumber operator *(BinaryNumber a, int b)
        {
            int result = a.Decimal * b;
            bool overflow = result > Math.Pow(2, 16) - 1;
            a.OverflowFlag = a.CarryFlag = overflow;

            bool[] res = BinaryNumber.GetBinary32(result);
            BinaryNumber.MakeZero(a.number);
            int l = 15;
            for (int i = res.Length - 1; i >= 0 && l >= 0; i--)
            {
                a.number[l] = res[i];
                l--;
            }
            // При переполении, переход на High
            if (overflow) 
            {
                a.HighMul = BinaryNumber.Zero;
                l = 15;
                for (int i = res.Length - 17; i >= 0 && l >= 0; i--)
                {
                    a.HighMul[l] = res[i];
                    l--;
                }
            }
            return a;
        }

        // Оператор умножения
        public static BinaryNumber operator *(BinaryNumber a, BinaryNumber b)
        {
            return a * b.Decimal; 
        }

        // Оператор деления
        public static BinaryNumber operator /(BinaryNumber a, int b)
        {
            int dec = BinaryNumber.GetDecimal(a.HighDiv, a.number) / b;
            a.HighDiv = BinaryNumber.Zero;
            // флаг прерывания
            if (b == 0)
            {
                a.PrerFlag = true;
                return a;
            }
            a.number = BinaryNumber.GetBinary(dec);
            return a;
        }

        // Оператор деления
        public static BinaryNumber operator /(BinaryNumber a, BinaryNumber b)
        {
            return a / b.Decimal;
        }
        
        // Получает двоичное представление из массива
        public static string GetBinaryString(bool[] bin)
        {
            string s = "";
            for (int i = 0; i < 16; i++) s += bin[i] ? "1" : "0";
            return s;
        }

        // Получает двоичное представление из массива (8 бит)______________________________________?
        public static string GetBinaryString8(bool[] bin)
        {
            string s = "";
            for (int i = 0; i < 8; i++) s += bin[i] ? "1" : "0";
            return s;
        }

        // Преобразует десятичное в 32-битное двоичное
        public static bool[] GetBinary32(int dec)
        {
            List<bool> bin = new List<bool>();
            while (dec > 0)
            {
                bin.Insert(bin.Count, dec % 2 == 1);
                dec /= 2;
            }
            bin.Reverse();
            return bin.ToArray();
        }

        // Преобразует десятичное число в двоичное
        public static bool[] GetBinary(int dec)
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
        public static int GetDecimal(string n, int b)
        {
            const string table = "ABCDEF";
            int m;
            long d = 0;
            for (int i = 0; i < n.Length; i++)
            {
                if(!int.TryParse(n.Substring(i, 1), out m))
                {
                    m = 10 + table.IndexOf(n.Substring(i, 1).ToUpper());
                }
                d += m * Convert.ToInt64(Math.Pow(b, n.Length - i - 1));
            }
            return (int) d;
        }

        // Перевод числа в десятичное
        public static int GetDecimal(params bool[][] bin)
        {
            string a = "";
            for (int i = 0; i < bin.Length; i++)
            {
                for (int j = 0; j < bin[i].Length; j++)
                {
                    a += bin[i][j] ? "1" : "0";
                }
            }
            return BinaryNumber.GetDecimal(a, 2);
        }
    }
}
