using IhChegou.Repository.Contract;
using System.Data;

namespace IhChegou.Repository.Session
{
    public class MysqlSession : IDatabaseSession
    {
        public IDbConnection GetConnection()
        {

#if DEBUG
            return new MySql.Data.MySqlClient.MySqlConnection("Server=localhost;Database=ihchegou;Uid=root;Pwd=root;");
#else
           return  new MySql.Data.MySqlClient.MySqlConnection("Server=terra;Database=homolog_ihchegou;Uid=mobfiq;Pwd=e5828c564f71fea3a12dde8bd5d27063;");
#endif
        }
    }
}


