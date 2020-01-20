using Dapper;
using IhChegou.DTO.Client;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Pharma.Model.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Pharma.Database
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        public ClientRepository(IDatabaseSession session) : base(session)
        {
        }

        public int GetClientId(string token)
        {
            var client = GetByKey<Client>("Token", token, "Client");
            return client.Id;
        }
        public DtoClient GetClient(string email, string passwordHash)
        {
            var client = GetByKey<Client>("Email", email, "Client");
            if (client?.PasswordHash == passwordHash)
                return FillClient(client);
            else
                return null;
        }
        public DtoClient GetClient(string token)
        {
            var client = GetByKey<Client>("Token", token, "Client");
            return FillClient(client);
        }
        public DtoClient GetClient(int id)
        {
            var client = GetById<Client>(id, "Client");
            return FillClient(client);
        }

        public void Create(ref DtoClient dtoClient)
        {
            var model = dtoClient.ToRepository();
            var address = dtoClient.Addresses.Where(i => i.Id == 0);
            var insertdId = Insert(model, "Client");
            var addresses = dtoClient.Addresses.Where(i => i.Id == 0).Select(i => i.ToRepository(insertdId));
            Insert(addresses, "Address");
            dtoClient.Id = insertdId;
            dtoClient.Addresses = GetAllAddress(dtoClient.Token);
        }
        public void UpdateClient(ref DtoClient dtoClient)
        {
            var model = dtoClient.ToRepository();

            var dbClient = this.GetClient(dtoClient.Token);

            model.Id = dbClient.Id;

            if (model.PasswordHash == null)
                model.PasswordHash = dbClient?.PasswordHash;

            this.Update(model, "Client");
        }

        public DtoAddress GetAddress(int id)
        {
            var address = GetById<Address>(id, "Address");
            return address.ToDTO();
        }
        public List<DtoAddress> GetAllAddress(string clientToken)
        {
            var clientId = this.GetClientId(clientToken);
            var address = GetByKeyAll<Address>("Client_Id", clientId, "Address");
            return address.Select(i => i.ToDTO()).ToList();
        }
        public void AddAddress(ref DtoAddress address, string token)
        {
            var clientId = this.GetClientId(token);
            var addressModel = address.ToRepository(clientId);
            var insetedId = Insert(addressModel, "Address");
            address.Id = insetedId;
        }
        public void DeleteAddress(string clientToken, int addressId)
        {
            var clientId = this.GetClientId(clientToken);
            var address = GetByKeyAll<Address>("Client_Id", clientId, "Address");
            if (address.Where(i => i.Id == addressId).Count() > 0)
            {
                Delete(addressId, "Address");
            }
        }


        private DtoClient FillClient(Client client)
        {
            var address = GetByKeyAll<Address>("Client_Id", client.Id, "Address");
            return client.ToDTO(address);
        }
    }
}
