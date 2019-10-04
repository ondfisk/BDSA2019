using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BDSA2019.Lecture07.Entities
{
    public class Episode
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public DateTime? FirstAired { get; set; }

        public ICollection<EpisodeCharacter> EpisodeCharacters { get; set; }
    }
}
