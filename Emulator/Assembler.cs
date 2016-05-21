using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Emulator
{
    public enum aiOperandType
    {
        Register,
        Value,
        Null
    };

    public struct AsmInstruction
    {
        public string opcode, operand1, operand2;
        public int line;
        public aiOperandType type1, type2;
        public Register.Types regtype1, regtype2;
        public string raw;
    }

    public class Assembler
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

        /// <summary>
        /// Получает или задает, остановлена программа или нет
        /// </summary>
        public bool ProgramEnd
        {
            get
            {
                return currentInstructionIndex >= instructions.Count;
            }
            set
            {
                currentInstructionIndex = instructions.Count;
            }
        }

        /// <summary>
        /// Получает, готова программа или нет
        /// </summary>
        public bool ProgramReady
        {
            get
            {
                return currentInstructionIndex == 0 && instructions.Count > 0;
            }
        }

        /// <summary>
        /// Указывает, выполняется программа или нет
        /// </summary>
        public bool ProgramExecuting
        {
            get
            {
                return !ProgramEnd && currentInstructionIndex >= 0 && instructions.Count > 0;
            }
        }

        /// <summary>
        /// Получает номер строки, выполняющейся в данный момент
        /// </summary>
        /// <returns>Номер строки</returns>
        public int GetCurrentLine()
        {
            if (ProgramExecuting) return instructions[currentInstructionIndex].line;
            return -1;
        }

        public List<AsmInstruction> Instructions
        {
            get
            {
                return this.instructions;
            }
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
            currentInstructionIndex = 0;
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
            instruct.raw = str;
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
            if(instruct.type1 == aiOperandType.Register) instruct.regtype1 = GetRegisterType(operands[0]);
            if(instruct.type2 == aiOperandType.Register) instruct.regtype2 = GetRegisterType(operands[1]);
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
            if (operand.Length == 2)
            {
                if (operand.Substring(1, 1) == "l") return Register.Types.Low;
                else if (operand.Substring(1, 1) == "h") return Register.Types.High;
            }
            return Register.Types.None;
        }

        // Выполняет текущую инструкцию
        public bool ExecuteInstruction()
        {
            if (currentInstructionIndex >= instructions.Count) return false;
            AsmInstruction inst = instructions[currentInstructionIndex];
            currentInstructionIndex++;

            object value_a = null, value_b = null;
            if(!GetValueOrRegister(inst.operand1, inst.type1, out value_a) ||
                !GetValueOrRegister(inst.operand2, inst.type2, out value_b))
            {
                return false;
            }

            // Увеличение флага IP
            processor.IP.Decimal += 1;

            Register ra = null;
            if (value_a is Register) ra = (Register)value_a;
            
            // TODO: проверка опкода

            switch (inst.opcode)
            {
                case "add":
                    processor.Add(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "adc":
                    processor.Adc(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "and":
                    processor.And(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "bsf":
                    processor.Bsf(ra, value_b);
                    break;

                case "bsr":
                    processor.Bsr(ra, value_b);
                    break;

                case "bt":
                    processor.Bt(ra, (int)value_b);
                    break;

                case "btc":
                    processor.Btc(ra, (int)value_b);
                    break;

                case "btr":
                    processor.Btr(ra, (int)value_b);
                    break;

                case "bts":
                    processor.Bts(ra, (int)value_b);
                    break;

                // TODO: call
                case "call":
                    break;

                case "cbw":
                    processor.Cbw(ra);
                    break;

                case "clc":
                    processor.Clc();
                    break;

                case "cld":
                    processor.Cld();
                    break;

                case "cli":
                    processor.Cli();
                    break;

                case "cmc":
                    processor.Cmc();
                    break;

                case "cmp":
                    processor.Cmp(ra, value_a, inst.regtype1, inst.regtype2);
                    break;

                case "cwd":
                    processor.Cwd();
                    break;

                case "dec":
                    processor.Dec(ra, inst.regtype1);
                    break;

                case "div":
                    processor.Div(value_a, inst.regtype1);
                    break;

                case "idiv":
                    processor.Idiv(value_a, inst.regtype1);
                    break;

                case "enter":
                    processor.Enter();
                    break;

                case "imul":
                    processor.Imul(value_a, inst.regtype1);
                    break;

                case "inc":
                    processor.Inc(ra, inst.regtype1);
                    break;

                case "int":
                    processor.Int(value_a);
                    break;

                case "je": case "jz":
                    processor.JZ(value_a);
                    break;

                case "jnz": case "jne":
                    processor.JNZ(value_a);
                    break;

                case "jc": case "jnae": case "jb":
                    processor.JC(value_a);
                    break;

                case "jnc": case "jae": case "jnb":
                    processor.JNC(value_a);
                    break;

                case "jp":
                    processor.JP(value_a);
                    break;

                case "jnp":
                    processor.JNP(value_a);
                    break;

                case "js":
                    processor.JS(value_a);
                    break;

                case "jns":
                    processor.JNS(value_a);
                    break;

                case "jo":
                    processor.JO(value_a);
                    break;

                case "jno":
                    processor.JNO(value_a);
                    break;

                case "ja": case "jnbe":
                    processor.JA(value_a);
                    break;

                case "jna": case "jbe":
                    processor.JNA(value_a);
                    break;

                case "jg": case "jnle":
                    processor.JG(value_a);
                    break;

                case "jge": case "jnl":
                    processor.JGE(value_a);
                    break;

                case "jl": case "jnge":
                    processor.JL(value_a);
                    break;

                case "jle": case "jng":
                    processor.JLE(value_a);
                    break;

                case "jcxz":
                    processor.JCXZ(value_a);
                    break;

                case "jmp":
                    processor.Jmp(value_a);
                    break;

                case "lahf":
                    processor.Lahf();
                    break;

                case "leave":
                    break;

                case "loop":
                    processor.Loop(value_a);
                    break;

                case "loopnz": case "loopne":
                    processor.Loopnz(value_a);
                    break;

                case "loopz": case "loppe":
                    processor.Loopz(value_a);
                    break;

                case "mov":
                    processor.Mov(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "movsb":
                    processor.Movsb();
                    break;

                case "mul":
                    processor.Mul(value_a, inst.regtype1);
                    break;

                case "neg":
                    processor.Neg(ra, inst.regtype1);
                    break;

                case "nop":
                    break;

                case "not":
                    processor.Not(ra, inst.regtype1);
                    break;

                case "or":
                    processor.Or(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "pop":
                    processor.Pop(ra);
                    break;

                case "popa": case "popad":
                    processor.Popa();
                    break;

                case "popf": case "popfd":
                    processor.Popf();
                    break;

                case "push":
                    processor.Push(value_a, inst.regtype1);
                    break;

                case "pusha": case "pushad":
                    processor.Pusha(value_a, inst.regtype1);
                    break;

                case "pushf": case "pushfd":
                    processor.Pushf(value_a, inst.regtype1);
                    break;

                case "rcl":
                    processor.Rcl(ra, (int)value_b);
                    break;

                case "rcr":
                    processor.Rcr(ra, (int)value_b);
                    break;

                case "rol":
                    processor.Rol(ra, (int)value_b);
                    break;

                case "ror":
                    processor.Ror(ra, (int)value_b);
                    break;

                case "sahf":
                    processor.Sahf();
                    break;

                case "sal": case "shl":
                    processor.Sal(ra, (int)value_b, inst.regtype1);
                    break;

                case "sar":
                    processor.Sar(ra, (int)value_b, inst.regtype1);
                    break;

                case "sbb":
                    processor.Sbb(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "shr":
                    processor.Shr(ra, (int)value_b, inst.regtype1);
                    break;

                case "stc":
                    processor.Stc();
                    break;

                case "std":
                    processor.Std();
                    break;

                case "sti":
                    processor.Sti();
                    break;

                case "sub":
                    processor.Sub(ra, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "test":
                    processor.Test(value_a, value_b, inst.regtype1, inst.regtype2);
                    break;

                case "xchg":
                    processor.Xchg(ra, (Register)value_b, inst.regtype1, inst.regtype2);
                    break;

                case "xor":
                    processor.Xor(ra, value_b, inst.regtype1, inst.regtype2);
                    break;
            }
            return true;
        }

        public bool GetValueOrRegister(string data, aiOperandType type, out object obj)
        {
            obj = 0;
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
                    data = data.Trim(("'").ToCharArray());
                    if (data.Length > 1)
                    {
                        string letter = data.Substring(data.Length - 1, 1);
                        int nbase = 0;
                        if (letter == "h") nbase = 16;
                        else if (letter == "o") nbase = 8;
                        else if (letter == "b") nbase = 2;
                        if (nbase != 0)
                        {
                            data = data.Substring(0, data.Length - 1);
                            obj = BinaryNumber.GetDecimal(data, nbase);
                            return true;
                        }
                    }
                    obj = data;
                    return true;
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

        // Перепрыгнуть на метку
        public void JumpOnLabel(string label)
        {
            if (labelIndices.ContainsKey(label))
            {
                currentInstructionIndex = labelIndices[label];
            }
        }
    }
}
