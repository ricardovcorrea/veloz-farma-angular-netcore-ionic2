using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.Store
{
    public class DtoWorkDay
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan OpenAt { get; set; }
        public TimeSpan CloseAt { get; set; }
        public int Id { get; set; }
    }
}
