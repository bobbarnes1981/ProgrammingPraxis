using System.Text;

namespace ProgrammingPraxis.Library.Cryptography
{
    public class ROT13 : IStringCipher
    {
        private char m_offset = (char)13;
        
        public string Cipher(string input)
        {
            StringBuilder output = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                char o = c;
                if (c >= 'a' && c <= 'z')
                {
                    o = (char)(c + m_offset);
                    if (o > 'z')
                    {
                        o -= (char)26;
                    }
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    o = (char)(c + m_offset);
                    if (o > 'Z')
                    {
                        o -= (char)26;
                    }
                }
                output.Append(o);
            }

            return output.ToString();
        }

        public string Encipher(string message)
        {
            return Cipher(message);
        }

        public string Decipher(string code)
        {
            return Cipher(code);
        }
    }
}
