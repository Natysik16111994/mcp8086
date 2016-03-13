using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class Number
    {   
        struct NonDec
        {
            public bool Changed;
            public string Value;
        }

        private int valueDec;
        private NonDec hex, oct, bin;

        // 
        public Number(int dec)
        {
            hex.Changed = oct.Changed = bin.Changed = true;
            valueDec = dec;
        }

        private string ToBase(int bs)
        {
            const string table = "ABCDEF";
            int a; string b = "";
            int d = valueDec;
            while (d >= bs)
            {
                a = d % bs;
                if (a < 10) b += a.ToString();
                else b += table.Substring(a - 10, 1);
                d = d / bs;
            }
            if (d < 10) b += d.ToString();
            else b += table.Substring(d - 10, 1);
            string c = "";
            for (int i = 0; i < b.Length; i++) c += b.Substring(b.Length - i - 1, 1);
            return c;  
        }

        public int Dec
        {
            get
            {
                return valueDec;
            }
        }

        public string Hex
        {
            get
            {
                if (!hex.Changed) return hex.Value;
                hex.Value = ToBase();
                hex.Changed = false;
                return hex.Value;
            }
        }

        public string Oct
        {
            get
            {
                if (!oct.Changed) return oct.Value;
                int d = valueDec;
                int a;
                string b = "";
                while (d >= 8)
                {
                    a = d % 8;
                    b += a.ToString();
                    d = d / 8;
                }
                b += d.ToString();
                oct.Value = "";
                for (int i = 0; i < b.Length; i++) oct.Value += b.Substring(b.Length - i - 1, 1);
                oct.Changed = false;
                return oct.Value;
            }
        }

        public string Bin
        {
            get
            {
                if (!bin.Changed) return bin.Value;
                int d = valueDec;
                int a;
                string b = "";
                while (d >= 2)
                {
                    a = d % 2;
                    b += a.ToString();
                    d = d / 2;
                }
                b += d.ToString();
                bin.Value = "";
                for (int i = 0; i < b.Length; i++) bin.Value += b.Substring(b.Length - i - 1, 1);
                bin.Changed = false;
                return bin.Value;
            }
        }

        private void ConvertValue()
        {
            int d = valueDec;
        }

        // Таблица, перевод шестнадцатиричной системы в двоичную
        private static string hex2bin_table(string hex)
        {
            string s = hex.ToUpper();
            if (s == "1") return "0001";
            else if (s == "2") return "0010";
            else if (s == "3") return "0011";
            else if (s == "4") return "0100";
            else if (s == "5") return "0101";
            else if (s == "6") return "0110";
            else if (s == "7") return "0111";
            else if (s == "8") return "1000";
            else if (s == "9") return "1001";
            else if (s == "A") return "1010";
            else if (s == "B") return "1011";
            else if (s == "C") return "1100";
            else if (s == "D") return "1101";
            else if (s == "E") return "1110";
            else if (s == "F") return "1111";
            return "0000";
        }

       // Таблица, перевод двоичной системы в шестнадцатиричную
        private static string bin2hex_table(string bin)
        {
            string s = bin.ToLower();
            if (s == "0001") return "1";
            else if (s == "0010") return "2";
            else if (s == "0011") return "3";
            else if (s == "0100") return "4";
            else if (s == "0101") return "5";
            else if (s == "0110") return "6";
            else if (s == "0111") return "7";
            else if (s == "1000") return "8";
            else if (s == "1001") return "9";
            else if (s == "1010") return "A";
            else if (s == "1011") return "B";
            else if (s == "1100") return "C";
            else if (s == "1101") return "D";
            else if (s == "1110") return "E";
            else if (s == "1111") return "F";
            return "0";
        }
    }
}
