//using IhChegou.Domain.Client;
//using IhChegou.Domain.Product;
//using IhChegou.Domain.Search;
//using IhChegou.ElasticSearch;
//using IhChegou.Global.Extensions.Object;
using IhChegou.DTO.Client;
using IhChegou.Global.Extensions.IList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.POCO
{
    class Program
    {
        static  void Main(string[] args)
        {


            var list = new List<string>() { "mimimi" };
            var list2 = new List<string>() { "mimimi" };

            list.SetRowCount(1);
            list2.SetRowCount(2);

            Console.WriteLine(list.RowCount());
            Console.WriteLine(list2.RowCount());

            Console.ReadKey();

        }
    }
}
