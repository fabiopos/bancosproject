using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancosProject.Data;

namespace BancosProject.Business.Interfaz
{
    public interface ICsvFile
    {
        void WriteFile(List<ClientesBanco> Customers, string filePath);
        void WriteFile(string data, string filePath);
        void WriteFile(byte[] data, string filePath);
        string ReadFile(string filePath);
        byte[] ReadFileData(string filePath);
        string[] ReadFileLines(string filePath);
        void RunProcess(string filePath, string publicKeyPath, string destinyPath, FtpEntity ftp);
        void RunProcessSftp(string publicKeyPath, FtpEntity ftp);
    }
}
