using System;

namespace DAL.Models
{
    public interface IPersistable
    {
        Guid Id { get; set; }
    }
}