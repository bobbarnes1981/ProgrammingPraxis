using System;
using System.IO;

namespace ProgrammingPraxis.Library.Cryptography
{
    public class DES : IStreamCipher
    {
        //http://page.math.tu-berlin.de/~kant/teaching/hess/krypto-ws2006/des.htm

        private int[] m_PC1 = new int[]
        {
              57,   49,    41,   33,    25,    17,    9,
               1,   58,    50,   42,    34,    26,   18,
              10,    2,    59,   51,    43,    35,   27,
              19,   11,     3,   60,    52,    44,   36,
              63,   55,    47,   39,    31,    23,   15,
               7,   62,    54,   46,    38,    30,   22,
              14,    6,    61,   53,    45,    37,   29,
              21,   13,     5,   28,    20,    12,    4,
        };

        private int[] m_PC2 = new int[]
        {
            14,    17,   11,    24,     1,    5,
             3,    28,   15,     6,    21,   10,
            23,    19,   12,     4,    26,    8,
            16,     7,   27,    20,    13,    2,
            41,    52,   31,    37,    47,   55,
            30,    40,   51,    45,    33,   48,
            44,    49,   39,    56,    34,   53,
            46,    42,   50,    36,    29,   32,
        };

        private int[] m_IP = new int[]
        {
            58,    50,   42,    34,    26,   18,    10,    2,
            60,    52,   44,    36,    28,   20,    12,    4,
            62,    54,   46,    38,    30,   22,    14,    6,
            64,    56,   48,    40,    32,   24,    16,    8,
            57,    49,   41,    33,    25,   17,     9,    1,
            59,    51,   43,    35,    27,   19,    11,    3,
            61,    53,   45,    37,    29,   21,    13,    5,
            63,    55,   47,    39,    31,   23,    15,    7,
        };

        private int[] m_E = new int[]
        {
            32,     1,    2,     3,     4,    5,
             4,     5,    6,     7,     8,    9,
             8,     9,   10,    11,    12,   13,
            12,    13,   14,    15,    16,   17,
            16,    17,   18,    19,    20,   21,
            20,    21,   22,    23,    24,   25,
            24,    25,   26,    27,    28,   29,
            28,    29,   30,    31,    32,    1,
        };

        private int[][] m_S = new int[][]
        {
            new int[] {
                14,  4,  13,  1,   2, 15,  11,  8,   3, 10,   6, 12,   5,  9,   0,  7,
                 0, 15,   7,  4,  14,  2,  13,  1,  10,  6,  12, 11,   9,  5,   3,  8,
                 4,  1,  14,  8,  13,  6,   2, 11,  15, 12,   9,  7,   3, 10,   5,  0,
                15, 12,   8,  2,   4,  9,   1,  7,   5, 11,   3, 14,  10,  0,   6, 13,
            },
            new int[]
            {
                15,  1,   8, 14,   6, 11,   3,  4,   9,  7,   2, 13,  12,  0,   5, 10,
                 3, 13,   4,  7,  15,  2,   8, 14,  12,  0,   1, 10,   6,  9,  11,  5,
                 0, 14,   7, 11,  10,  4,  13,  1,   5,  8,  12,  6,   9,  3,   2, 15,
                13,  8,  10,  1,   3, 15,   4,  2,  11,  6,   7, 12,   0,  5,  14,  9,
            },
            new int[]
            {
                10,  0,   9, 14,   6,  3,  15,  5,   1, 13,  12,  7,  11,  4,   2,  8,
                13,  7,   0,  9,   3,  4,   6, 10,   2,  8,   5, 14,  12, 11,  15,  1,
                13,  6,   4,  9,   8, 15,   3,  0,  11,  1,   2, 12,   5, 10,  14,  7,
                 1, 10,  13,  0,   6,  9,   8,  7,   4, 15,  14,  3,  11,  5,   2, 12,
            },
            new int[]
            {
                 7, 13,  14,  3,   0,  6,   9, 10,   1,  2,   8,  5,  11, 12,   4, 15,
                13,  8,  11,  5,   6, 15,   0,  3,   4,  7,   2, 12,   1, 10,  14,  9,
                10,  6,   9,  0,  12, 11,   7, 13,  15,  1,   3, 14,   5,  2,   8,  4,
                 3, 15,   0,  6,  10,  1,  13,  8,   9,  4,   5, 11,  12,  7,   2, 14,
            },
            new int[]
            {
                 2, 12,   4,  1,   7, 10,  11,  6,   8,  5,   3, 15,  13,  0,  14,  9,
                14, 11,   2, 12,   4,  7,  13,  1,   5,  0,  15, 10,   3,  9,   8,  6,
                 4,  2,   1, 11,  10, 13,   7,  8,  15,  9,  12,  5,   6,  3,   0, 14,
                11,  8,  12,  7,   1, 14,   2, 13,   6, 15,   0,  9,  10,  4,   5,  3,
            },
            new int[]
            {
                12,  1,  10, 15,   9,  2,   6,  8,   0, 13,   3,  4,  14,  7,   5, 11,
                10, 15,   4,  2,   7, 12,   9,  5,   6,  1,  13, 14,   0, 11,   3,  8,
                 9, 14,  15,  5,   2,  8,  12,  3,   7,  0,   4, 10,   1, 13,  11,  6,
                 4,  3,   2, 12,   9,  5,  15, 10,  11, 14,   1,  7,   6,  0,   8, 13,
            },
            new int[]
            {
                 4, 11,   2, 14,  15,  0,   8, 13,   3, 12,   9,  7,   5, 10,   6,  1,
                13,  0,  11,  7,   4,  9,   1, 10,  14,  3,   5, 12,   2, 15,   8,  6,
                 1,  4,  11, 13,  12,  3,   7, 14,  10, 15,   6,  8,   0,  5,   9,  2,
                 6, 11,  13,  8,   1,  4,  10,  7,   9,  5,   0, 15,  14,  2,   3, 12,
            },
            new int[]
            {
                13,  2,   8,  4,   6, 15,  11,  1,  10,  9,   3, 14,   5,  0,  12,  7,
                 1, 15,  13,  8,  10,  3,   7,  4,  12,  5,   6, 11,   0, 14,   9,  2,
                 7, 11,   4,  1,   9, 12,  14,  2,   0,  6,  10, 13,  15,  3,   5,  8,
                 2,  1,  14,  7,   4, 10,   8, 13,  15, 12,   9,  0,   3,  5,   6, 11,
            },
        };

