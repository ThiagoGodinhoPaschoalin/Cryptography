using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MicrosoftDocumentation.Hash
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/dotnet/api/system.security.cryptography.sha256managed?view=netcore-2.1
    /// https://docs.microsoft.com/pt-br/dotnet/api/system.security.cryptography.sha512managed?view=netcore-2.1
    /// </summary>
    public class MicrosoftDocs
    {
        private string plainText = "A aplicação deve codificar este texto aqui!";
        private byte[] data;
        private byte[] result;

        public MicrosoftDocs(string plainText = null)
        {
            Console.WriteLine("\nHASHs:\n");

            this.plainText = plainText ?? this.plainText;
            UnicodeEncoding unicode = new UnicodeEncoding();
            data = unicode.GetBytes(this.plainText);

            Console.WriteLine("ASCII(PlainText): {0}", this.plainText);
            Console.WriteLine("ASCII(Length): {0}", this.plainText.Length);

            SHA256();
            SHA512();

            Console.WriteLine("\n-- / -- / --\n\n");
        }

        void SHA256()
        {
            SHA256 shaM = new SHA256Managed();
            result = shaM.ComputeHash(data);

            ConsoleResult("SHA256");

            shaM.Dispose();
        }

        void SHA512()
        {
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);

            ConsoleResult("SHA512");

            shaM.Dispose();
        }

        void ConsoleResult(string hashType)
        {
            Console.WriteLine("{0}:", hashType);
            Console.WriteLine("Byte(Hash): {0}", result);
            Console.WriteLine("Byte(Length): {0}", result.Length);
            Console.WriteLine("Hexadecimal(Hash): {0}", Byte2Hexa(result));
            Console.WriteLine("Hexadecimal(Length): {0}", Byte2Hexa(result).Length);
            Console.WriteLine("Base64(Hash): {0}", Convert.ToBase64String(result));
            Console.WriteLine("Base64(Length): {0}", Convert.ToBase64String(result).Length);
            Console.WriteLine("\n");
        }

        public string Byte2Hexa(byte[] array)
        {
            return string.Concat(array.Select(by => $"{by:X2}"));
        }

        public byte[] Hexa2Byte(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
