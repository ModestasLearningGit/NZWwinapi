using WZWalksAPI.Models.Domain;

namespace WZWalksAPI.Models.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Leght { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //navigation props

        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
