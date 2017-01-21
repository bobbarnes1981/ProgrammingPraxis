using System.IO;

namespace ProgrammingPraxis.Library.Cryptography
{
    interface IStreamCipher
    {
        void Encipher(Stream input, Stream output);
        void Decipher(Stream input, Stream output);
    }
}
