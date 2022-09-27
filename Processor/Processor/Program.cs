using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor
{
    class Program
    {
        const int MEMORY_SIZE = 100;
        //Registers
        static byte Accumulator = new byte();

        static byte RegisterB = new byte();
        static byte RegisterC = new byte();
        static byte RegisterD = new byte();
        static byte RegisterE = new byte();

        //Flags
        static bool ZeroFlag = false;
        static bool SignFlag = false;

        //Initialize program counter with first address of RAM, thats 0
        static int ProgramCounter = 0;
        //Memory declaration with 100 location with Data space of 2 byte width        
        static byte[,] RAM = new byte[MEMORY_SIZE, 2];

        static byte[] Instruction = new byte[2];

        private void LoadProgramToRAM()
        {
            //Initialise RAM with the binary program code
            RAM[0, 0] = 2; RAM[0, 1] = 1;//MVI 1
            RAM[1, 0] = 1; RAM[1, 1] = 40;//STA 40
            RAM[2, 0] = 2; RAM[2, 1] = 2;//MVI 2
            RAM[3, 0] = 1; RAM[3, 1] = 41;//STA 41
            RAM[4, 0] = 2; RAM[4, 1] = 3;//MVI 3
            RAM[5, 0] = 1; RAM[5, 1] = 42;//STA 42
            RAM[6, 0] = 2; RAM[6, 1] = 4;//MVI 4
            RAM[7, 0] = 1; RAM[7, 1] = 43;//STA 43
            RAM[8, 0] = 2; RAM[8, 1] = 5;//MVI 5
            RAM[9, 0] = 1; RAM[9, 1] = 44;//STA 44
            RAM[10, 0] = 2; RAM[10, 1] = 5;//MVI 5
            RAM[11, 0] = 1; RAM[11, 1] = 50;//STA 50
            RAM[12, 0] = 2; RAM[12, 1] = 4;//MVI 4
            RAM[13, 0] = 1; RAM[13, 1] = 51;//STA 51
            RAM[14, 0] = 2; RAM[14, 1] = 3;//MVI 3
            RAM[15, 0] = 1; RAM[15, 1] = 52;//STA 52
            RAM[16, 0] = 2; RAM[16, 1] = 2;//MVI 2
            RAM[17, 0] = 1; RAM[17, 1] = 53;//STA 53
            RAM[18, 0] = 2; RAM[18, 1] = 1;//MVI 1
            RAM[19, 0] = 1; RAM[19, 1] = 54;//STA 54
            RAM[20, 0] = 2; RAM[20, 1] = 0;//MVI 0
            RAM[21, 0] = 8; RAM[21, 1] = 0;//MOV C,A
            RAM[22, 0] = 2; RAM[22, 1] = 40;//MVI 40
            RAM[23, 0] = 9; RAM[23, 1] = 0;//MOV D,A
            RAM[24, 0] = 2; RAM[24, 1] = 50;//MVI 50
            RAM[25, 0] = 10; RAM[25, 1] = 0;//MOV E,A
            RAM[26, 0] = 2; RAM[26, 1] = 6;//MVI 6
            RAM[27, 0] = 7; RAM[27, 1] = 0;//MOV B,A
            RAM[28, 0] = 43; RAM[28, 1] = 0;//DEC B
            RAM[29, 0] = 64; RAM[29, 1] = 37;//JZ 37
            RAM[30, 0] = 75; RAM[30, 1] = 0;//LDA D
            RAM[31, 0] = 88; RAM[31, 1] = 0;//MUL EM
            RAM[32, 0] = 12; RAM[32, 1] = 0;//ADD C
            RAM[33, 0] = 8; RAM[33, 1] = 0;//MOV C,A
            RAM[34, 0] = 39; RAM[34, 1] = 0;//INC D
            RAM[35, 0] = 40; RAM[35, 1] = 0;//INC E
            RAM[36, 0] = 63; RAM[36, 1] = 28;//JMP 28
            RAM[37, 0] = 72; RAM[37, 1] = 0;//HLT
        
        }

        private bool FetchDecodeNExecute()
        {
            if (ProgramCounter >= MEMORY_SIZE)
            {
                //finish if program counter exceeds memory limit
                return true;
            }
            Instruction[0] = RAM[ProgramCounter, 0];
            Instruction[1] = RAM[ProgramCounter, 1];

            Console.WriteLine("Fetching Address : " + ProgramCounter + " Instruction OPCODE : " + RAM[ProgramCounter, 0] + " OPERAND : " + RAM[ProgramCounter, 1]);

            return ControlUnitParse();
        }

        private bool ControlUnitParse()
        {
            const byte LDA = 0;
            const byte STA = 1;
            const byte MVI = 2;
            const byte MOV_A_B = 3;
            const byte MOV_A_C = 4;
            const byte MOV_A_D = 5;
            const byte MOV_A_E = 6;
            const byte MOV_B_A = 7;
            const byte MOV_C_A = 8;
            const byte MOV_D_A = 9;
            const byte MOV_E_A = 10;
            const byte ADD_B = 11;
            const byte ADD_C = 12;
            const byte ADD_D = 13;
            const byte ADD_E = 14;
            const byte ADD_MEM = 15;
            const byte SUB_B = 16;
            const byte SUB_C = 17;
            const byte SUB_D = 18;
            const byte SUB_E = 19;
            const byte SUB_MEM = 20;
            const byte MUL_B = 21;
            const byte MUL_C = 22;
            const byte MUL_D = 23;
            const byte MUL_E = 24;
            const byte MUL_MEM = 25;
            const byte DIV_B = 26;
            const byte DIV_C = 27;
            const byte DIV_D = 28;
            const byte DIV_E = 29;
            const byte DIV_MEM = 30;
            const byte REM_B = 31;
            const byte REM_C = 32;
            const byte REM_D = 33;
            const byte REM_E = 34;
            const byte REM_MEM = 35;
            const byte INC_A = 36;
            const byte INC_B = 37;
            const byte INC_C = 38;
            const byte INC_D = 39;
            const byte INC_E = 40;
            const byte INC_MEM = 41;
            const byte DEC_A = 42;
            const byte DEC_B = 43;
            const byte DEC_C = 44;
            const byte DEC_D = 45;
            const byte DEC_E = 46;
            const byte DEC_MEM = 47;
            const byte AND_B = 48;
            const byte AND_C = 49;
            const byte AND_D = 50;
            const byte AND_E = 51;
            const byte AND_MEM = 52;
            const byte OR_B = 53;
            const byte OR_C = 54;
            const byte OR_D = 55;
            const byte OR_E = 56;
            const byte OR_MEM = 57;
            const byte XOR_B = 58;
            const byte XOR_C = 59;
            const byte XOR_D = 60;
            const byte XOR_E = 61;
            const byte XOR_MEM = 62;
            const byte JMP = 63;
            const byte JZ = 64;
            const byte JN = 67;
            const byte JNZ = 68;
            const byte JNN = 71;
            const byte HLT = 72;
            const byte LDA_B = 73;
            const byte LDA_C = 74;
            const byte LDA_D = 75;
            const byte LDA_E = 76;
            const byte ADD_B_M = 77;
            const byte ADD_C_M = 78;
            const byte ADD_D_M = 79;
            const byte ADD_E_M = 80;
            const byte SUB_B_M = 81;
            const byte SUB_C_M = 82;
            const byte SUB_D_M = 83;
            const byte SUB_E_M = 84;
            const byte MUL_B_M = 85;
            const byte MUL_C_M = 86;
            const byte MUL_D_M = 87;
            const byte MUL_E_M = 88;
            const byte DIV_B_M = 89;
            const byte DIV_C_M = 90;
            const byte DIV_D_M = 91;
            const byte DIV_E_M = 92;
            const byte REM_B_M = 93;
            const byte REM_C_M = 94;
            const byte REM_D_M = 95;
            const byte REM_E_M = 96;

            bool stopExecution = false;

            switch (Instruction[0])
            {
                case LDA:
                    {
                        Console.WriteLine("Executing LDA");
                        //Loading Ram data at particular address to accumulator
                        Accumulator = RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case STA:
                    {
                        Console.WriteLine("Executing STA");
                        //Storing accumulator data to particular memory address from instruction
                        RAM[Instruction[1], 1] = Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case MVI:
                    {
                        Console.WriteLine("Executing MVI");
                        Accumulator = Instruction[1];
                        ++ProgramCounter;
                        break;
                    }
                case MOV_A_B:
                    {
                        Console.WriteLine("Executing MOV A,B");
                        Accumulator = RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_A_C:
                    {
                        Console.WriteLine("Executing MOV A,C");
                        Accumulator = RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_A_D:
                    {
                        Console.WriteLine("Executing MOV A,D");
                        Accumulator = RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_A_E:
                    {
                        Console.WriteLine("Executing MOV A,E");
                        Accumulator = RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_B_A:
                    {
                        Console.WriteLine("Executing MOVE B,A");
                        RegisterB = Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_C_A:
                    {
                        Console.WriteLine("Executing MOV C,A");
                        RegisterC = Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_D_A:
                    {
                        Console.WriteLine("Executing MOV D,A");
                        RegisterD = Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case MOV_E_A:
                    {
                        Console.WriteLine("Executing MOV E,A");
                        RegisterE = Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case ADD_B:
                    {
                        Console.WriteLine("Executing ADD B");
                        Accumulator += RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case ADD_C:
                    {
                        Console.WriteLine("Executing ADD C");
                        Accumulator += RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case ADD_D:
                    {
                        Console.WriteLine("Executing ADD D");
                        Accumulator += RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case ADD_E:
                    {
                        Console.WriteLine("Executing ADD E");
                        Accumulator += RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case ADD_MEM:
                    {
                        Console.WriteLine("Executing ADD MEM");
                        Accumulator += RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case SUB_B:
                    {
                        Console.WriteLine("Executing SUB B");
                        Accumulator -= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case SUB_C:
                    {
                        Console.WriteLine("Executing SUB C");
                        Accumulator -= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case SUB_D:
                    {
                        Console.WriteLine("Executing SUB D");
                        Accumulator -= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case SUB_E:
                    {
                        Console.WriteLine("Executing SUB E");
                        Accumulator -= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case SUB_MEM:
                    {
                        Console.WriteLine("Executing SUB MEM");
                        Accumulator -= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case MUL_B:
                    {
                        Console.WriteLine("Executing MUL B");
                        Accumulator *= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case MUL_C:
                    {
                        Console.WriteLine("Executing MUL C");
                        Accumulator *= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case MUL_D:
                    {
                        Console.WriteLine("Executing MUL D");
                        Accumulator *= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case MUL_E:
                    {
                        Console.WriteLine("Executing MUL E");
                        Accumulator *= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case MUL_MEM:
                    {
                        Console.WriteLine("Executing MUL MEM");
                        Accumulator *= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case DIV_B:
                    {
                        Console.WriteLine("Executing DIV B");
                        Accumulator /= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case DIV_C:
                    {
                        Console.WriteLine("Executing DIV C");
                        Accumulator /= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case DIV_D:
                    {
                        Console.WriteLine("Executing DIV D");
                        Accumulator /= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case DIV_E:
                    {
                        Console.WriteLine("Executing DIV E");
                        Accumulator /= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case DIV_MEM:
                    {
                        Console.WriteLine("Executing DIV MEM");
                        Accumulator /= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case REM_B:
                    {
                        Console.WriteLine("Executing REM B");
                        Accumulator %= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case REM_C:
                    {
                        Console.WriteLine("Executing REM C");
                        Accumulator %= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case REM_D:
                    {
                        Console.WriteLine("Executing REM D");
                        Accumulator %= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case REM_E:
                    {
                        Console.WriteLine("Executing REM E");
                        Accumulator %= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case REM_MEM:
                    {
                        Console.WriteLine("Executing REM MEM");
                        Accumulator %= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case INC_A:
                    {
                        Console.WriteLine("Executing INC A");
                        ++Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case INC_B:
                    {
                        Console.WriteLine("Executing INC B");
                        Accumulator = ++RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case INC_C:
                    {
                        Console.WriteLine("Executing INC C");
                        Accumulator = ++RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case INC_D:
                    {
                        Console.WriteLine("Executing INC D");
                        Accumulator = ++RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case INC_E:
                    {
                        Console.WriteLine("Executing INC E");
                        Accumulator = ++RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case INC_MEM:
                    {
                        Console.WriteLine("Executing INC MEM");
                        Accumulator = ++RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case DEC_A:
                    {
                        Console.WriteLine("Executing DEC A");
                        --Accumulator;
                        ++ProgramCounter;
                        break;
                    }
                case DEC_B:
                    {
                        Console.WriteLine("Executing DEC B");
                        Accumulator = --RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case DEC_C:
                    {
                        Console.WriteLine("Executing DEC C");
                        Accumulator = --RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case DEC_D:
                    {
                        Console.WriteLine("Executing DEC D");
                        Accumulator = --RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case DEC_E:
                    {
                        Console.WriteLine("Executing DEC E");
                        Accumulator = --RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case DEC_MEM:
                    {
                        Console.WriteLine("Executing DEC MEM");
                        Accumulator = --RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case AND_B:
                    {
                        Console.WriteLine("Executing AND B");
                        Accumulator &= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case AND_C:
                    {
                        Console.WriteLine("Executing AND C");
                        Accumulator &= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case AND_D:
                    {
                        Console.WriteLine("Executing AND D");
                        Accumulator &= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case AND_E:
                    {
                        Console.WriteLine("Executing AND E");
                        Accumulator &= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case AND_MEM:
                    {
                        Console.WriteLine("Executing AND MEM");
                        Accumulator &= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case OR_B:
                    {
                        Console.WriteLine("Executing OR B");
                        Accumulator |= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case OR_C:
                    {
                        Console.WriteLine("Executing OR C");
                        Accumulator |= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case OR_D:
                    {
                        Console.WriteLine("Executing OR D");
                        Accumulator |= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case OR_E:
                    {
                        Console.WriteLine("Executing OR E");
                        Accumulator |= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case OR_MEM:
                    {
                        Console.WriteLine("Executing OR MEM");
                        Accumulator |= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case XOR_B:
                    {
                        Console.WriteLine("Executing XOR B");
                        Accumulator ^= RegisterB;
                        ++ProgramCounter;
                        break;
                    }
                case XOR_C:
                    {
                        Console.WriteLine("Executing XOR C");
                        Accumulator ^= RegisterC;
                        ++ProgramCounter;
                        break;
                    }
                case XOR_D:
                    {
                        Console.WriteLine("Executing XOR D");
                        Accumulator ^= RegisterD;
                        ++ProgramCounter;
                        break;
                    }
                case XOR_E:
                    {
                        Console.WriteLine("Executing XOR E");
                        Accumulator ^= RegisterE;
                        ++ProgramCounter;
                        break;
                    }
                case XOR_MEM:
                    {
                        Console.WriteLine("Executing XOR MEM");
                        Accumulator ^= RAM[Instruction[1], 1];
                        ++ProgramCounter;
                        break;
                    }
                case JMP:
                    {
                        Console.WriteLine("Executing JMP");
                        ProgramCounter = Instruction[1];
                        break;
                    }
                case JZ:
                    {
                        Console.WriteLine("Executing JZ");
                        if (ZeroFlag)
                        {
                            ProgramCounter = Instruction[1];
                        }
                        else
                        {
                            ++ProgramCounter;
                        }
                        break;
                    }

                case JN:
                    {
                        Console.WriteLine("Executing JN");
                        if (SignFlag)
                        {
                            ProgramCounter = Instruction[1];
                        }
                        else
                        {
                            ++ProgramCounter;
                        }
                        break;
                    }
                case JNZ:
                    {
                        Console.WriteLine("Executing JNZ");
                        if (!ZeroFlag)
                        {
                            ProgramCounter = Instruction[1];
                        }
                        else
                        {
                            ++ProgramCounter;
                        }
                        break;
                    }

                case JNN:
                    {
                        Console.WriteLine("Executing JNN");
                        if (!SignFlag)
                        {
                            ProgramCounter = Instruction[1];
                        }
                        else
                        {
                            ++ProgramCounter;
                        }
                        break;
                    }
                case HLT:
                    {
                        Console.WriteLine("Executing HLT");
                        stopExecution = true;
                        break;
                    }
                case LDA_B:
                    {
                        Console.WriteLine("Executing LDA_B");
                        Accumulator = RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case LDA_C:
                    {
                        Console.WriteLine("Executing LDA_C");
                        Accumulator = RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case LDA_D:
                    {
                        Console.WriteLine("Executing LDA_D");
                        Accumulator = RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case LDA_E:
                    {
                        Console.WriteLine("Executing LDA_E");
                        Accumulator = RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                case ADD_B_M:
                    {
                        Console.WriteLine("Executing ADD_B_M");
                        Accumulator += RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case ADD_C_M:
                    {
                        Console.WriteLine("Executing ADD_C_M");
                        Accumulator += RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case ADD_D_M:
                    {
                        Console.WriteLine("Executing ADD_D_M");
                        Accumulator += RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case ADD_E_M:
                    {
                        Console.WriteLine("Executing ADD_E_M");
                        Accumulator += RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                case SUB_B_M:
                    {
                        Console.WriteLine("Executing SUB_B_M");
                        Accumulator -= RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case SUB_C_M:
                    {
                        Console.WriteLine("Executing SUB_C_M");
                        Accumulator -= RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case SUB_D_M:
                    {
                        Console.WriteLine("Executing SUB_D_M");
                        Accumulator -= RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case SUB_E_M:
                    {
                        Console.WriteLine("Executing SUB_E_M");
                        Accumulator -= RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                case DIV_B_M:
                    {
                        Console.WriteLine("Executing DIV_B_M");
                        Accumulator /= RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case DIV_C_M:
                    {
                        Console.WriteLine("Executing DIV_C_M");
                        Accumulator /= RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case DIV_D_M:
                    {
                        Console.WriteLine("Executing DIV_D_M");
                        Accumulator /= RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case DIV_E_M:
                    {
                        Console.WriteLine("Executing DIV_E_M");
                        Accumulator /= RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                case MUL_B_M:
                    {
                        Console.WriteLine("Executing MUL_B_M");
                        Accumulator *= RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case MUL_C_M:
                    {
                        Console.WriteLine("Executing MUL_C_M");
                        Accumulator *= RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case MUL_D_M:
                    {
                        Console.WriteLine("Executing MUL_D_M");
                        Accumulator *= RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case MUL_E_M:
                    {
                        Console.WriteLine("Executing MUL_E_M");
                        Accumulator *= RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                case REM_B_M:
                    {
                        Console.WriteLine("Executing REM_B_M");
                        Accumulator %= RAM[RegisterB, 1];
                        ++ProgramCounter;
                        break;
                    }
                case REM_C_M:
                    {
                        Console.WriteLine("Executing REM_C_M");
                        Accumulator %= RAM[RegisterC, 1];
                        ++ProgramCounter;
                        break;
                    }
                case REM_D_M:
                    {
                        Console.WriteLine("Executing REM_D_M");
                        Accumulator %= RAM[RegisterD, 1];
                        ++ProgramCounter;
                        break;
                    }
                case REM_E_M:
                    {
                        Console.WriteLine("Executing REM_E_M");
                        Accumulator %= RAM[RegisterE, 1];
                        ++ProgramCounter;
                        break;
                    }
                default:
                    {
                        stopExecution = true;
                    }
                    break;
            }


            //Setting flags
            if (Accumulator == 0)
            {
                ZeroFlag = true;
            }
            else
            {
                ZeroFlag = false;
            }

            if (Accumulator >= 128)
            {
                SignFlag = true;
            }
            else
            {
                SignFlag = false;
            }

            return stopExecution;
        }

        public void runProcessor()
        {

            LoadProgramToRAM();
            //program counter point to first address in ram
            ProgramCounter = 0;

            //Printing status and memory map before running program 
            PrintProcessorStatus();
            PrintMemory();

            while (!FetchDecodeNExecute())
            {
                //PrintProcessorStatus();
                //Console.ReadLine();
            }

            //Printing status and memory mab after execution
            PrintProcessorStatus();
            PrintMemory();
        }


        private void PrintProcessorStatus()
        {
            Console.WriteLine("Printing Processor Status...");
            Console.WriteLine("Data in Accumulator : " + Accumulator);
            Console.WriteLine("Data in Register B : " + RegisterB);
            Console.WriteLine("Data in Register C : " + RegisterC);
            Console.WriteLine("Data in Register D : " + RegisterD);
            Console.WriteLine("Data in Register E : " + RegisterE);
            Console.WriteLine("Zero Flag : " + (ZeroFlag ? 1 : 0));
            Console.WriteLine("Sign Flag : " + (SignFlag ? 1 : 0));
            Console.WriteLine("Program Counter has Address : " + ProgramCounter);
            Console.WriteLine("Instruction Register has OPCODE : " + Instruction[0] + " OPERAND : " + Instruction[1]);
        }

        private void PrintMemory()
        {
            Console.WriteLine("Memory Starting from address 0 to " + (MEMORY_SIZE - 1));
            for (int i = 0; i < MEMORY_SIZE; i++)
            {
                Console.WriteLine("Address : " + i + " OPCODE : " + RAM[i, 0] + " OPERAND : " + RAM[i, 1]);
            }
            Console.WriteLine("Memory Reading Finished.");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Processor Starts...");
            p.runProcessor();

            Console.Write("\nPress any key to continue... ");
            Console.ReadLine();
        }
    }
}
