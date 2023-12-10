using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUPS_Backend.Entities
{
    [Table("object_of_labor_material")]
    public partial class ObjectOfLaborMaterial
    {
        [Key]
        [Column("object_of_labor_material_id")]
        public Guid ObjectOfLaborMaterialId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("object_of_labor_id")]
        public Guid ObjectOfLaborId { get; set; }

        [Column("material_id")]
        public Guid MaterialId { get; set; }

        [ForeignKey("ObjectOfLaborId")]
        [InverseProperty("ObjectOfLaborMaterials")]
        public virtual ObjectOfLabor ObjectOfLabor { get; set; } = null!;

        [ForeignKey("MaterialId")]
        [InverseProperty("ObjectOfLaborMaterials")]
        public virtual Material Material { get; set; } = null!;
    }
}
