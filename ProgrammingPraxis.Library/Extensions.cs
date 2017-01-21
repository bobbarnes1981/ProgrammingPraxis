using System.IO;
using System.Text;

namespace ProgrammingPraxis.Library
{
    public static class Extensions
    {
        public static string GetHexString(this Stream stream)
        {
            StringBuilder output = new StringBuilder();
            stream.Position = 0;
            byte[] bytes = new byte[8];
            do
            {
                int a = stream.Read(bytes, 0, 8);
                if (a == 0)
                {
                    break;
                }
                foreach (byte b in bytes)
                {
                    output.AppendFormat("{0:x2}", b);
                }
            } while (true);
            stream.Position = 0;
            return output.ToString();
        }
    }
}
