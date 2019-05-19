using System;
using System.Security.Cryptography;

namespace Asymmetric
{
    public class Class1
    {
        public void teste()
        {
            RSACryptoServiceProvider rSA = new RSACryptoServiceProvider();
            RSAParameters rsaKeyInfoP = rSA.ExportParameters(false);
            RSAParameters rsaKeyInfoPP = rSA.ExportParameters(true);
        }
    }
}
