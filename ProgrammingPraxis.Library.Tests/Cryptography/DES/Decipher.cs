using NUnit.Framework;
using System.IO;

namespace ProgrammingPraxis.Library.Tests.Cryptography.DES
{
    [TestFixture]
    public class Decipher
    {
        [Test]
        public void DecipherECBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.ECB);

            MemoryStream input = new MemoryStream(new byte[] { 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05, 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05 });
            MemoryStream output = new MemoryStream();
            des.Decipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("0123456789abcdef0123456789abcdef"));
        }

        [Test]
        public void DecipherCBCSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.CBC);

            MemoryStream input = new MemoryStream(new byte[] { 0x85, 0xE8, 0x13, 0x54, 0x0F, 0x0A, 0xB4, 0x05, 0xeb, 0x46, 0x29, 0x11, 0x66, 0x49, 0x3c, 0xd4 });
            MemoryStream output = new MemoryStream();
            des.Decipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("0123456789abcdef0123456789abcdef"));
        }

        [Test]
        public void DecipherCFBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.CFB);

            MemoryStream input = new MemoryStream(new byte[] { 0x95, 0xa9, 0x06, 0x9e, 0x03, 0x28, 0x82, 0x91, 0x04, 0xb6, 0xa2, 0x15, 0x0a, 0xbc, 0xe3, 0x18 });
            MemoryStream output = new MemoryStream();
            des.Decipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("0123456789abcdef0123456789abcdef"));
        }

        [Test]
        public void DecipherOFBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.OFB);

            MemoryStream input = new MemoryStream(new byte[] { 0x95, 0xa9, 0x06, 0x9e, 0x03, 0x28, 0x82, 0x91, 0x3f, 0xda, 0xf9, 0xb0, 0xd7, 0xc2, 0xa8, 0x1d });
            MemoryStream output = new MemoryStream();
            des.Decipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("0123456789abcdef0123456789abcdef"));
        }
    }
}
