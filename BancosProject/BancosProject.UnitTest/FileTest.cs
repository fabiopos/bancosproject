using BancosProject.Business.Implementacion;
using BancosProject.Business.Interfaz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancosProject.UnitTest
{
    [TestClass]
    public class FileTest
    {
        ICsvFile _bFile;
        public FileTest()
        {
            _bFile = new BusinessFile(new Customer(), new Cifrado(2048),new FtpUtil());
        }

        [TestMethod]
        public void GenerarArchivo()
        {
            string publicKey = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\public.xml";
            string filePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientes.txt";
            string encriptedFilePath = @"F:\Universidad\Teoría de la Información y la Comunicación\Project\FTP\clientesEncripted.txt";
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Uri = "ftp://40.71.253.112/customers.txt" };
            _bFile.RunProcess(filePath, publicKey, encriptedFilePath, ftpInfo);
        }

        [TestMethod]
        public void GenerarArchivoFtpSinLocal()
        {
            string publicKeyString = "<RSAKeyValue><Modulus>wdACfChfxVibN1lWC3wj9/KYRDHntYbMazJj8GaDgIHhhurwm48PTbdOf7mk+Sd/NLdpMNOucWXjSQhBhT7pFTlkQeFo4mDKqljCV127eoG1zsO49x6L7EPhvVuZPgasDQnDSZJX8NeBbtHVAShDMjV95J+regVorseI84jcAk1fjZ1Bj4pRBU/26T9axu+7JkRohURU5d7w0DiOuz04Jpc95jGP1P3THsWIVQF5w4aeaeDmXTEhByBFlR2+4G6znlO5Px3W9fdpnPn3ykJIwxauNh5os5pxlUcEWGfv1KPAxNOxJxZhOgRgjJhRlLaxLKQSIRCQfKQ0DZWDy32L7w==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";            
            var ftpInfo = new FtpEntity { User = "developer", Password = "Developer05*", Host = "40.71.253.112" };
            _bFile.RunProcessSftp(publicKeyString, ftpInfo);
        }

    }
}
