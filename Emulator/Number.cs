using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class Number
    {   
        private int valueDec;
        private Dictionary<string, string> valueList;

        private const int HexLength = 4;
        private const int OctLength = 6;
        private const int BinLength = 16;

        private bool flagOverflow = false;
        private bool flagCarry = false;

        // 
        public Number(int dec)
        {
            valueList = new Dictionary<string, string>();
            valueDec = dec;
        }

        public static int ConvertToDec(string s, int b)
        {
            if (b == 10)
            {
                int na = 0;
                int.TryParse(s, out na);
                return na;
            }

            const string table = "ABCDEF";
            int d = 0, c = 0; string a = "";

            bool sign = true;
            if(s.Substring(0, 1) == "-")
            {
                sign = false;
                s = s.Substring(1);
            }

            for (int i = 0; i < s.Length; i++)
            {
                a = s.Substring(i, 1).ToUpper();
                if(!int.TryParse(a, out c))
                {
                    c = 10 + table.IndexOf(a);
                }
                if(c >= b) return 0;
                d += c * Convert.ToInt32(Math.Pow(b, s.Length - i - 1));
            }
            if(!sign) d *= -1;
            return d;
        }

        public void Dispose()
        {
            valueDec = 0;
            valueList.Clear();
            valueList = null;
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

            // Дополняем нулями
            int f = 0;
            if (bs == 16) f = HexLength;
            else if (bs == 8) f = OctLength;
            else if (bs == 2) f = BinLength;
            while (c.Length < f) c = "0" + c;
            while (c.Length > f) c = c.Substring(1);
            return c;  
        }

        public int Dec
        {
            get
            {
                return valueDec;
            }
            set
            {
                valueDec = value;
                valueList.Clear();
            }
        }

        public string Hex
        {
            get
            {
                if (!valueList.ContainsKey("hex")) valueList.Add("hex", ToBase(16));
                return valueList["hex"];
            }
            set
            {
                this.Dec = Number.ConvertToDec(value, 16);
            }
        }

        public string Oct
        {
            get
            {
                if (!valueList.ContainsKey("oct")) valueList.Add("oct", ToBase(8));
                return valueList["oct"];
            }
            set
            {
                this.Dec = Number.ConvertToDec(value, 8);
            }
        }

        public string Bin
        {
            get
            {
                if (!valueList.ContainsKey("bin")) valueList.Add("bin", ToBase(2));
                return valueList["bin"]; 
            }
            set
            {
                this.Dec = Number.ConvertToDec(value, 2);
            }
        }

        public bool Sign()
        {
            return this.Dec < 0;
        }

        public void Add(string s, int b)
        {
            Add(Number.ConvertToDec(s, b));
        }

        public void Add(int n)
        {

            valueDec += n;
            valueList.Clear();

            this.flagCarry = valueDec > 65535;
            if(this.flagCarry && this.Bin.Substring(0, 1) != "0") valueDec = 0;
        }

        public void Sub(string s, int b)
        {
            int num = Number.ConvertToDec(s, b);
            if (valueDec < num) this.flagCarry = true;
            valueDec -= num;
            valueList.Clear();
        }

        public void Mul(string s, int b)
        {
            valueDec *= Number.ConvertToDec(s, b);
            valueList.Clear();
        }

        public void Div(string s, int b)
        {
            valueDec /= Number.ConvertToDec(s, b);
            valueList.Clear();
        }

        public bool IsCarryFlag()
        {
            return flagCarry;
        }

        public static Number operator +(Number a, Number b)
        {
            a.Add(b.Dec);
            return a;
        }
    }
}
