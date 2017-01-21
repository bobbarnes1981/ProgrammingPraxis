namespace ProgrammingPraxis.Library.Cryptography
{
    interface IStringCipher
    {
        string Encipher(string message);
        string Decipher(string code);
    }
}
