using System.Data;

namespace IhChegou.Repository.Contract
{
    public interface IDatabaseSession
    {
        IDbConnection GetConnection();
    }
}