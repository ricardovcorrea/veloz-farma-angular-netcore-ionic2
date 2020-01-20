using Nest;

namespace IhChegou.Repository.Contract
{
    public interface IElasticSession
    {
        IElasticClient GetElasticClient();
    }
}