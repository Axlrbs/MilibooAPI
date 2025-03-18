using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_constitue_cst", Schema = "miliboo")]
    [PrimaryKey(nameof(ProduitId), nameof(EnsembleId))]
    public partial class Constitue
    {
        [Key]
        [Column("cst_idproduit")]
        public int ProduitId { get; set; }

        [Key]
        [Column("cst_idensemble")]
        public int EnsembleId { get; set; }

        [ForeignKey(nameof(EnsembleId))]
        [InverseProperty(nameof(EnsembleProduit.Constitues))]
        public virtual EnsembleProduit IdensembleNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.Constitues))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;
    }
}
