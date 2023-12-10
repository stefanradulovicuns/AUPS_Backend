namespace AUPS_Backend.DTO
{
    public class ObjectOfLaborMaterialUpdateDTO
    {
        public Guid ObjectOfLaborMaterialId { get; set; }

        public int Quantity { get; set; }

        public Guid ObjectOfLaborId { get; set; }

        public Guid MaterialId { get; set; }
    }
}
