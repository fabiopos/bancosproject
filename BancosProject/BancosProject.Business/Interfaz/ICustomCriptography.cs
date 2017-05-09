using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancosProject.Business.Interfaz
{
    public interface ICustomCriptography
    {
        void GenerateKeys(string filePath);
        void Crypt(string publicKeyPath, string fileToCypher, string filePathDestiny);
        void Decrypt(string privateKeyPath, string fileToDecrypt, string filePathDestiny);

        string GetEncriptedText(string publicXmlKey, string [] textToEncript);
        
    }
    public class CriptoKeys
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }

    }
}
