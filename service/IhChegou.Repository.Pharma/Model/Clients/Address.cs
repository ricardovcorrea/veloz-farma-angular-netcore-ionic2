namespace IhChegou.Repository.Pharma.Model.Clients
{
     public class Address
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Street { get; set; }
        public virtual string Number { get; set; }
        public virtual string Neighborhood { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Complement { get; set; }
        public virtual string Reference { get; set; }
        public virtual double? Latitude { get; set; }
        public virtual double? Longitude { get; set; }
        public virtual int? Client_Id { get; internal set; }
    }
}