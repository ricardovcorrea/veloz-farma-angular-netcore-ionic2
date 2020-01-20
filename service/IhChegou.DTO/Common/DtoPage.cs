using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.Common
{
    public class DtoPage<T>
    {
        public DtoPage(IEnumerable<T> result, int size, int? rowcount, int currentPage)
        {
            Result = result?.ToList();
            Size = size;
            RowCount = rowcount;
            CurrentPage = currentPage;

        }
        private int? RowCount { get; set; }
        private decimal Size { get; set; }

        public List<T> Result { get; set; }
        public int? TotalPages => GetTotalPages();
        public int CurrentPage { get; private set; }


        public int GetTotalPages()
        {
            if (RowCount != null)
            {
                var totalpage = this.RowCount / this.Size;
                return (int)Math.Ceiling(totalpage ?? 0);
            }
            return 0;
        }

    }
}
