using BancosProject.Business.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancosProject.Data;

namespace BancosProject.Business.Implementacion
{
    public class Customer : ICustomer
    {
        private BanRepEntities _context;

        public Customer()
        {
            _context = new BanRepEntities();
        }

        public void CreateCustomer(string id, string name, string lastName, bool isValid)
        {
            _context.ClientesBanco.Add(new ClientesBanco { Id = id, Name = name, LastName = lastName, IsValid = isValid });
            _context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            var customer = _context.ClientesBanco.Find(id);
            _context.ClientesBanco.Remove(customer);
        }

        public bool Exists(string id)
        {
            return _context.ClientesBanco.Find(id) != null;
        }

        public ClientesBanco Find(string id)
        {
            return _context.ClientesBanco.Find(id);
        }

        public List<ClientesBanco> GetCustomers()
        {
            return _context.ClientesBanco.ToList();
        }

        public void ToggleValidCustomer(string id, bool status)
        {
            var customer = _context.ClientesBanco.Find(id);

            if (customer == null)
                throw new Exception("Cliente no existe");

            customer.IsValid = !customer.IsValid;
            _context.SaveChanges();

        }

        public void UpdateCustomer(string id, string name, string lastName)
        {
            var customer = _context.ClientesBanco.Find(id);

            if (customer == null)
                throw new Exception("Cliente no existe");

            customer.Name = name;
            customer.LastName = lastName;
            _context.SaveChanges();
        }
    }
}
