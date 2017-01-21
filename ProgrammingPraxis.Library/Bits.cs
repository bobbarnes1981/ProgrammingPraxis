using System;
using System.Text;

namespace ProgrammingPraxis.Library
{
    public class Bits
    {
        private bool[] m_bits;

        public Bits(int length)
        {
            m_bits = new bool[length];
            for (int i = 0; i < m_bits.Length; i++)
            {
                m_bits[i] = false;
            }
        }

        public Bits(Bits bits)
        {
            m_bits = new bool[bits.Length];
            for (int i = 0; i < bits.Length; i++)
            {
                m_bits[i] = bits[i];
            }
        }

        public bool this[int index]
        {
            get
            {
                return m_bits[index];
            }

            set
            {
                m_bits[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return m_bits.Length;
            }
        }
        
        public Bits Concatenate(Bits other)
        {
            Bits output = new Bits(Length + other.Length);
            for (int i = 0; i < Length; i++)
            {
                output[i] = this[i];
            }
            for (int j = 0; j < other.Length; j++)
            {
                output[Length + j] = other[j];
            }
            return output;
        }

        public Bits RotateLeft(int bits)
        {
            //validate positive number

            Bits output = new Bits(this);
            for (int j = 0; j < bits; j++)
            {
                bool first = output[0];
                for (int i = 1; i < output.Length; i++)
                {
                    output[i - 1] = output[i];
                }

                output[output.Length - 1] = first;
            }

            return output;
        }

        public Bits Xor(Bits other)
        {
            // validate same length

            Bits output = new Bits(Length);
            for (int i = 0; i < Length; i++)
            {
                output[i] = this[i] ^ other[i];
            }

            return output;
        }

        public Tuple<Bits, Bits> Split()
        {
            // TODO: check that is even
            int half = Length / 2;

            Bits l = new Bits(half);
            Bits r = new Bits(half);
            for (int i = 0; i < half; i++)
            {
                l[i] = this[i];
                r[i] = this[i + half];
            }

            return new Tuple<Bits, Bits>(l, r);
        }

        public byte[] ToByteArray()
        {
            byte[] output = new byte[m_bits.Length / 8];
            for (int i = 0; i < output.Length; i ++)
            {
                byte b = 0x00;
                for (int j = 0; j < 8; j++)
                {
                    b |= (byte)((this[(i*8) + j] ? 0x01 : 0x00) << (8 - j - 1));
                }
                output[i] = b;
            }
            return output;
        }

        public string ToHexString()
        {
            StringBuilder output = new StringBuilder(Length);
            for (int i = 0; i < m_bits.Length; i+=8)
            {
                byte b = 0x00;
                for (int j = 0; j < 8; j++)
                {
                    b |= (byte)((this[i+j] ? 0x01 : 0x00) << (8 - j - 1));
                }
                output.AppendFormat("{0:x2}", b);
            }
            return output.ToString();
        }

        public override string ToString()
        {
            return ToString(4);
        }

        public string ToString(int grouping)
        {
            StringBuilder output = new StringBuilder();
            int counter = 0;
            foreach (bool bit in m_bits)
            {
                output.Append(bit ? '1' : '0');
                counter++;
                if (counter > grouping - 1)
                {
                    output.Append(' ');
                    counter = 0;
                }
            }
            return output.ToString();
        }

        public Bits[] Blocks(int blockSize)
        {
            Bits[] blocks = new Bits[Length / blockSize];
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = new Bits(blockSize);
                for (int j = 0; j < blockSize; j++)
                {
                    blocks[i][j] = this[(i * blockSize) + j];
                }
            }
            return blocks;
        }

        public static Bits Create(long input)
        {
            int length = 64;
            Bits bits = new Bits(length);
            for (int i = 0; i < length; i++)
            {
                bits[length - i - 1] = ((input >> i) & 0x00000001) == 0x00000001;
            }
            return bits;
        }

        public static Bits Create(string input)
        {
            int bitsPerByte = 8;
            Bits block = new Bits(input.Length * bitsPerByte);
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                for (int j = 0; j < bitsPerByte; j++)
                {
                    int realBit = (i * bitsPerByte) + bitsPerByte - j - 1;
                    int charBit = bitsPerByte - j - 1;
                    bool bit = ((c >> j) & 0x01) == 0x01;
                    block[realBit] = bit;
                }
            }
            return block;
        }

        public static Bits Create(params byte[] input)
        {
            int bitsPerByte = 8;
            Bits block = new Bits(input.Length * bitsPerByte);
            for (int i = 0; i < input.Length; i++)
            {
                byte c = input[i];
                for (int j = 0; j < bitsPerByte; j++)
                {
                    int realBit = (i * bitsPerByte) + bitsPerByte - j - 1;
                    int charBit = bitsPerByte - j - 1;
                    bool bit = ((c >> j) & 0x01) == 0x01;
                    block[realBit] = bit;
                }
            }
            return block;
        }
    }
}
