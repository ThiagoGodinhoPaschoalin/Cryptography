namespace _ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            MicrosoftDocumentation.Asymmetric.MicrosoftRsaProvider rsaProvider = new MicrosoftDocumentation.Asymmetric.MicrosoftRsaProvider();
            MicrosoftDocumentation.Symmetric.MicrosoftRijndaelManaged rijndaelManaged = new MicrosoftDocumentation.Symmetric.MicrosoftRijndaelManaged();
            MicrosoftDocumentation.Symmetric.MicrosoftAesManaged aesManaged = new MicrosoftDocumentation.Symmetric.MicrosoftAesManaged();
            MicrosoftDocumentation.Hash.MicrosoftDocs Hashs = new MicrosoftDocumentation.Hash.MicrosoftDocs();
        }
    }
}
