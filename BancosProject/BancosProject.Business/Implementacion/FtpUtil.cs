using BancosProject.Business.Interfaz;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BancosProject.Business.Implementacion
{
    public class FtpUtil : IFileTransfer
    {
        public bool DeleteFileFromFtp(FtpEntity ftp)
        {
            // Get the object used to communicate with the server.  
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.Uri);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftp.User, ftp.Password);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return true;
        }

        public bool TransferFileToFtp(string filePathSource, FtpEntity ftp)
        {
            // Get the object used to communicate with the server.  
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.Uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(ftp.User, ftp.Password);

            // Copy the contents of the file to the request stream.  
            StreamReader sourceStream = new StreamReader(filePathSource);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);            
            response.Close();
            return true;
        }

        public void TrasnferInfoToFtp(FtpEntity ftp, string text)
        {
            // Get the object used to communicate with the server.  
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp.Uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(ftp.User, ftp.Password);
            request.UseBinary = true;
            request.KeepAlive = false;
            byte[] messageContent = Encoding.ASCII.GetBytes(text);
            request.ContentLength = messageContent.Length;

            using (Stream s = request.GetRequestStream())
            {
                s.Write(messageContent, 0, messageContent.Length);
            }



        }



        public bool DeleteFileFromSFtp(FtpEntity ftp)
        {
            var connectionInfo = new ConnectionInfo(ftp.Host,
                                        ftp.User,
                                        new PasswordAuthenticationMethod(ftp.User, ftp.Password)
                                        );


            using (var sftp = new SftpClient(connectionInfo))
            {
                sftp.Connect();
                sftp.DeleteFile("customers.txt");
                sftp.Disconnect();

            }

            return true;

        }

        public bool TransferFileToSFtp(string filePathSource, FtpEntity ftpDestiny)
        {
            var connectionInfo = new ConnectionInfo(ftpDestiny.Host,
                                       ftpDestiny.User,
                                       new PasswordAuthenticationMethod(ftpDestiny.User, ftpDestiny.Password)
                                       );

            using (var sftp = new SftpClient(connectionInfo))
            {
                using (var uplfileStream = System.IO.File.OpenRead(filePathSource))
                {
                    sftp.Connect();
                    sftp.UploadFile(uplfileStream, "customers.txt", true);
                    sftp.Disconnect();
                }
            }
            return true;
        }

        public void TrasnferInfoToSFtp(FtpEntity ftp, string text)
        {
            var connectionInfo = new ConnectionInfo(ftp.Host,
                                       ftp.User,
                                       new PasswordAuthenticationMethod(ftp.User, ftp.Password)
                                       );
            byte[] messageContent = Encoding.ASCII.GetBytes(text);
            //string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            using (var sftp = new SftpClient(connectionInfo))
            {
                sftp.Connect();
                sftp.WriteAllBytes("customers.txt", messageContent);
                sftp.Disconnect();
            }

        }
    }
}
