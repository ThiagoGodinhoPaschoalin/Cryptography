using System;
using System.Security.Cryptography;

namespace MicrosoftDocumentation.Asymmetric
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/dotnet/standard/security/cryptographic-signatures
    /// </summary>
    public class MicrosoftSignatures
    {
        //The hash value to sign.
        byte[] hashValue = { 59, 4, 248, 102, 77, 97, 142, 201, 210, 12, 224, 93, 25, 41, 100, 197, 213, 134, 130, 135 };

        //The value to hold the signed value.
        byte[] signedHashValue;

        public void Generating(RSAParameters privateParams)
        {
            //Generate a public/private key pair.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(privateParams);

            //Create an RSAPKCS1SignatureFormatter object and pass it the
            //RSACryptoServiceProvider to transfer the private key.
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);

            //Set the hash algorithm to SHA256.
            rsaFormatter.SetHashAlgorithm("SHA256");

            //Create a signature for hashValue and assign it to
            //signedHashValue.
            signedHashValue = rsaFormatter.CreateSignature(hashValue);
        }

        public void Verifying(RSAParameters publicParams)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(publicParams);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm("SHA256");
            if (rsaDeformatter.VerifySignature(hashValue, signedHashValue))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }
        }
    }
}
