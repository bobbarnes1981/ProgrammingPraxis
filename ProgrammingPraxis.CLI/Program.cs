using ProgrammingPraxis.Library;
using ProgrammingPraxis.Library.Cryptography;
using System;
using System.IO;
using System.Text;

namespace ProgrammingPraxis.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new Bifid().Encipher("PROGRAMMINGPRAXIS"));
            //Console.WriteLine(new Bifid().Decipher("OMQNHHQWUIGBIMWCS"));

            //Console.WriteLine(new ROT13().Encipher("Programming Praxis is fun!"));
            //Console.WriteLine(new ROT13().Decipher("Cebtenzzvat Cenkvf vf sha!"));

            //Console.WriteLine(new RailFence(4).Encipher("PROGRAMMING PRAXIS"));
            //Console.WriteLine(new RailFence(4).Decipher("PMPRAM RSORIGAIGNX"));

            //Console.WriteLine(new RailFence(5).Encipher("PROGRAMMING PRAXIS"));
            //Console.WriteLine(new RailFence(5).Decipher("PIIRMNXSOMGAGA RRP"));

            //MemoryStream input;
            //MemoryStream output = new MemoryStream();

            //input = new MemoryStream(Encoding.ASCII.GetBytes("ProgPraxProgPrax"));
            //DisplayStream(input);
            //new DES(0x0123456789ABCDEF, DESMode.ECB).Encipher(input, output);
            //DisplayStream(output);
            //input = new MemoryStream(new byte[] { 0xcc, 0x99, 0xea, 0x46, 0xb1, 0x6e, 0x28, 0x90, 0xcc, 0x99, 0xea, 0x46, 0xb1, 0x6e, 0x28, 0x90 });
            //DisplayStream(input);
            //new DES(0x0123456789ABCDEF, DESMode.ECB).Decipher(input, output);
            //DisplayStream(output);

            //input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.ECB).Encipher(input, output);
            //DisplayStream(output);
            //input = new MemoryStream(new byte[] { 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05, 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05 });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.ECB).Decipher(input, output);
            //DisplayStream(output);

            //input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.CBC).Encipher(input, output);
            //DisplayStream(output);
            //input = new MemoryStream(new byte[] { 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05, 0xeb, 0x46, 0x29, 0x11, 0x66, 0x49, 0x3c, 0xd4 });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.CBC).Decipher(input, output);
            //DisplayStream(output);

            //input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.CFB).Encipher(input, output);
            //DisplayStream(output);
            //input = new MemoryStream(new byte[] { 0x95, 0xa9, 0x06, 0x9e, 0x03, 0x28, 0x82, 0x91, 0x04, 0xb6, 0xa2, 0x15, 0x0a, 0xbc, 0xe3, 0x18 });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.CFB).Decipher(input, output);
            //DisplayStream(output);

            //input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.OFB).Encipher(input, output);
            //DisplayStream(output);
            //input = new MemoryStream(new byte[] { 0x95, 0xa9, 0x06, 0x9e, 0x03, 0x28, 0x82, 0x91, 0x3f, 0xda, 0xf9, 0xb0, 0xd7, 0xc2, 0xa8, 0x1d });
            //DisplayStream(input);
            //new DES(0x133457799BBCDFF1, DESMode.OFB).Decipher(input, output);
            //DisplayStream(output);

            using (FileStream inputStream = new FileStream("files/ofb_encrypt_input_file.txt", FileMode.Open))
            {
                using (FileStream outputStream = new FileStream("files/ofb_encrypt_output_file.txt", FileMode.CreateNew))
                {
                    new DES(0x133457799BBCDFF1, DESMode.OFB).Encipher(inputStream, outputStream);
                }
            }
            using (FileStream inputStream = new FileStream("files/ofb_decrypt_input_file.txt", FileMode.Open))
            {
                using (FileStream outputStream = new FileStream("files/ofb_decrypt_output_file.txt", FileMode.CreateNew))
                {
                    new DES(0x133457799BBCDFF1, DESMode.OFB).Decipher(inputStream, outputStream);
                }
            }
        }

        private static void DisplayStream(Stream stream)
        {
            Console.WriteLine(stream.GetHexString());
        }
    }
}
