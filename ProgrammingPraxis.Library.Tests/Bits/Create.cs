using NUnit.Framework;

namespace ProgrammingPraxis.Library.Tests.Bits
{
    [TestFixture]
    public class Create
    {
        [Test]
        public void FromLongSucceeds()
        {
            bool[] expected = new bool[]
            {
                false, false, false, true,  // 1
                false, false, true, true,   // 3
                false, false, true, true,   // 3
                false, true, false, false,  // 4
                false, true, false, true,   // 5
                false, true, true, true,    // 7
                false, true, true, true,    // 7
                true, false, false, true,   // 9
                true, false, false, true,   // 9
                true, false, true, true,    // B
                true, false, true, true,    // B
                true, true, false, false,   // C
                true, true, false, true,    // D
                true, true, true, true,     // F
                true, true, true, true,     // F
                false, false, false, true,  // 1
            };

            // 0x133457799BBCDFF1
            long input = 0x133457799BBCDFF1;

            Library.Bits bits = Library.Bits.Create(input);

            Assert.That(bits.Length, Is.EqualTo(64));
            for (int i = 0; i < 64; i++)
            {
                Assert.That(bits[i], Is.EqualTo(expected[i]));
            }
        }

        [Test]
        public void FromStringSucceeds()
        {
            bool[] expected = new bool[]
            {
                false, false, false, false, // 0
                false, false, false, true,  // 1
                false, false, true, false,  // 2
                false, false, true, true,   // 3
                false, true, false, false,  // 4
                false, true, false, true,   // 5
                false, true, true, false,   // 6
                false, true, true, true,    // 7
                true, false, false, false,  // 8
                true, false, false, true,   // 9
                true, false, true, false,   // A
                true, false, true, true,    // B
                true, true, false, false,   // C
                true, true, false, true,    // D
                true, true, true, false,    // E
                true, true, true, true,     // F
            };

            // 0x0123456789ABCDEF
            string input = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", (char)0x01, (char)0x23, (char)0x45, (char)0x67, (char)0x89, (char)0xAB, (char)0xCD, (char)0xEF);

            Library.Bits bits = Library.Bits.Create(input);

            Assert.That(bits.Length, Is.EqualTo(64));
            for (int i = 0; i < 64; i++)
            {
                Assert.That(bits[i], Is.EqualTo(expected[i]));
            }
        }
    }
}
