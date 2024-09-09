using System;

namespace katio_net.Data.Models;

public class BaseEntity<TId> where TId: struct
{
    public TId Id { get; set;}
}
