namespace WZWalksAPI.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double Leght { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

    }
}
