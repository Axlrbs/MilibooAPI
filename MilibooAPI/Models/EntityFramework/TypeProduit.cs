using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_typeproduit_tpp", Schema = "miliboo")]
    [Index(nameof(TypeProduitId), Name = "tpp_id", IsUnique = false)]
    public partial class TypeProduit
    {
        [Key]
        [Column("tpp_id")]
        public int TypeProduitId { get; set; }

        [Column("tpp_libelle")]
        [StringLength(50)]
        public string? LibelleTypeProduit { get; set; }

        [InverseProperty(nameof(EstDans.IdtypeproduitNavigation))]
        public virtual ICollection<EstDans> EstDanss { get; set; }
    }
}
