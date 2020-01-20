using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IhChegou.DTO.Order;
using IhChegou.DTO.Store;

namespace IhChegou.Repository.Pharma.Model.Stores
{
    public class Workday
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan CloseAt { get; set; }

        internal DtoWorkDay ToDto()
        {
            return new DtoWorkDay()
            {
                Id = Id,
                CloseAt = CloseAt,
                Day = Day,
                OpenAt = OpenAt
            };
        }
    }
}
