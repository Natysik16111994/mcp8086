using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    // Класс Регистр
    class Register
    {
        public Number Value;

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

        // Конструктор Регистр. Обнуляем все 16 бит
        public Register()
        {
            Value = new Number(0);
        }
        
        // Разделяем 16 битный регистр на два других по 8 бит 
        // Записываем старший регистр (high)
        public string GetHigh()
        {
            return Value.Bin.Substring(0, 8);
        }

        public string GetLow()
        {
            return Value.Bin.Substring(8, 8);
        }

        public string GetAll()
        {
            return Value.Bin;
        }

        // Сдвиг влево на 1 бит 
        public void Shl()
        {
            Value.Dec /= 2;
        }

        // Сдвиг вправо на 1 бит
        public void Shr()
        {
            Value.Dec *= 2;
        }


        public bool Get(int a)
        {
            return Value.Bin.Substring(a, 1) == "1";
        }

        // Значения битов (разрядов)
        public void Set(int a, bool b)
        {
            string s = Value.Bin;
            s = s.Substring(0, a)+ (b ? "1" : "0") + s.Substring(a + 1);
            Value.Bin = s;
        }
        
        // Значения флагов 
        public bool GetFlag(Flags f)
        {
            return Get((int)f);
        }

        public void SetFlag(Flags f, bool state)
        {
            Set((int)f, state);
        }

        // Выставляет флаги
        public void UpdateFlags(Register flags)
        {
            /*CF = 0,
            OF = 1,
            ZF = 2,
            SF = 3,
            PF = 4,
            AF = 5,
            DF = 6,
            IF = 7,
            TF = 8*/

            flags.SetFlag(Flags.CF, Value.IsCarryFlag());
            flags.SetFlag(Flags.OF, Value.Dec > 32767);
            flags.SetFlag(Flags.ZF, Value.Dec == 0);
            flags.SetFlag(Flags.SF, Value.Bin.Substring(0, 1) == "1");
            flags.SetFlag(Flags.PF, Value.Dec % 2 == 0);
            //flags.SetFlag(Flags.AF, ...);
        }
    }
}