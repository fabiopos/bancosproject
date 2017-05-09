using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancosProject.Data;

namespace BancosProject.Business.Interfaz
{
    public interface ICustomer
    {
        void CreateCustomer(string id, string name, string lastName, bool isValid);
        void ToggleValidCustomer(string id, bool status);
        void UpdateCustomer(string id, string name, string lastName);
        void DeleteCustomer(string id);
        bool Exists(string id);
        ClientesBanco Find(string id);
        List<ClientesBanco> GetCustomers();

    }
}
