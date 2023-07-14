using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class IndexedEntity
    {
        [Key] public int Id { get; set; }
    }
}