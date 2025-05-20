using System;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Entities.AppEntitie;

public class BaseEntity
{
    [Key]
public int Id { get; set; }
}
