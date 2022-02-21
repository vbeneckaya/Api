using System;

namespace DAL.Models
{
    public class MyDaysGroup : Base
    {
        public Guid UserId { get; set; }
        public Int64 Size { get; set; }
        public Int64 SizeBefore { get; set; }
        public Int64 SizeTotal { get; set; }
        public Int64 Number { get; set; }
    }
}