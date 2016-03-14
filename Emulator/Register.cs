using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    // Класс Регистр
    class Register
    {
        public BinaryNumber Value;

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
            Value = new BinaryNumber(0);
        }
        
        // Разделяем 16 битный регистр на два других по 8 бит 
        // Записываем старший регистр (high)
        public string GetHigh()
        {
            return Value.Binary.Substring(0, 8);
        }

        public string GetLow()
        {
            return Value.Binary.Substring(8, 8);
        }

        public string GetAll()
        {
            return Value.Binary;
        }

        // Сдвиг влево на 1 бит 
        public void Shl()
        {
            Value /= 2;
        }

        // Сдвиг вправо на 1 бит
        public void Shr()
        {
            Value *= 2;
        }

        public bool Get(int a)
        {
            return Value.Number[a];
        }

        // Значения битов (разрядов)
        public void Set(int a, bool b)
        {
            Value.Number[a] = b;
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

        public void SetFlag(bool state, params Flags[] f)
        {
            for (int i = 0; i < f.Length; i++) Set((int)f[i], state);
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

            flags.SetFlag(Flags.CF, Value.CarryFlag);
            flags.SetFlag(Flags.OF, Value.OverflowFlag);
            flags.SetFlag(Flags.ZF, Value.Decimal == 0);
            flags.SetFlag(Flags.SF, Value.Number[0]);
            flags.SetFlag(Flags.PF, Value.Decimal % 2 == 0);
            //flags.SetFlag(Flags.AF, ...);
        }
    }
}