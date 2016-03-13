using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class Number
    {   
        private int valueDec;
        private Dictionary<string, string> valueList;

        // 
        public Number(int dec)
        {
            valueList = new Dictionary<string, string>();
            valueDec = dec;
        }

        public static int ConvertToDec(string s, int b)
        {
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
        }

        public string Oct
        {
            get
            {
                if (!valueList.ContainsKey("oct")) valueList.Add("oct", ToBase(8));
                return valueList["oct"];
            }
        }

        public string Bin
        {
            get
            {
                if (!valueList.ContainsKey("bin")) valueList.Add("bin", ToBase(2));
                return valueList["bin"]; 
            }
        }

        public void Add(string s, int b)
        {
            valueDec += Number.ConvertToDec(s, b);
            valueList.Clear();
        }

        public void Sub(string s, int b)
        {
            valueDec -= Number.ConvertToDec(s, b);
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
    }
}
