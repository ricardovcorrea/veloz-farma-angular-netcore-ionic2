using System.Collections.Generic;
using IhChegou.DTO.Client;

namespace Contract.Domain.Pharma
{
    public interface IClientDomain
    {
        DtoClient Get(string token);
        DtoAddress GetAddressInfo(double latitude, double longitude);
        DtoAddress GetAddressInfo(DtoAddress address);
        DtoAddress GetAddressInfo(string postalcode);
        DtoClient Login(string email, string password);
        List<DtoAddress> RemoveAddress(int id, string clientToken);
        void SaveAddress(ref DtoAddress address, string clientToken);
        void SaveOrUpdate(ref DtoClient client);
    }
}