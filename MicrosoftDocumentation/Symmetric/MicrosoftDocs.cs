using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;

namespace MicrosoftDocumentation.Symmetric
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/dotnet/standard/security/encrypting-data
    /// https://docs.microsoft.com/pt-br/dotnet/standard/security/decrypting-data
    /// </summary>
    public class MicrosoftDocs
    {
        //The key and IV must be the same values that were used
        //to encrypt the stream.
        //32 bytes ou 256 bits;
        private readonly byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private readonly byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public void EncryptingData()
        {
            try
            {
                //Create a TCP connection to a listening TCP process.  
                //Use "localhost" to specify the current computer or  
                //replace "localhost" with the IP address of the   
                //listening process.    
                TcpClient tcp = new TcpClient("localhost", 11000);

                //Create a network stream from the TCP connection.   
                NetworkStream netStream = tcp.GetStream();

                //Create a new instance of the RijndaelManaged class  
                // and encrypt the stream.  
                RijndaelManaged rmCrypto = new RijndaelManaged();

                //Create a CryptoStream, pass it the NetworkStream, and encrypt   
                //it with the Rijndael class.  
                CryptoStream cryptStream = new CryptoStream(netStream,
                rmCrypto.CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

                //Create a StreamWriter for easy writing to the   
                //network stream.  
                StreamWriter sWriter = new StreamWriter(cryptStream);

                //Write to the stream.  
                sWriter.WriteLine("Hello World!");

                //Inform the user that the message was written  
                //to the stream.  
                Console.WriteLine("The message was sent.");

                //Close all the connections.  
                sWriter.Close();
                cryptStream.Close();
                netStream.Close();
                tcp.Close();
            }
            catch
            {
                //Inform the user that an exception was raised.  
                Console.WriteLine("The connection failed.");
            }
        }

        public void DecryptingData()
        {
            try
            {
                //Initialize a TCPListener on port 11000
                //using the current IP address.
                TcpListener tcpListen = new TcpListener(IPAddress.Any, 11000);

                //Start the listener.
                tcpListen.Start();

                //Check for a connection every five seconds.
                while (!tcpListen.Pending())
                {
                    Console.WriteLine("Still listening. Will try in 5 seconds.");
                    Thread.Sleep(5000);
                }

                //Accept the client if one is found.
                TcpClient tcp = tcpListen.AcceptTcpClient();

                //Create a network stream from the connection.
                NetworkStream netStream = tcp.GetStream();

                //Create a new instance of the RijndaelManaged class
                // and decrypt the stream.
                RijndaelManaged rmCrypto = new RijndaelManaged();

                //Create a CryptoStream, pass it the NetworkStream, and decrypt
                //it with the Rijndael class using the key and IV.
                CryptoStream cryptStream = new CryptoStream(netStream,
                   rmCrypto.CreateDecryptor(key, iv),
                   CryptoStreamMode.Read);

                //Read the stream.
                StreamReader sReader = new StreamReader(cryptStream);

                //Display the message.
                Console.WriteLine("The decrypted original message: {0}", sReader.ReadToEnd());

                //Close the streams.
                sReader.Close();
                netStream.Close();
                tcp.Close();
            }
            //Catch any exceptions.
            catch
            {
                Console.WriteLine("The Listener Failed.");
            }
        }
    }
}
