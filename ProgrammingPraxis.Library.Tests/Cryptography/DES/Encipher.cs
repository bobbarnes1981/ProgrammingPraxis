using NUnit.Framework;
using System.IO;

namespace ProgrammingPraxis.Library.Tests.Cryptography.DES
{
    [TestFixture]
    public class Encipher
    {
        [Test]
        public void EncipherECBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.ECB);

            MemoryStream input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            MemoryStream output = new MemoryStream();
            des.Encipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("85e813540f0ab40585e813540f0ab405"));
        }

        [Test]
        public void EncipherCBCSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.CBC);

            MemoryStream input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            MemoryStream output = new MemoryStream();
            des.Encipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("85e813540f0ab405eb46291166493cd4"));
        }

        [Test]
        public void EncipherCFBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.CFB);

            MemoryStream input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            MemoryStream output = new MemoryStream();
            des.Encipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("95a9069e0328829104b6a2150abce318"));
        }

        [Test]
        public void EncipherOFBSucceeds()
        {
            Library.Cryptography.DES des = new Library.Cryptography.DES(0x133457799BBCDFF1, Library.Cryptography.DESMode.OFB);

            MemoryStream input = new MemoryStream(new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
            MemoryStream output = new MemoryStream();
            des.Encipher(input, output);

            Assert.That(output.GetHexString(), Is.EqualTo("95a9069e032882913fdaf9b0d7c2a81d"));
        }
    }
}
