using System.Security.Cryptography;

namespace MicrosoftDocumentation.Asymmetric
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/dotnet/standard/security/encrypting-data
    /// https://docs.microsoft.com/pt-br/dotnet/standard/security/decrypting-data
    /// </summary>
    public class MicrosoftDocs
    {
        //Create values to store encrypted symmetric keys.  
        private byte[] encryptedSymmetricKey;
        private byte[] encryptedSymmetricIV;

        private byte[] decryptedSymmetricKey;
        private byte[] decryptedSymmetricIV;

        public void EncryptingData()
        {
            //Initialize the byte arrays to the public key information.  
            byte[] publicKey = {214,46,220,83,160,73,40,39,201,155,19,202,3,11,191,178,56,
            74,90,36,248,103,18,144,170,163,145,87,54,61,34,220,222,
            207,137,149,173,14,92,120,206,222,158,28,40,24,30,16,175,
            108,128,35,230,118,40,121,113,125,216,130,11,24,90,48,194,
            240,105,44,76,34,57,249,228,125,80,38,9,136,29,117,207,139,
            168,181,85,137,126,10,126,242,120,247,121,8,100,12,201,171,
            38,226,193,180,190,117,177,87,143,242,213,11,44,180,113,93,
            106,99,179,68,175,211,164,116,64,148,226,254,172,147};

            byte[] exponent = { 1, 0, 1 };

            //Create a new instance of the RSACryptoServiceProvider class.  
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            //Create a new instance of the RSAParameters structure.  
            RSAParameters rsaKeyInfo = new RSAParameters();

            //Set rsaKeyInfo to the public key values.   
            rsaKeyInfo.Modulus = publicKey;
            rsaKeyInfo.Exponent = exponent;

            //Import key parameters into RSA.  
            rsa.ImportParameters(rsaKeyInfo);

            //Create a new instance of the RijndaelManaged class.  
            RijndaelManaged rm = new RijndaelManaged();

            //Encrypt the symmetric key and IV.  
            encryptedSymmetricKey = rsa.Encrypt(rm.Key, false);
            encryptedSymmetricIV = rsa.Encrypt(rm.IV, false);
        }

        public void DecryptingData()
        {
            //Create a new instance of the RSACryptoServiceProvider class.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            // Export the public key information and send it to a third party.
            // Wait for the third party to encrypt some data and send it back.

            //Decrypt the symmetric key and IV.
            decryptedSymmetricKey = rsa.Decrypt(encryptedSymmetricKey, false);
            decryptedSymmetricIV = rsa.Decrypt(encryptedSymmetricIV, false);
        }
    }
}
