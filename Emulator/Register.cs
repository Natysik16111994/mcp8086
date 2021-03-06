﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    // Класс Регистр
    public class Register
    {
        public BinaryNumber Value;

        // Тип регистра
        public enum Types
        {
            None,
            Low,
            High
        };

        // Флаги
        public enum Flags
        {
            CF = 15,
            PF = 13,
            AF = 11,
            ZF = 9,
            SF = 8,
            TF = 7,
            IF = 6,
            DF = 5,  
            OF = 4   
        };

        // Конструктор Регистр. Обнуляем все 16 бит
        public Register()
        {
            Value = new BinaryNumber(0);
        }


        // Десятичное значение
        public int Decimal
        {
            set
            {
                this.Value.Decimal = value;
            }
            get
            {
                return this.Value.Decimal;
            }
        }

        // Десятичный верхний
        public int HighDecimal
        {
            set
            {
                bool[] b = BinaryNumber.GetBinary(value);
                for (int i = 0; i < 8; i++) this.Value.Number[i] = b[8 + i];
            }
            get 
            {   
                double d = 0;
                for (int i = 0; i < 8; i++) d += (this.Value.Number[i] ? 1 : 0) * Math.Pow(2, 7 - i);
                return Convert.ToInt32(d);
            }
        }

        // Десятичный нижний
        public int LowDecimal
        {
            set
            {
                bool[] b = BinaryNumber.GetBinary(value);
                for (int i = 0; i < 8; i++) this.Value.Number[8 + i] = b[8 + i];
            }
            get 
            {
                double d = 0;
                for (int i = 8; i < 16; i++) d += (this.Value.Number[i] ? 1 : 0) * Math.Pow(2, 15 - i);
                return Convert.ToInt32(d);
            }
        }

        public int SignDecimal
        {
            get
            {
                double d = 0;
                for (int i = 1; i < 16; i++) d += (this.Value.Number[i] ? 1 : 0) * Math.Pow(2, 15 - i);
                if (this.Value.Number[0]) d = -d;
                return Convert.ToInt32(d);
            }
        }

        public string HighHex
        {
            get
            {
                return BinaryNumber.GetHex(HighDecimal, 2);
            }
        }

        public string LowHex
        {
            get
            {
                return BinaryNumber.GetHex(LowDecimal, 2);
            }
        }

        public string Hex
        {
            get
            {
                return BinaryNumber.GetHex(Decimal, 4);
            }
        }

        // Обнуляем регистр
        public void SetZero()
        {
            Value.Decimal = 0;
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

            // PF
            int pf_count = 0;
            for (int i = 8; i < 16; i++)
                if (Value.Number[i]) pf_count++;
            flags.SetFlag(pf_count % 2 == 0, Flags.PF);


            //flags.SetFlag(Flags.AF, ...);
        }
    }
}