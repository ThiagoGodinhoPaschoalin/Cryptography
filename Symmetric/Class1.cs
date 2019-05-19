using System;
using System.Security.Cryptography;

namespace Symmetric
{
    public class Class1
    {
        private void Teste()
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.GenerateIV();
            tdes.GenerateKey();
        }
    }
}
