using System.ComponentModel.DataAnnotations;

namespace MovieRental.Shared;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}