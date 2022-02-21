using System;

namespace DAL.Models
{
    public class File : Base
    {
        public String FileName { get; set; }
        public String Ext { get; set; }
        public Byte[] Data { get; set; }
    }
}