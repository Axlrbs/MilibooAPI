using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_appartient_aprt", Schema = "miliboo")]
    [PrimaryKey(nameof(RegroupementId),nameof(ProduitId))]
    public partial class Appartient
    {
        [Key]
        [Column("aprt_idregroupement")]
        public int RegroupementId { get; set; }

        [Key]
        [Column("aprt_idproduit")]
        public int ProduitId { get; set; }

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.Appartients))]
        public virtual Produit IdProduitNavigation { get; set; } = null!;

        [ForeignKey(nameof(RegroupementId))]
        [InverseProperty(nameof(Regroupement.Appartients))]
        public virtual Regroupement IdRegroupementNavigation { get; set; } = null!;
    }
}
