using Dapper;
using IhChegou.Global.Extensions.IList;
using IhChegou.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IhChegou.Repository.Pharma.Database
{
    public class RepositoryBase
    {
        protected IDatabaseSession SessionManager { get; set; }
        public RepositoryBase(IDatabaseSession session)
        {
            SessionManager = session;
        }
        protected List<T> GetAll<T>(string table)
        {
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<T>($"select * from {table};");
                return result.ToList();
            }
        }

        protected List<T> GetAll<T>(string table, int offset = 0, int limit = 50)
        {
            var query = $"select * from {table}";
            query += $" limit {limit}";
            query += $" offset {offset}";

            var countQuery = $"select count(*) from {table}";

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();

                var result = connecton.Query<T>(query).ToList();
                var count = connecton.Query<int>(countQuery).Single();

                result.SetRowCount(count);

                return result;
            }
        }
        protected T GetById<T>(int id, string table)
        {
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.QueryFirstOrDefault<T>($"select * from {table} where Id = {id};");
                return result;
            }
        }
        protected List<T> GetByIdIn<T>(List<int> Ids, string table)
        {
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<T>($"select * from {table} where Id in @Ids;", new { Ids = Ids });
                return result.ToList();
            }
        }

        protected T GetByKey<T>(string key, int value, string table)
        {
            return GetByKey<T>(key, value.ToString(), table);
        }
        protected T GetByKey<T>(string key, string value, string table)
        {
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.QueryFirstOrDefault<T>($"select * from {table} where {key} = '{value}'");
                return result;
            }
        }
        protected List<T> GetByKeyAll<T>(string key, int value, string table)
        {
            return GetByKeyAll<T>(key, value.ToString(), table);
        }
        protected List<T> GetByKeyAll<T>(string key, string value, string table)
        {
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<T>($"select * from {table} where {key} = {value};");
                return result.ToList();
            }
        }

        protected int Insert<T>(T _obj, string table)
        {
            var popNames = typeof(T).GetProperties()?.Select(i => i.Name);
            var props = string.Join(", ", popNames);

            List<string> valueNames = new List<string>();
            foreach (var item in popNames)
            {
                valueNames.Add($"@{item}");
            }
            var values = string.Join(", ", valueNames);
            using (var con = SessionManager.GetConnection())
            {
                int insertedId = 0;
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    insertedId = con.Query<int>($"INSERT INTO {table}" +
                       $" ({props}) Values({values});" +
                       $" SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER)", _obj).SingleOrDefault();
                    trans.Commit();
                }
                return insertedId;
            }
        }
        protected void Insert<T>(IEnumerable<T> _obj, string table)
        {
            var popNames = typeof(T).GetProperties()?.Select(i => i.Name);
            var props = string.Join(", ", popNames);

            List<string> valueNames = new List<string>();
            foreach (var item in popNames)
            {
                valueNames.Add($"@{item}");
            }
            var values = string.Join(", ", valueNames);
            using (var con = SessionManager.GetConnection())
            {
                int insertedId = 0;
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    insertedId = con.Execute($"INSERT INTO {table}" +
                       $" ({props}) Values({values});", _obj);
                    trans.Commit();
                }
            }
        }

        protected int Update<T>(T _obj, string table)
        {
            var popNames = typeof(T).GetProperties()?.Select(i => i.Name);
            List<string> valueNames = new List<string>();
            foreach (var item in popNames)
            {
                valueNames.Add($"{item} = @{item}");
            }
            var props = string.Join(", ", popNames);
            var values = string.Join(", ", valueNames);
            using (var con = SessionManager.GetConnection())
            {
                int insertedId = 0;
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    insertedId = con.Query<int>($"UPDATE {table} " +
                       $" SET {values} " +
                       $" Where Id = @Id;" +
                       $" SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER)", _obj).SingleOrDefault();
                    trans.Commit();
                }
                return insertedId;
            }
        }

        protected void InsertFk(string paramName, int paramValue, string paramName2, int paramValue2, string table)
        {
            var query = $"INSERT IGNORE {table} ({paramName}, {paramName2}) " +
                            $"SELECT DISTINCT {paramValue}, {paramValue2} FROM CategoryToProduct " +
                                $"WHERE NOT EXISTS " +
                                $"(SELECT * FROM {table} WHERE {paramName} = {paramValue} and {paramName2} = {paramValue2});";

            using (var con = SessionManager.GetConnection())
            {
                int insertedId = 0;
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    insertedId = con.Execute(query);
                    trans.Commit();
                }
            }
        }


        protected void Delete(int id, string table)
        {

            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    con.Execute($"delete {table} " +
                        $" Where Id = @Id");
                    trans.Commit();
                }
            }
        }


        protected string GetCountQuery(string query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (query.ToLower().Contains("size") || query.ToLower().Contains("offset"))
            {
                throw new ArgumentException("Count query with pagination");
            }

            return query.Replace("*", "Count(*) ");
        }

        protected void SetPagination(string query, int size, int page)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (query.Last() == ';')
            {
                query = query.Remove(query.LastIndexOf(';'));
            }

            if (query.ToLower().Contains("size") || query.ToLower().Contains("offset"))
            {
                throw new ArgumentException("Count query with pagination");
            }

            query += $" SIZE {size} OFFSET {page * size}; ";
        }


    }
}