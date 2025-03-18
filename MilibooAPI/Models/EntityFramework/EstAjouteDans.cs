using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estajoutedans_ejd", Schema = "miliboo")]
    [PrimaryKey(nameof(ProduitId), nameof(PanierId), nameof(ColorisId))]
    public partial class EstAjouteDans
    {
        [Key]
        [Column("ejd_idproduit")]
        public int ProduitId { get; set; }

        [Key]
        [Column("ejd_idpanier")]
        public int PanierId { get; set; }

        [Key]
        [Column("ejd_idcoloris")]
        public int ColorisId { get; set; }

        [Column("ejd_quantitepanier")]
        public int? Quantitepanier { get; set; }

        [ForeignKey(nameof(ColorisId))]
        [InverseProperty(nameof(Coloris.EstAjouteDanss))]
        public virtual Coloris IdcolorisNavigation { get; set; } = null!;

        [ForeignKey(nameof(PanierId))]
        [InverseProperty(nameof(Panier.EstAjouteDanss))]
        public virtual Panier IdpanierNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.EstAjouteDanss))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;
    }
}