        private int[] m_P = new int[]
        {
            16,   7,  20,  21,
            29,  12,  28,  17,
             1,  15,  23,  26,
             5,  18,  31,  10,
             2,   8,  24,  14,
            32,  27,   3,   9,
            19,  13,  30,   6,
            22,  11,   4,  25,
        };

        private int[] m_IP1 = new int[]
        {
            40,     8,   48,    16,    56,   24,    64,   32,
            39,     7,   47,    15,    55,   23,    63,   31,
            38,     6,   46,    14,    54,   22,    62,   30,
            37,     5,   45,    13,    53,   21,    61,   29,
            36,     4,   44,    12,    52,   20,    60,   28,
            35,     3,   43,    11,    51,   19,    59,   27,
            34,     2,   42,    10,    50,   18,    58,   26,
            33,     1,   41,     9,    49,   17,    57,   25,
        };

        private int[] m_shifts = new int[]
        {
                1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1
        };

        private Bits m_key;

        private Bits[] m_subKeys;

        private Bits m_initializationVector;

        private DESMode m_mode;

        public DES(long key, DESMode mode)
            : this(key, mode, 0x00000000)
        {
        }

        public DES(long key, DESMode mode, long initializationVector)
        {
            m_key = Bits.Create(key);
            //Console.WriteLine("Key: {0}", m_key.ToString());
            m_mode = mode;
            m_initializationVector = Bits.Create(initializationVector);
        }

        public void Encipher(Stream inStream, Stream outStream)
        {
            // Create 16 subkeys, each of which is 48-bits long
            m_subKeys = GenerateSubKeys();

            switch (m_mode)
            {
                case DESMode.ECB:
                    ProcessStream(inStream, outStream, true, ProcessECBStreamDelegate);
                    break;

                case DESMode.CBC:
                    ProcessStream(inStream, outStream, true, ProcessCBCStreamDelegate);
                    break;

                case DESMode.CFB:
                    ProcessStream(inStream, outStream, true, ProcessCFBStreamDelegate);
                    break;

                case DESMode.OFB:
                    ProcessStream(inStream, outStream, true, ProcessOFBStreamDelegate);
                    break;

                default:
                    throw new Exception(string.Format("Unhandled DES mode {0}", m_mode));
            }
        }

