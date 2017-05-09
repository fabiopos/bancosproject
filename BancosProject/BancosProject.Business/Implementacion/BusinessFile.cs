using BancosProject.Business.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancosProject.Data;

namespace BancosProject.Business.Implementacion
{
    public class BusinessFile : ICsvFile
    {
        ICustomer _customer;
        ICustomCriptography _crypto;
        IFileTransfer _fileTransfer;
        public BusinessFile() { }
        public BusinessFile(ICustomer customer, ICustomCriptography criptoUtil, IFileTransfer fileTransferUtil)
        {
            _customer = customer;
            _crypto = criptoUtil;
            _fileTransfer = fileTransferUtil;
                     
        }

        public void WriteFile(string data, string filePath)
        {
            System.IO.File.WriteAllText(filePath, data);
        }

        public void WriteFile(byte[]data, string filePath)
        {
            System.IO.File.WriteAllBytes(filePath, data);
        }

        public byte[] GetBytes(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);
        }
        
        public void WriteFile(List<ClientesBanco> Customers, string filePath)
        {
            using (System.IO.StreamWriter file =  new System.IO.StreamWriter(@filePath))
            {
                foreach (ClientesBanco item in Customers)                
                    file.WriteLine("{0};{1};{2};{3}", item.Id, item.Name, item.LastName, item.IsValid);                
            }
        }

        public string []  GetLines(List<ClientesBanco> Customers)
        {
            List<string> sb = new List<string>();
                foreach (ClientesBanco item in Customers)
                    sb.Add(string.Format("{0};{1};{2};{3}", item.Id, item.Name, item.LastName, item.IsValid));

            return sb.ToArray();
          
        }

        public string ReadFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
        public string [] ReadFileLines(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }

        public byte[] ReadFileData(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);
        }
        public void RunProcess(string filePath, string publicKeyPath, string encriptedFilePath, FtpEntity ftp)
        {
            var customers = _customer.GetCustomers();
            WriteFile(customers, filePath);
            _crypto.Crypt(publicKeyPath, filePath, encriptedFilePath);
            _fileTransfer.TransferFileToFtp(encriptedFilePath, ftp);
        }
        public void RunProcess(string publicXmlKey, FtpEntity ftp)
        {
            var customers = _customer.GetCustomers();
            var lines = GetLines(customers);
            var encriptedText = _crypto.GetEncriptedText(publicXmlKey, lines);
            _fileTransfer.TrasnferInfoToFtp(ftp, encriptedText);
        }

        public void RunProcessSftp(string publicXmlKey, FtpEntity ftp)
        {
            var customers = _customer.GetCustomers();
            var lines = GetLines(customers);
            var encriptedText = _crypto.GetEncriptedText(publicXmlKey, lines);
            _fileTransfer.TrasnferInfoToSFtp(ftp, encriptedText);
        }
    }
}
