using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BancosProject.Business.Interfaz;
using BancosProject.Business.Implementacion;
using Renci.SshNet;

namespace BancosProject.UnitTest
{
    [TestClass]
    public class FTP
    {
        [TestMethod]
        public void SubirArchivoAlFtp()
        {
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "ftp://40.71.253.112/customers.txt" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.TransferFileToFtp(filePath, ftpInfo);

        }

        [TestMethod]
        public void SubirArchivoAlSFtp()
        {
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "sftp://40.71.253.112" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.TransferFileToFtp(filePath, ftpInfo);

        }

        [TestMethod]
        public void DeleteFilesFtp()
        {           
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "ftp://40.71.253.112/customers.txt" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.DeleteFileFromFtp(ftpInfo);

        }

        [TestMethod]
        public void UploadFilesSFtp()
        {
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.TransferFileToSFtp(filePath, ftpInfo);
        }

        [TestMethod]
        public void UploadFilesSFtpByText()
        {

            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.TrasnferInfoToSFtp(ftpInfo,"texto de prueba");
        }

        [TestMethod]
        public void DeleteFilesSFtp()
        {
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            IFileTransfer _ftp = new FtpUtil();
            _ftp.DeleteFileFromSFtp(ftpInfo);
        }
        private void uploadSftp()
        {
            var connectionInfo = new ConnectionInfo("40.71.253.112",
                                        "developer",
                                        new PasswordAuthenticationMethod("developer", "Developer05*")
                                        );

            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            using (var sftp = new SftpClient(connectionInfo))
            {
                sftp.Connect();
                using (var uplfileStream = System.IO.File.OpenRead(filePath))
                {
                   
                    sftp.UploadFile(uplfileStream, "customersSftp.txt", true);
                }
                sftp.Disconnect();

            }
        }

        public void DeleteFile()
        {
            var connectionInfo = new ConnectionInfo("40.71.253.112",
                                      "developer",
                                      new PasswordAuthenticationMethod("developer", "Developer05*")
                                      );

            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            using (var sftp = new SftpClient(connectionInfo))
            {
                sftp.Connect();
                using (var uplfileStream = System.IO.File.OpenRead(filePath))
                {                    
                    sftp.DeleteFile("customersSftp.txt");
                }
                sftp.Disconnect();

            }

        }
    }
}