        public void Decipher(Stream inStream, Stream outStream)
        {
            // Create 16 subkeys, each of which is 48-bits long
            m_subKeys = GenerateSubKeys();

            switch (m_mode)
            {
                case DESMode.ECB:
                    ProcessStream(inStream, outStream, false, ProcessECBStreamDelegate);
                    break;

                case DESMode.CBC:
                    ProcessStream(inStream, outStream, false, ProcessCBCStreamDelegate);
                    break;

                case DESMode.CFB:
                    ProcessStream(inStream, outStream, false, ProcessCFBStreamDelegate);
                    break;

                case DESMode.OFB:
                    ProcessStream(inStream, outStream, false, ProcessOFBStreamDelegate);
                    break;

                default:
                    throw new Exception(string.Format("Unhandled DES mode {0}", m_mode));
            }
        }
        
        public delegate Bits ProcessStreamDelegate(Bits previousBlock, Bits currentBlock, int bytesRead, Stream output, bool forwardKeys);

        public void ProcessStream(Stream input, Stream output, bool forwardKeys, ProcessStreamDelegate processor)
        {
            Bits previousBlock = new Bits(m_initializationVector);
            do
            {
                byte[] inBuffer = new byte[8];
                int bytesRead = input.Read(inBuffer, 0, 8);
                if (bytesRead == 0)
                {
                    // reached end of stream, so break out of loop
                    break;
                }

                previousBlock = processor(previousBlock, Bits.Create(inBuffer), bytesRead, output, forwardKeys);
            } while (true);
        }

        public Bits ProcessECBStreamDelegate(Bits previousBlock, Bits currentBlock, int bytesRead, Stream output, bool forwardKeys)
        {
            if (bytesRead < 8)
            {
                // pad block
            }

            Console.WriteLine("Input Block: {0}", currentBlock.ToHexString());

            Bits processedBlock = Process64BitBlock(currentBlock, forwardKeys);

            Console.WriteLine("Output Block: {0}", processedBlock.ToHexString());

            byte[] outBuffer = processedBlock.ToByteArray();
            output.Write(outBuffer, 0, outBuffer.Length);

            return currentBlock;
        }
        
        public Bits ProcessCBCStreamDelegate(Bits previousBlock, Bits currentBlock, int bytesRead, Stream output, bool forwardKeys)
        {
            if (bytesRead < 8)
            {
                // pad block
            }
            
            Console.WriteLine("Input Block: {0}", currentBlock.ToHexString());

            if (forwardKeys)
            {
                // Encryption
                currentBlock = currentBlock.Xor(previousBlock);
                Console.WriteLine("Pre Chain Block: {0}", currentBlock.ToHexString());
            }

            Bits processedBlock = Process64BitBlock(currentBlock, forwardKeys);

            if (!forwardKeys)
            {
                // Decryption
                processedBlock = processedBlock.Xor(previousBlock);
                Console.WriteLine("Post Chain Block: {0}", processedBlock.ToHexString());
            }

            Console.WriteLine("Output Block: {0}", processedBlock.ToHexString());

            byte[] outBuffer = processedBlock.ToByteArray();
            output.Write(outBuffer, 0, outBuffer.Length);


            if (forwardKeys)
            {
                // Encryption
                return new Bits(processedBlock);
            }
            else
            {
                // Decryption
                return new Bits(currentBlock);
            }
        }

        public Bits ProcessCFBStreamDelegate(Bits previousBlock, Bits currentBlock, int bytesRead, Stream output, bool forwardKeys)
        {
            if (bytesRead < 8)
            {
                // pad block
            }
            
            Console.WriteLine("Input Block: {0}", currentBlock.ToHexString());

            Bits processedBlock = Process64BitBlock(previousBlock, true);

            processedBlock = processedBlock.Xor(currentBlock);

            Console.WriteLine("Output Block: {0}", processedBlock.ToHexString());

            byte[] outBuffer = processedBlock.ToByteArray();
            output.Write(outBuffer, 0, outBuffer.Length);

            if (forwardKeys)
            {
                return new Bits(processedBlock);
            }
            else
            {
                return new Bits(currentBlock);
            }
        }

        public Bits ProcessOFBStreamDelegate(Bits previousBlock, Bits currentBlock, int bytesRead, Stream output, bool forwardKeys)
        {
            if (bytesRead < 8)
            {
                // pad block
            }

            Console.WriteLine("Input Block: {0}", currentBlock.ToHexString());

            Bits outputBlock = Process64BitBlock(previousBlock, true);

            Bits processedBlock = outputBlock.Xor(currentBlock);

            Console.WriteLine("Output Block: {0}", processedBlock.ToHexString());

            byte[] outBuffer = processedBlock.ToByteArray();
            output.Write(outBuffer, 0, outBuffer.Length);

            return outputBlock;
        }

