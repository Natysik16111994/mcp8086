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
        Label,
        Null
    };

    public struct AsmInstruction
    {
        public string opcode, operand1, operand2;
        public int line;
        public aiOperandType type1, type2;
        public Register.Types regtype1, regtype2;
        public string raw;
        public int partsCount;
    }

    public struct AsmProc
    {
        public int startInstruction, endInstruction;
        public string name;
        public AsmProc(string name, int startInstruction)
        {
            this.name = name;
            this.startInstruction = startInstruction;
            this.endInstruction = 0;
        }
    }

    public struct AsmTrace
    {
        public int callFrom, endProc;
        public AsmTrace(int callFrom, int endproc)
        {
            this.callFrom = callFrom;
            this.endProc = endproc;
        }
    }

    public class Assembler
    {
        private Processor processor;
        private AsmValidator validator;

        // программа
        private List<AsmInstruction> instructions = new List<AsmInstruction>();
        // список меток
        private Dictionary<string, int> labelIndices = new Dictionary<string, int>();
        private int currentInstructionIndex = 0;

        // открытые процедуры
        private Dictionary<string, AsmProc> openedProc = new Dictionary<string, AsmProc>();
        private Dictionary<string, AsmProc> closedProc = new Dictionary<string, AsmProc>();
        private Dictionary<int, int> bodyProc = new Dictionary<int, int>();

        // стек вызовов
        private Stack<AsmTrace> callStack = new Stack<AsmTrace>();

        // Конструктор
        public Assembler(Processor proc)
        {
            this.processor = proc;
            validator = AsmValidator.GetInstance();
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
                return instructions.Count > 0 && !ProgramEnd;
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
            openedProc.Clear(); closedProc.Clear();
            bodyProc.Clear();
            callStack.Clear();
            processor.memory.Clear();

            string a, b, c;
            for (int i = 0; i < content.Length; i++)
            {
                a = content[i].Trim();
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

                // определение объявления массива
                if (IsArraySet(b))
                {
                    string name = "";
                    int[] array = null;
                    if (ParseArray(b, out name, out array))
                    {
                        processor.memory.Add(name, array);
                        continue;
                    }
                }

                // определение ассемблерной инструкции
                AsmInstruction instruction = new AsmInstruction();
                if (ParseString(b, out instruction))
                {
                    instruction.line = i;

                    // проверка на процедуру
                    if (instruction.partsCount == 2)
                    {
                        if (instruction.operand1 == "proc")
                        {
                            openedProc.Add(instruction.opcode, new AsmProc(instruction.opcode, instruction.line));
                            continue;
                        }
                        else if (instruction.operand1 == "endp")
                        {
                            if (openedProc.ContainsKey(instruction.opcode))
                            {
                                AsmProc proc = openedProc[instruction.opcode];
                                openedProc.Remove(instruction.opcode);
                                proc.startInstruction = FindNextInstruction(proc.startInstruction);
                                proc.endInstruction = FindNextInstruction(i - 1);
                                closedProc.Add(instruction.opcode, proc);
                                bodyProc.Add(proc.startInstruction, proc.endInstruction);
                                continue;
                            }
                            else
                            {
                                MainForm.Instance.WriteConsole(string.Format("Тело процедуры {0} не было открыто, но есть попытка его закрытия.", instruction.opcode));
                                return false;
                            }
                        }
                    }

                    // валидация процедуры
                    if (!validator.Validate(instruction))
                    {
                        return false;
                    }
                    instructions.Add(instruction);
                }
                else
                {
                    return false;
                }
            }

            // проверка, что тела процедур закрыты
            if (openedProc.Count > 0)
            {
                foreach (var p in openedProc)
                {
                    MainForm.Instance.WriteConsole(string.Format("Тело процедуры {0} было открыто, но не закрыто.", p.Value.name));
                }
                return false;
            }

            // проверка, чтобы тело процедур не выполнялись
            while (bodyProc.ContainsKey(currentInstructionIndex))
            {
                currentInstructionIndex = bodyProc[currentInstructionIndex] + 1;
            }

            if (instructions.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// Возвращает индекс инструкции по номеру строки
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private int FindNextInstruction(int line)
        {
            for(int i = 0; i < instructions.Count; i++)
            {
                if(instructions[i].line >= line)
                {
                    return i;
                }
            }
            return instructions.Count;
        }

        // Парсинг строки
        private bool ParseString(string str, out AsmInstruction instruct)
        {
            instruct.raw = str;
            instruct.opcode = instruct.operand1 = instruct.operand2 = "";
            instruct.type1 = instruct.type2 = aiOperandType.Value;
            instruct.regtype1 = instruct.regtype2 = Register.Types.None;
            instruct.partsCount = instruct.line = 0;

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

            for (int i = 0; i < 2; i++)
            {
                if (operands[i].Length >= 2)
                {
                    if (operands[i].Substring(0, 1) == "'" || operands[i].Substring(operands[i].Length - 1, 1) == "'")
                    {
                        operands[i] = operands[i].Trim('\'');
                        if (operands[i].Length == 0) operands[i] = "0";
                        operands[i] = Encoding.ASCII.GetBytes(operands[i])[0].ToString();
                    }
                }
            }

            instruct.opcode = opcode.ToLower();
            instruct.operand1 = operands[0]; instruct.operand2 = operands[1];
            instruct.type1 = GetOperandType(operands[0]);
            instruct.type2 = GetOperandType(operands[1]);
            if (instruct.type1 == aiOperandType.Register) instruct.regtype1 = GetRegisterType(operands[0]);
            if (instruct.type2 == aiOperandType.Register) instruct.regtype2 = GetRegisterType(operands[1]);
            instruct.partsCount = 1 + (instruct.type1 != aiOperandType.Null ? 1 : 0) + (instruct.type2 != aiOperandType.Null ? 1 : 0);
            return true;
        }

        /// <summary>
        /// Парсит строку в массив
        /// </summary>
        /// <param name="name">Возвращаемое имя массива</param>
        /// <param name="array">Возвращаемый массив</param>
        /// <returns></returns>
        public bool ParseArray(string s, out string name, out int[] array)
        {
            name = "";
            array = new int[] { 0 };
            string[] parts = s.Split(' ');
            if (parts[1] == "db" && parts[1] == "dw" && parts[1] == "dd") return false;
            name = parts[0];
            string p = string.Join(" ", parts, 2, parts.Length - 2);
            List<int> bytes = new List<int>();
            string a, b = "";
            bool quotes = false;
            bool str = false;
            for (int i = 0; i < p.Length; i++)
            {
                a = p.Substring(i, 1);
                if (a == "'")
                {
                    quotes = !quotes;
                    if (quotes) str = true;
                }
                if ((a == "," || i == p.Length - 1) && !quotes)
                {
                    EncodeData(str ? b : b.Trim(), str, bytes);
                    str = false;
                    b = "";
                    continue;
                }
                if(a != "'") b += a;
            }
            array = bytes.ToArray();
            bytes.Clear();
            return true;
        }

        private void EncodeData(string d, bool text, List<int> list)
        {
            if (text)
            {
                byte[] data = Encoding.ASCII.GetBytes(d.TrimEnd('$'));
                foreach (var dat in data) list.Add(dat);
                return;
            }
            int res = 0;
            int.TryParse(d, out res);
            list.Add(res);
        }

        /// <summary>
        /// Определяет, является ли строка определением массива
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns></returns>
        public bool IsArraySet(string s)
        {
            string[] parts = s.Split(' ');
            if(parts.Length < 3) return false;
            string p;
            for(int i = 0; i < parts.Length; i++)
            {
                p = parts[i].Trim().ToLower();
                if(p == "db" || p == "dw" || p == "dd")
                {
                    if (i != 0 && i != parts.Length - 1) return true;
                }
            }
            return false;
        }

        // Возвращает тип операнда
        public aiOperandType GetOperandType(string operand)
        {
            operand = operand.Trim();
            if (operand.Length == 0) return aiOperandType.Null;
            if(processor.GetRegisterByName(operand) != null) return aiOperandType.Register;
            if (OperandIsLabel(operand)) return aiOperandType.Label;
            return aiOperandType.Value;
        }

        /// <summary>
        /// Проверяет, является ли операнд меткой
        /// </summary>
        /// <param name="operand"></param>
        /// <returns></returns>
        public bool OperandIsLabel(string operand)
        {
            if (operand.Length == 0) return false;
            int test = 0;
            if (int.TryParse(operand, out test)) return false;

            string a = operand.Substring(operand.Length - 1, 1);
            if (a == "b")
            {
                a = operand.Trim(("b01").ToCharArray());
                if (a.Length == 0) return false;
            }
            else if (a == "o")
            {
                a = operand.Trim(("o01234567").ToCharArray());
                if(a.Length == 0) return false;
            }
            else if (a == "h")
            {
                a = operand.Trim(("h0123456789abcdef").ToCharArray());
                if(a.Length == 0) return false;
            }
            return true;
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

            // проверка, выполнилась ли процедура, для возврата обратно
            if (callStack.Count > 0)
            {
                AsmTrace trace = callStack.Peek();
                if (trace.endProc + 1 == currentInstructionIndex)
                {
                    currentInstructionIndex = trace.callFrom;
                    callStack.Pop();
                }
            }

            // проверка, чтобы тело процедур не выполнялись
            if (bodyProc.ContainsKey(currentInstructionIndex))
            {
                currentInstructionIndex = bodyProc[currentInstructionIndex] + 1;
            }

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
                    AsmProc proc = closedProc[inst.operand1];
                    callStack.Push(new AsmTrace(currentInstructionIndex, proc.endInstruction));
                    currentInstructionIndex = proc.startInstruction;
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
                    processor.Cmp(value_a, value_b, inst.regtype1, inst.regtype2);
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

                // Прерывание
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

                case "lea":
                    processor.Lea(ra, (string)value_b, inst.regtype1);
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

                case "ret":
                    processor.Ret();
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
            else if (type == aiOperandType.Label)
            {
                obj = data;
                return true;
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

        /// <summary>
        /// Возвращает управление
        /// </summary>
        public void Ret()
        {
            if (callStack.Count > 0)
            {
                AsmTrace trace = callStack.Pop();
                currentInstructionIndex = trace.callFrom;
            }
        }
    }
}
