namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborMaterialCreateDTO
    {
        public int Quantity { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public Guid MaterialId { get; set; }
    }
}
