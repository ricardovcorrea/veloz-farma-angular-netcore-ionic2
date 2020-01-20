using Correios;
using IhChegou.DTO.Client;
using IhChegou.Tools.Contract;

namespace IhChegou.Tools.Correios
{
    public class CepApi : ICepApi
    {
        public DtoAddress Search(string cep)
        {
            var service = new CorreiosApi();

            var task =  service.consultaCEPAsync(cep).Result;

            var result = task.@return;
            if (result == null)
                return null;

            return new DtoAddress()
            {
                City = result.cidade,
                Complement = result.complemento,
                Neighborhood = result.bairro,
                PostalCode = result.cep,
                Reference = result.complemento2,
                State = result.uf,
                Street = result.end,
            };
        }
    }
}
