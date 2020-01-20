namespace IhChegou.DTO.Client
{
    public class DtoAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}