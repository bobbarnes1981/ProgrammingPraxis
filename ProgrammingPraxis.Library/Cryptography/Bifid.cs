using System;
using System.Text;

namespace ProgrammingPraxis.Library.Cryptography
{
    public class Bifid : IStringCipher
    {
        // Polybius square
        private char[,] polybius = new char[,]
        {
            { 'A', 'B', 'C', 'D', 'E' },
            { 'F', 'G', 'H', 'I', 'K' },
            { 'L', 'M', 'N', 'O', 'P' },
            { 'Q', 'R', 'S', 'T', 'U' },
            { 'V', 'W', 'X', 'Y', 'Z' },
        };

        // Get tuple for character
        public Tuple<int, int> GetTuple(char c)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (polybius[x, y] == c)
                    {
                        return new Tuple<int, int>(x, y);
                    }
                }
            }

            throw new Exception(string.Format("Unhandled character {0}", c));
        }

        public string Encipher(string message)
        {
            int[] intermediate = new int[message.Length * 2];

            for (int i = 0; i < message.Length; i++)
            {
                Tuple<int, int> t = GetTuple(message[i]);
                intermediate[i] = t.Item1;
                intermediate[i + (intermediate.Length / 2)] = t.Item2;
            }

            StringBuilder output = new StringBuilder(message.Length);
            for (int j = 0; j < intermediate.Length; j += 2)
            {
                output.Append(polybius[intermediate[j], intermediate[j + 1]]);
            }

            return output.ToString();
        }

        public string Decipher(string code)
        {
            int[] intermediate = new int[code.Length * 2];

            for (int i = 0; i < code.Length; i++)
            {
                Tuple<int, int> t = GetTuple(code[i]);
                intermediate[i * 2] = t.Item1;
                intermediate[(i * 2) + 1] = t.Item2;
            }

            StringBuilder output = new StringBuilder(code.Length);
            for (int j = 0; j < intermediate.Length / 2; j++)
            {
                output.Append(polybius[intermediate[j], intermediate[j + (intermediate.Length / 2)]]);
            }

            return output.ToString();
        }
    }
}
