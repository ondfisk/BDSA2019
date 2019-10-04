namespace BDSA2019.Lecture07.Entities
{
    public class EpisodeCharacter
    {
        public int CharacterId { get; set; }

        public int EpisodeId { get; set; }

        public Character Character { get; set; }

        public Episode Episode { get; set; }
    }
}
