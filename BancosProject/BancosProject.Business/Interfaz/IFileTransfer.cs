using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancosProject.Business.Interfaz
{
    public interface IFileTransfer
    {
        bool TransferFileToFtp(string filePathSource, FtpEntity ftpDestiny);
        bool TransferFileToSFtp(string filePathSource, FtpEntity ftpDestiny);
        bool DeleteFileFromFtp(FtpEntity ftpSource);
        bool DeleteFileFromSFtp(FtpEntity ftpSource);
        void TrasnferInfoToFtp(FtpEntity ftp, string text);        
        void TrasnferInfoToSFtp(FtpEntity ftp, string text);
    }

    public class FtpEntity
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Uri { get; set; }
        public string Host { get; set; }
    }
}
