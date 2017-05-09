using BancosProject.Business.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BancosProject.Business.Implementacion
{
    public class Cifrado : ICustomCriptography
    {
        RSACryptoServiceProvider _rsa;
        BusinessFile _bFile;
        private int bytesTo;

        public Cifrado(int byteSize)
        {
            bytesTo = byteSize;
            _rsa = new RSACryptoServiceProvider(bytesTo);
            _bFile = new BusinessFile();
        }

        public void Crypt(string publicKeyPath, string fileToCypher, string filePathDestiny)
        {
            string cypherText = GetEncryptText(publicKeyPath, fileToCypher);
            _bFile.WriteFile(cypherText, filePathDestiny);

        }

        public void Decrypt(string privateKeyPath, string fileToDecrypt, string filePathDestiny)
        {
            string decryptText = GetDecryptText(privateKeyPath, fileToDecrypt);
            _bFile.WriteFile(decryptText, filePathDestiny);
        }

        public void GenerateKeys(string filePath)
        {
            var keys = SetKeys();
            _bFile.WriteFile(keys.PublicKey, filePath+"\\public.xml");
            _bFile.WriteFile(keys.PrivateKey, filePath + "\\private.xml");
        }

        private CriptoKeys SetKeys()
        {

            var privKey = _rsa.ToXmlString(true);
            var pubKey = _rsa.ToXmlString(false);

            return new CriptoKeys
            {
                PublicKey = pubKey,
                PrivateKey = privKey
            };

        }

        private string GetXmlString(RSAParameters key)
        {
            var sw = new System.IO.StringWriter();
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, key);
            return sw.ToString();
        }

        private string GetEncryptText(string publicKeyPath, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            var pubKey = _bFile.ReadFile(publicKeyPath);
            var rsa = new RSACryptoServiceProvider(bytesTo);            
            rsa.FromXmlString(pubKey);

            var lines = _bFile.ReadFileLines(filePath);
            foreach (var line in lines)
            {
                var data = Encoding.ASCII.GetBytes(line);
                var bytesCypherText = rsa.Encrypt(data, false);
                sb.AppendLine(Convert.ToBase64String(bytesCypherText));
            }
            
            return sb.ToString();
        }

        private string GetDecryptText(string privKeyPath, string filePathToDecrypt)
        {
            StringBuilder sb = new StringBuilder();
            var privKey = _bFile.ReadFile(privKeyPath);
            var rsa = new RSACryptoServiceProvider(bytesTo);
            rsa.FromXmlString(privKey);

            var lines = _bFile.ReadFileLines(filePathToDecrypt);
            foreach (var line in lines)
            {
                var dataDecrypted = rsa.Decrypt(Convert.FromBase64String(line), false);
                sb.AppendLine(Encoding.ASCII.GetString(dataDecrypted));
            }

            return sb.ToString();
        }
      
        private RSAParameters GetKeyObject(string keyPath)
        {
            string pubKeyString = _bFile.ReadFile(keyPath);
            var sr = new System.IO.StringReader(pubKeyString);
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            return (RSAParameters)xs.Deserialize(sr);
        }

        public string GetEncriptedText(string publicXmlKey, string [] lines)
        {
            StringBuilder sb = new StringBuilder();
            var pubKey = publicXmlKey;
            var rsa = new RSACryptoServiceProvider(bytesTo);
            rsa.FromXmlString(pubKey);
            
            foreach (var line in lines)
            {
                var data = Encoding.ASCII.GetBytes(line);
                var bytesCypherText = rsa.Encrypt(data, false);
                sb.AppendLine(Convert.ToBase64String(bytesCypherText));
            }

            return sb.ToString();
        }
    }
}
