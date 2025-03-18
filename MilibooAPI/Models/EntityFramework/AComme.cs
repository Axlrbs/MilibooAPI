using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_a_comme_acm", Schema = "miliboo")]
    [PrimaryKey(nameof(ProduitId), nameof(CategorieId))]
    public partial class AComme
    {
        [Key]
        [Column("acm_idproduit")]
        public int ProduitId { get; set; }

        [Key]
        [Column("acm_idcategorie")]
        public int CategorieId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.ACommes))]
        public virtual Categorie IdcategorieNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.ACommes))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;
    }
}
