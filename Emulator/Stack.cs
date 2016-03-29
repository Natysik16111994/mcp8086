using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class Stack
    {
        public List<BinaryNumber> list;

        public Stack()
        {
            list = new List<BinaryNumber>();
        }

        public void Push(string n, int b)
        {
            bool[] c = BinaryNumber.GetBinary(BinaryNumber.GetDecimal(n, b));
            for (int i = 0; i < 16; i++)
                if (i < 8) 
                
        }
    }
}
