using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estdecouleur_edc", Schema = "miliboo")]
    [Index(nameof(Nomproduit), Name = "idx_estdecouleur_nomproduit")]
    [PrimaryKey(nameof(EstDeCouleurId))]
    public partial class EstDeCouleur
    {
        [Key]
        [Column("edc_id")]
        public int EstDeCouleurId { get; set; }

        [Column("edc_idcoloris")]
        public int ColorisId { get; set; }

        [Column("edc_idproduit")]
        public int ProduitId { get; set; }

        [Column("edc_codephoto")]
        [StringLength(10)]
        public string Codephoto { get; set; } = null!;

        [Column("edc_nomproduit")]
        [StringLength(200)]
        public string? Nomproduit { get; set; }

        [Column("edc_prixttc")]
        public decimal? Prixttc { get; set; }

        [Column("edc_description")]
        [StringLength(2000)]
        public string? Description { get; set; }

        [Column("edc_quantite")]
        public int? Quantite { get; set; }

        [Column("edc_promotion")]
        public int? Promotion { get; set; }

        [ForeignKey(nameof(ColorisId))]
        [InverseProperty(nameof(Coloris.EstDeCouleurs))]
        public virtual Coloris IdcolorisNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.EstDeCouleurs))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;

        [InverseProperty(nameof(EstCommande.IdEstDeCouleurNavigation))]
        public virtual ICollection<EstCommande> EstCommandes { get; set; } = new List<EstCommande>();
    }
}
