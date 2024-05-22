using System.ComponentModel.DataAnnotations;

namespace Model.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
