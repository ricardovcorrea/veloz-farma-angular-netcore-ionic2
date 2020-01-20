
namespace IhChegou.Repository.Pharma.Model.Clients
{
     public class Client
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Token { get; set; }
        public virtual string Email { get; set; }
        public virtual string DeviceId { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string Document { get; set; }
    }
}
