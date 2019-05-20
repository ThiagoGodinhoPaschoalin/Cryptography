using System;
using System.Security.Cryptography;
using System.Text;

namespace RsaLibrary
{
    public class Helper
    {
        public (string, string) KeyGen(int bitSize = 2048)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(bitSize))
            {
                var publicKey = RSA.ExportParameters(false);
                var privateKey = RSA.ExportParameters(true);

                return (privateKey.FromRsaParametersXML(), publicKey.FromRsaParametersXML());
            }
        }

        public string Encrypt(string publicKey, string plainText, int bitSize = 2048)
        {
            RSACryptoServiceProvider RSA = default;
            string base64Cypher = string.Empty;

            try
            {
                RSAParameters _key = publicKey.ToRsaParametersXML();
                byte[] _plainBytes = Encoding.Default.GetBytes(plainText);

                RSA = new RSACryptoServiceProvider(bitSize);
                RSA.ImportParameters(_key);

                byte[] _bytesCypher = RSA.Encrypt(_plainBytes, false);

                base64Cypher = Convert.ToBase64String(_bytesCypher);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encrypt FAIL: {0}", ex.Message);
            }
            finally
            {
                RSA.Dispose();
            }

            return base64Cypher;
        }

        public string Decrypt(string privateKey, string base64Cypher, int bitSize = 2048)
        {
            RSACryptoServiceProvider RSA = default;
            string plainText = string.Empty;

            try
            {
                RSAParameters _key = privateKey.ToRsaParametersXML();
                byte[] _bytesCypher = Convert.FromBase64String(base64Cypher);

                RSA = new RSACryptoServiceProvider(bitSize);
                RSA.ImportParameters(_key);
                var _payload = RSA.Decrypt(_bytesCypher, false);

                plainText = Encoding.Default.GetString(_payload);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decrypt FAIL: {0}", ex.Message);
            }
            finally
            {
                RSA.Dispose();
            }

            return plainText;
        }

        public string GeneratingSignature(string privateKey, string base64Cypher, int bitSize = 2048, string hashType = "SHA1")
        {
            RSACryptoServiceProvider RSA = default;
            string base64Signature = string.Empty;

            try
            {
                RSAParameters _key = privateKey.ToRsaParametersXML();
                byte[] _bytesCypher = Convert.FromBase64String(base64Cypher);

                RSA = new RSACryptoServiceProvider(bitSize);
                RSA.ImportParameters(_key);

                RSAPKCS1SignatureFormatter RsaFormatter = new RSAPKCS1SignatureFormatter();
                RsaFormatter.SetKey(RSA);
                RsaFormatter.SetHashAlgorithm(hashType);

                byte[] bytesSignature = RsaFormatter.CreateSignature(_bytesCypher);

                base64Signature = Convert.ToBase64String(bytesSignature);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GeneratingSignature FAIL: {0}", ex.Message);
            }
            finally
            {
                RSA.Dispose();
            }

            return base64Signature;
        }

        public bool VerifyingSignature(string publicKey, string base64Signature, string base64Cypher, int bitSize = 2048, string hashType = "SHA1")
        {
            RSACryptoServiceProvider RSA = default;
            bool hasVerify = false;

            try
            {
                RSAParameters _key = publicKey.ToRsaParametersXML();

                byte[] _bytesCypher = Convert.FromBase64String(base64Cypher);
                byte[] _bytesSignature = Convert.FromBase64String(base64Signature);

                RSA = new RSACryptoServiceProvider(bitSize);
                RSA.ImportParameters(_key);

                RSAPKCS1SignatureDeformatter RsaDeformatter = new RSAPKCS1SignatureDeformatter();
                RsaDeformatter.SetKey(RSA);
                RsaDeformatter.SetHashAlgorithm(hashType);

                hasVerify = RsaDeformatter.VerifySignature(_bytesCypher, _bytesSignature);
            }
            catch (Exception ex)
            {
                Console.WriteLine("VerifyingSignature FAIL: {0}", ex.Message);
            }
            finally
            {
                RSA.Dispose();
            }

            return hasVerify;
        }
    }
}