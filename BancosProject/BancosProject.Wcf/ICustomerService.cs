using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BancosProject.Wcf
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICustomerService
    {

        [OperationContract]
        void DeleteCustomersFile(string user, string password);

        [OperationContract]
        void GenerateFile();

        [OperationContract]
        void DeleteCustomersSftpFile(string user, string password);

        [OperationContract]
        void GenerateFileSftp(string user, string password);
    }


 
}
