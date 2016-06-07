using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    /// <summary>
    /// Представляет собой класс базовой памяти.
    /// Используется для хранения массивов.
    /// </summary>
    public class BasicMemory
    {
        private Dictionary<string, int[]> mem;
        private Dictionary<string, int> address;
        private int lastAddr = 0;

        public BasicMemory()
        {
            mem = new Dictionary<string, int[]>();
            address = new Dictionary<string, int>();
        }

        /// <summary>
        /// Очищает память
        /// </summary>
        public void Clear()
        {
            mem.Clear();
            address.Clear();
        }

        public void Add(string id, int[] array)
        {
            if (mem.ContainsKey(id)) mem[id] = array;
            else
            {
                mem.Add(id, array);
                address.Add(id, lastAddr);
                lastAddr += array.Length * 2;
            }
        }

        public int GetAddr(string id)
        {
            if (address.ContainsKey(id)) return address[id];
            return 0;
        }

        public int[] Get(string id)
        {
            if (mem.ContainsKey(id)) return mem[id];
            return new int[] { 0 };
        }

        public int[] Get(int addr)
        {
            foreach (var key in address.Keys)
            {
                if (addr == address[key]) return mem[key];
            }
            return new int[] { 0 };
        }
    }
}
