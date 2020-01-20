using System.Collections.Generic;
using IhChegou.DTO.Client;

namespace IhChegou.Repository.Pharma.Database
{
    public interface IClientRepository
    {
        void AddAddress(ref DtoAddress address, string token);
        void Create(ref DtoClient dtoClient);
        void DeleteAddress(string clientToken, int addressId);
        DtoAddress GetAddress(int id);
        List<DtoAddress> GetAllAddress(string clientToken);
        DtoClient GetClient(int id);
        DtoClient GetClient(string token);
        DtoClient GetClient(string email, string passwordHash);
        int GetClientId(string token);
        void UpdateClient(ref DtoClient dtoClient);
    }
}