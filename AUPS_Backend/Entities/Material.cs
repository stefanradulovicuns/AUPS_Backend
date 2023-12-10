using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUPS_Backend.Entities
{
    [Table("material")]
    public partial class Material
    {
        [Key]
        [Column("material_id")]
        public Guid MaterialId { get; set; }

        [Column("material_name")]
        [StringLength(200)]
        public string MaterialName { get; set; } = null!;

        [Column("stock_quantity")]
        public int StockQuantity { get; set; }

        [InverseProperty("Material")]
        public virtual ICollection<ObjectOfLaborMaterial> ObjectOfLaborMaterials { get; set; } = new List<ObjectOfLaborMaterial>();
    }
}
