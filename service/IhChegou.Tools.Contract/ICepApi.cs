using IhChegou.DTO.Client;

namespace IhChegou.Tools.Contract
{
    public interface ICepApi
    {
        DtoAddress Search(string cep);
    }
}