        public Bits Process64BitBlock(Bits block, bool forwardKeys)
        {
            if (block.Length != 64)
            {
                throw new Exception(string.Format("Block is {0}, it should be 64", block.Length));
            }

            Bits ip = Permutate(block, m_IP);

            Bits[] lBlocks = new Bits[17];
            Bits[] rBlocks = new Bits[17];

            Tuple<Bits, Bits> split = ip.Split();
            lBlocks[0] = split.Item1;
            rBlocks[0] = split.Item2;

            for (int i = 1; i < 17; i++)
            {
                //Ln = Rn-1
                lBlocks[i] = rBlocks[i - 1];
                
                //Rn = Ln-1 + f(Rn-1, Kn)
                if (forwardKeys)
                {
                    // encryption
                    rBlocks[i] = lBlocks[i - 1].Xor(Function(rBlocks[i - 1], m_subKeys[i - 1]));
                }
                else
                {
                    // decryption
                    rBlocks[i] = lBlocks[i - 1].Xor(Function(rBlocks[i - 1], m_subKeys[17 - i - 1]));
                }
            }

            // final = R16.L16
            Bits final = rBlocks[16].Concatenate(lBlocks[16]);

            return Permutate(final, m_IP1);
        }

        public Bits Function(Bits data, Bits key)
        {
            //Console.WriteLine("R: {0}", data.ToString());

            Bits expanded = Permutate(data, m_E);

            //Console.WriteLine("E(R): {0}", expanded.ToString(6));

            Bits output = key.Xor(expanded);

            //Console.WriteLine("k^E(R): {0}", output.ToString(6));

            // Do S-Box stuff
            // use multiplication instead of multi-demensional array
            int rowLength = 16;
            Bits result = new Bits(32);
            for (int i = 0; i < 8; i++)
            {
                int k = i * 6;
                int y = ((output[k + 0] ? 0x01 : 0x00) << 1)
                    + ((output[k + 5] ? 0x01 : 0x00) << 0);
                int x = ((output[k + 1] ? 0x01 : 0x00) << 3)
                    + ((output[k + 2] ? 0x01 : 0x00) << 2)
                    + ((output[k + 3] ? 0x01 : 0x00) << 1)
                    + ((output[k + 4] ? 0x01 : 0x00) << 0);
                
                int z = m_S[i][x + (rowLength * y)];
                
                // load bits into result
                for (int j = 0; j < 4; j++)
                {
                    result[(i * 4) + (4-j-1)] = ((z >> j) & 0x01) == 0x01;
                }
            }

            result = Permutate(result, m_P);

            //Console.WriteLine("{0}", result);

            return result;
        }

        public Bits Permutate(Bits input, int[] permutate)
        {
            Bits permutation = new Bits(permutate.Length);
            for (int i = 0; i < permutate.Length; i++)
            {
                permutation[i] = input[permutate[i] - 1];
            }
            return permutation;
        }

        public Bits[] GenerateSubKeys()
        {
            if (m_key.Length != 64)
            {
                throw new Exception(string.Format("Key is {0}, it should be 64", m_key.Length));
            }
            if (m_PC1.Length != 56)
            {
                throw new Exception(string.Format("PC1 is {0}, it should be 56", m_PC1.Length));
            }
            if (m_PC2.Length != 48)
            {
                throw new Exception(string.Format("PC2 is {0}, it should be 48", m_PC2.Length));
            }

            // permutate key using PC1
            Bits permutation = Permutate(m_key, m_PC1);

            Bits[] cBlocks = new Bits[17];
            Bits[] dBlocks = new Bits[17];

            // split permutated key into two halves (c0 and d0)
            Tuple<Bits, Bits> split = permutation.Split();
            cBlocks[0] = split.Item1;
            dBlocks[0] = split.Item2;

            // shift to generate blocks
            for (int i = 1; i < 17; i++)
            {
                cBlocks[i] = cBlocks[i-1].RotateLeft(m_shifts[i-1]);
                dBlocks[i] = dBlocks[i-1].RotateLeft(m_shifts[i-1]);
            }

            // permutate to generate keys using PC2
            Bits[] keys = new Bits[16];
            for (int i = 0; i < 16; i++)
            {
                Bits cd = cBlocks[i + 1].Concatenate(dBlocks[i + 1]);

                keys[i] = Permutate(cd, m_PC2);
            }
            
            //Console.WriteLine("SubKeys");
            //foreach (Bits key in keys)
            //{
            //    Console.WriteLine("{0}", key.ToString(6));
            //}

            return keys;
        }
    }
}
