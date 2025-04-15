using System.ComponentModel.DataAnnotations;

namespace Persistence.Models;

public class RoleEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}