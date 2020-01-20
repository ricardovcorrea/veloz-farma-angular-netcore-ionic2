using IhChegou.Repository.Model.Clients;
using IhChegou.Repository.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Stores
{
     public class Store
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Document { get; set; }
        public virtual float Profit { get; set; }
        public virtual double MaxDistance { get; set; }
        public virtual int Address_Id { get; set; }

        //public virtual IList<PaymentMethod> AvailablePayments { get; set; }
        //public virtual IList<Delivery> AvaibleDeliveries { get; set; }

    }
}
