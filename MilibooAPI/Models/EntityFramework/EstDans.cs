using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estdans_esd", Schema = "miliboo")]
    [PrimaryKey(nameof(CategorieId), nameof(TypeProduitId))]
    public partial class EstDans
    {
        [Key]
        [Column("esd_idcategorie")]
        public int CategorieId { get; set; }

        [Key]
        [Column("esd_idtypeproduit")]
        public int TypeProduitId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.EstDanss))]
        public virtual Categorie IdcategorieNavigation { get; set; } = null!;

        [ForeignKey(nameof(TypeProduitId))]
        [InverseProperty(nameof(TypeProduit.EstDanss))]
        public virtual TypeProduit IdtypeproduitNavigation { get; set; } = null!;
    }
}
