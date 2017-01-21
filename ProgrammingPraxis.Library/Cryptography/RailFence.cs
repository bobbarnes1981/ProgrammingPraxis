using System.Text;

namespace ProgrammingPraxis.Library.Cryptography
{
    public class RailFence : IStringCipher
    {
        private int m_key;

        public RailFence(int key)
        {
            m_key = key;
        }

        public string Encipher(string message)
        {
            int a = m_key + m_key - 2;
            int b = 0;
            StringBuilder output = new StringBuilder(message.Length);
            for (int i = 0; i < m_key; i++)
            {
                int j = i;
                do
                {
                    if (a != 0)
                    {
                        if (j < message.Length)
                        {
                            output.Append(message[j]);
                        }
                    }
                    j += a;

                    if (b != 0)
                    {
                        if (j < message.Length)
                        {
                            output.Append(message[j]);
                        }
                    }
                    j += b;
                } while (j < message.Length);
                a -= 2;
                b += 2;
            }

            return output.ToString();
        }

        public string Decipher(string code)
        {
            int a = m_key + m_key - 2;
            int b = 0;
            char[] output = new char[code.Length];
            int k = 0;
            for (int i = 0; i < m_key; i++)
            {
                int j = i;
                do
                {
                    if (a != 0)
                    {
                        if (j < code.Length)
                        {
                            output[j] = code[k];
                            k++;
                        }
                    }
                    j += a;

                    if (b != 0)
                    {
                        if (j < code.Length)
                        {
                            output[j] = code[k];
                            k++;
                        }
                    }
                    j += b;
                } while (j < code.Length);
                a -= 2;
                b += 2;
            }

            return string.Join("", output);
        }
    }
}
