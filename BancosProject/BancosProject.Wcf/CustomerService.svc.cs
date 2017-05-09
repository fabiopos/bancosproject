using BancosProject.Business.Implementacion;
using BancosProject.Business.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BancosProject.Wcf
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : ICustomerService
    {
        IFileTransfer _ftp;
        public void DeleteCustomersFile(string user, string password)
        {
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "ftp://40.71.253.112/customers.txt" };
             _ftp = new FtpUtil();
            _ftp.DeleteFileFromFtp(ftpInfo);
        }      

        public void DeleteCustomersSftpFile(string user, string password)
        {
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            _ftp = new FtpUtil();
            _ftp.DeleteFileFromSFtp(ftpInfo);
        }

        public void GenerateFile()
        {
            var _bFile = new BusinessFile(new Customer(), new Cifrado(2048), new FtpUtil());
            string publicKeyString = "<RSAKeyValue><Modulus>wdACfChfxVibN1lWC3wj9/KYRDHntYbMazJj8GaDgIHhhurwm48PTbdOf7mk+Sd/NLdpMNOucWXjSQhBhT7pFTlkQeFo4mDKqljCV127eoG1zsO49x6L7EPhvVuZPgasDQnDSZJX8NeBbtHVAShDMjV95J+regVorseI84jcAk1fjZ1Bj4pRBU/26T9axu+7JkRohURU5d7w0DiOuz04Jpc95jGP1P3THsWIVQF5w4aeaeDmXTEhByBFlR2+4G6znlO5Px3W9fdpnPn3ykJIwxauNh5os5pxlUcEWGfv1KPAxNOxJxZhOgRgjJhRlLaxLKQSIRCQfKQ0DZWDy32L7w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "ftp://40.71.253.112/customers.txt" };
            _bFile.RunProcess(publicKeyString, ftpInfo);
        }

        public void GenerateFileSftp(string user, string password)
        {
            var _bFile = new BusinessFile(new Customer(), new Cifrado(2048), new FtpUtil());
            string publicKeyString = "<RSAKeyValue><Modulus>wdACfChfxVibN1lWC3wj9/KYRDHntYbMazJj8GaDgIHhhurwm48PTbdOf7mk+Sd/NLdpMNOucWXjSQhBhT7pFTlkQeFo4mDKqljCV127eoG1zsO49x6L7EPhvVuZPgasDQnDSZJX8NeBbtHVAShDMjV95J+regVorseI84jcAk1fjZ1Bj4pRBU/26T9axu+7JkRohURU5d7w0DiOuz04Jpc95jGP1P3THsWIVQF5w4aeaeDmXTEhByBFlR2+4G6znlO5Px3W9fdpnPn3ykJIwxauNh5os5pxlUcEWGfv1KPAxNOxJxZhOgRgjJhRlLaxLKQSIRCQfKQ0DZWDy32L7w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            _bFile.RunProcessSftp(publicKeyString, ftpInfo);
        }
    }
}
