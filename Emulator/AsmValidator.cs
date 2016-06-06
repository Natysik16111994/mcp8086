using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    class AsmValidator
    {
        private struct AsmConstruction
        {
            public string name, operand1, operand2;
            public int operandCount;
            public AsmConstruction(string s)
            {
                string[] dat = s.Split(' ');
                this.name = dat[0];
                this.operand1 = this.operand2 = "";
                if (dat.Length >= 2) this.operand1 = dat[1];
                if (dat.Length > 2) this.operand2 = dat[2];
                this.operandCount = dat.Length - 1;
            }
        }

        private static AsmValidator _instance = null;
        private Dictionary<string, AsmConstruction> dic;
        private List<string> openedProc;

        private AsmValidator()
        {
            dic = new Dictionary<string,AsmConstruction>();
            openedProc = new List<string>();

            string[] file = Properties.Resources.instructions.Split('\n');
            string a;
            foreach (string s in file)
            {
                if (s.Length - 1 == 0) continue;
                a = s.Trim((" \r").ToCharArray());
                if (a.Substring(0, 1) == ";") continue;
                AsmConstruction construction = new AsmConstruction(a);
                dic.Add(construction.name, construction);
            }
           
            Console.WriteLine("Hello");
        }

        private bool ValidateOperand(aiOperandType type, string op)
        {
            if (op == "r" && type != aiOperandType.Register) return false;
            if (op == "v")
            {
                if (type != aiOperandType.Register && type != aiOperandType.Value) return false;
            }
            if (op == "l" && type != aiOperandType.Label) return false;
            return true;
        }

        /// <summary>
        /// Выполняет проверку инструкции
        /// </summary>
        /// <param name="instruction">Экземпляр инструкции</param>
        /// <param name="log">Выводить информацию в консоль</param>
        /// <returns>Истина, если инструкция верна; ложь в ином случае.</returns>
        public bool Validate(AsmInstruction instruction, bool log = true)
        {
            // Проверка опкода
            if (!dic.ContainsKey(instruction.opcode))
            {
                if(log) MainForm.Instance.WriteConsole(string.Format("Ошибка. Неизвестная инструкция {0}.", instruction.opcode));
                return false;
            }

            // Проверка количества операндов
            AsmConstruction c = dic[instruction.opcode];
            int count = (instruction.type1 != aiOperandType.Null ? 1 : 0) + (instruction.type2 != aiOperandType.Null ? 1 : 0);
            if (count != c.operandCount)
            {
                if (log) MainForm.Instance.WriteConsole(string.Format("Ошибка. Количество операндов у инструкции {0}, должно равняться {1}.", instruction.opcode, c.operandCount));
                return false;
            }

            // Проверка операндов
            if (c.operandCount >= 1)
            {
                if(!ValidateOperand(instruction.type1, c.operand1))
                {
                    if (log) MainForm.Instance.WriteConsole(string.Format("Ошибка. Тип первого операнда у инструкции {0} не соотвествует нужному.", instruction.opcode));
                    return false;
                }
            }
            if (c.operandCount > 1)
            {
                if(!ValidateOperand(instruction.type2, c.operand2))
                {
                    if (log) MainForm.Instance.WriteConsole(string.Format("Ошибка. Тип второго операнда у инструкции {0} не соотвествует нужному.", instruction.opcode));
                    return false;
                }
            }
            return true;
        }

        public static AsmValidator GetInstance()
        {
            if (AsmValidator._instance == null) AsmValidator._instance = new AsmValidator();
            return AsmValidator._instance;
        }
    }
}
