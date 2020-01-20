using EnsureThat;
using IhChegou.Domain.Contract.Client;
using IhChegou.Domain.Contract.Order;
using IhChegou.DTO.Client;
using IhChegou.Global.Exceptions;
using IhChegou.Global.Extensions.String;
using IhChegou.Repository.Pharma.Database;
using IhChegou.Tools.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IhChegou.Domain.Pharma.Client
{
    public class ClientDomain : IClientDomain
    {
        private IClientRepository ClientQueries;
        private IOrderDomain OrderDomain;
        private ICepApi CepApi;
        private IGeocodeApi GeocodeApi;



        public ClientDomain(IClientRepository clientQueries, IOrderDomain orderDomain, ICepApi cepApi, IGeocodeApi geocodeApi)
        {
            ClientQueries = clientQueries;
            OrderDomain = orderDomain;
            CepApi = cepApi;
            GeocodeApi = geocodeApi;
        }

        public DtoClient Get(string token)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(token, nameof(token));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var client = ClientQueries.GetClient(token);
            GetAtualOrder(ref client);
            return client;
        }
        public DtoClient Login(string email, string password)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(email, nameof(email));
                EnsureArg.IsNotNullOrEmpty(password, nameof(password));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var client = ClientQueries.GetClient(email, password.EncryptSHA256());

            if (client == null)
                throw new UnauthorizedAccessException();
            return Get(client.Token);
        }

        public void SaveOrUpdate(ref DtoClient client)
        {
            try
            {
                EnsureArg.IsNotNull(client, nameof(client));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }


            if (client.Token == null)
            {
                client.Token = Guid.NewGuid().ToString();
                ClientQueries.Create(ref client);
            }
            GetAtualOrder(ref client);
        }
        public void SaveAddress(ref DtoAddress address, string clientToken)
        {
            try
            {
                EnsureArg.IsNotNull(address, nameof(address));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var client = Get(clientToken);
            ClientQueries.AddAddress(ref address, clientToken);
        }

        public List<DtoAddress> RemoveAddress(int id, string clientToken)
        {

            try
            {
                EnsureArg.IsGt(id, 0, nameof(id));
                EnsureArg.IsNotNullOrEmpty(clientToken, nameof(clientToken));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            ClientQueries.DeleteAddress(clientToken, id);
            return ClientQueries.GetAllAddress(clientToken);
        }

        public DtoAddress GetAddressInfo(DtoAddress address)
        {

            try
            {
                EnsureArg.IsNotNull(address, nameof(address));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            if (address?.PostalCode != null)
            {
                return GetAddressInfo(address.PostalCode);
            }
            return GeocodeApi.GetAddress(address);
        }
        public DtoAddress GetAddressInfo(double latitude, double longitude)
        {
            try
            {
                EnsureArg.IsGt(latitude, 0, nameof(latitude));
                EnsureArg.IsGt(longitude, 0, nameof(longitude));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }


            return GeocodeApi.GetAddress(latitude, longitude);
        }
        public DtoAddress GetAddressInfo(string postalcode)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(postalcode, nameof(postalcode));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var correioResult = CepApi.Search(postalcode);
            var googleRsult =  GeocodeApi.GetAddress(correioResult);

            return new DtoAddress()
            {
                City = correioResult.City ?? googleRsult.City,
                Complement = correioResult.Complement ?? googleRsult.Complement,
                Latitude = correioResult.Latitude ?? googleRsult.Latitude,
                Longitude = correioResult.Longitude ?? googleRsult.Longitude,
                Name = correioResult.Name ?? googleRsult.Name,
                Neighborhood = correioResult.Neighborhood ?? googleRsult.Neighborhood,
                Number = correioResult.Number ?? googleRsult.Number,
                PostalCode = correioResult.PostalCode ?? googleRsult.PostalCode,
                Reference = correioResult.Reference ?? googleRsult.Reference,
                State = correioResult.State ?? googleRsult.State,
                Street = correioResult.Street ?? googleRsult.Street,


            };
        }

        private void GetAtualOrder(ref DtoClient client)
        {

            if (client != null && client.AtualOrder == null)
            {
                client.AtualOrder = OrderDomain.GetIncompletLastOrder(client);
            }
        }
    }
}