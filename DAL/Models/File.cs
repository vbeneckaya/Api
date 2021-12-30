using System;

namespace DAL.Models
{
    public class File : IPersistable
    {
        public Guid Id { get; set; }
        public String FileName { get; set; }
        public String Ext { get; set; }
        public Byte[] Data { get; set; }
    }
}