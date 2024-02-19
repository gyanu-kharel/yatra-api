using System.ComponentModel.DataAnnotations.Schema;

namespace YatraBackend.Database.Models;

public class Metadata
{
    public Guid Id { get; set; }
    public List<string>? Content { get; set; }
}