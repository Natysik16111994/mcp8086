using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Emulator
{
    enum aiOperandType
    {
        Register,
        Value,
        Null
    };

    struct AsmInstruction
    {
        public string opcode, operand1, operand2;
        public int line;
        public aiOperandType type1, type2;
        public Register.Types regtype1, regtype2;
    }

    class Assembler
    {
        private Processor processor;

        // программа
        private List<AsmInstruction> instructions = new List<AsmInstruction>();
        // список меток
        private Dictionary<string, int> labelIndices = new Dictionary<string, int>();
        private int currentInstructionIndex = 0;

        // Конструктор
        public Assembler(Processor proc)
        {
            this.processor = proc;
        }

        // Сброс текущей информации о запуске
        public void ResetRuntime()
        {
            currentInstructionIndex = 0;
        }

        // Загружаем код из файла
        public bool LoadAsmFromFile(string filename)
        {
            if (!File.Exists(filename)) return false;
            return LoadAsm(File.ReadAllLines(filename));
        }

        // Загрузка кода
        public bool LoadAsm(string[] content)
        {
            // Очищаем старые значения
            instructions.Clear(); labelIndices.Clear();
            string a, b, c;
            for (int i = 0; i < content.Length; i++)
            {
                a = content[i].Trim().ToLower();
                if (a.Length == 0) continue;
                b = "";
                while (a.Length > 0)
                {
                    c = a.Substring(0, 1);
                    a = a.Substring(1);
                    if (c == ";") break; // Комментарий
                    else if (c == ":") // Метка
                    {
                        // Добавим следующую инструкцию в словарь
                        labelIndices.Add(b, instructions.Count);
                        b = "";
                        continue;
                    }
                    else
                    {
                        b += c;
                    }
                }
                b = b.Trim();
                if (b.Length == 0) continue;
                AsmInstruction instruction = new AsmInstruction();
                if (ParseString(b, out instruction))
                {
                    instruction.line = i;
                    instructions.Add(instruction);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // Парсинг строки
        private bool ParseString(string str, out AsmInstruction instruct)
        {
            instruct.opcode = instruct.operand1 = instruct.operand2 = "";
            instruct.type1 = instruct.type2 = aiOperandType.Value;
            instruct.regtype1 = instruct.regtype2 = Register.Types.None;
            instruct.line = 0;

            int len = str.IndexOf(" ");
            if (len < 0) len = str.Length;
            string opcode = str.Substring(0, len);
            str = str.Substring(len).Trim();
           
            // Операнды
            string[] operands = { "", "" };
            int operand = 0;
            bool quotes = false;
            while (str.Length > 0)
            {
                string a = str.Substring(0, 1);
                str = str.Substring(1).Trim();
                if (a == "[")
                {
                    if (!quotes) quotes = true;
                    else return false;
                }
                else if (a == "]")
                {
                    if (quotes) quotes = false;
                    else return false;
                }
                else if (a == "," && !quotes)
                {
                    operand++;
                    if (operand > 1) // TODO: сообщение об ошибке
                    {
                        return false;
                    }
                    continue;
                }
                operands[operand] += a;
            }

            instruct.opcode = opcode;
            instruct.operand1 = operands[0]; instruct.operand2 = operands[1];
            instruct.type1 = GetOperandType(operands[0]);
            instruct.type2 = GetOperandType(operands[1]);
            instruct.regtype1 = GetRegisterType(operands[0]);
            instruct.regtype2 = GetRegisterType(operands[1]);
            return true;
        }
        
        // Возвращает тип операнда
        public aiOperandType GetOperandType(string operand)
        {
            operand = operand.Trim();
            if (operand.Length == 0) return aiOperandType.Null;
            if(processor.GetRegisterByName(operand) != null) return aiOperandType.Register;
            return aiOperandType.Value;
        }

        // Возвращает тип регистра
        public Register.Types GetRegisterType(string operand)
        {
            if (operand.Length > 0)
            {
                if (operand.Substring(1, 1) == "l") return Register.Types.Low;
                else if (operand.Substring(1, 1) == "h") return Register.Types.High;
            }
            return Register.Types.None;
        }

        // Выполняет текущую инструкцию
        public bool ExecuteInstruction()
        {
            AsmInstruction inst = instructions[currentInstructionIndex];
            currentInstructionIndex++;

            object value_a = null, value_b = null;

            if(!GetValueOrRegister(inst.operand1, inst.type1, out value_a) ||
                !GetValueOrRegister(inst.operand2, inst.type2, out value_b))
            {
                return false;
            }
            
            // TODO: проверка опкода


            switch (inst.opcode)
            {
                case "add":
                    processor.Add((Register)value_a, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "adc":
                    processor.Adc((Register)value_a, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "mov":
                    processor.Mov((Register)value_a, value_b, inst.regtype1, inst.regtype2);
                    break;
            }
            return true;
        }

        public bool GetValueOrRegister(string data, aiOperandType type, out object obj)
        {
            obj = null;
            if (type == aiOperandType.Register)
            {
                obj = processor.GetRegisterByName(data.Trim());
                return true;
            }
            else if (type == aiOperandType.Value)
            {
                int val;
                if (!int.TryParse(data, out val))
                {
                    // строка
                    if (data.Substring(0, 1) == "'" && data.Substring(data.Length - 1, 1) == "'")
                    {
                        //
                    }
                    else
                    {
                        string letter = data.Substring(data.Length - 1, 1);
                        int nbase = 0;
                        if (letter == "h") nbase = 16;
                        if (letter == "o") nbase = 8;
                        else if (letter == "b") nbase = 2;
                        if (nbase != 0)
                        {
                            data = data.Substring(0, data.Length - 1);
                            obj = BinaryNumber.GetDecimal(data, nbase);
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    obj = val;
                    return true;
                }
            }
            else if (type == aiOperandType.Null) return true;
            return false;
        }

        /*
        public bool GetValueOrRegister(string data, aiOperandType type, ref Register reg, out int val)
        {
            reg = null;
            val = 0;
            if (type == aiOperandType.Register)
            {
                reg = processor.GetRegisterByName(data.Trim());
                return true;
            }
            else if (type == aiOperandType.Value)
            {
                if (!int.TryParse(data, out val))
                {
                    // строка
                    if (data.Substring(0, 1) == "'" && data.Substring(data.Length - 1, 1) == "'")
                    {
                        //
                    }
                    else
                    {
                        string letter = data.Substring(data.Length - 1, 1);
                        int nbase = 0;
                        if (letter == "h") nbase = 16;
                        if (letter == "o") nbase = 8;
                        else if (letter == "b") nbase = 2;
                        if (nbase != 0)
                        {
                            data = data.Substring(0, data.Length - 1);
                            val = BinaryNumber.GetDecimal(data, nbase);
                            return true;
                        }
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else if (type == aiOperandType.Null) return true;
            return false;
        }
        */
    }
}
