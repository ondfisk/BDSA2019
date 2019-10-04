using System.ComponentModel.DataAnnotations;

namespace BDSA2019.Lecture07.Models
{
    public class CharacterCreateUpdateDTO
    {
        public int Id { get; set; }

        public int? ActorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Species { get; set; }

        [StringLength(50)]
        public string Planet { get; set; }
    }
}
