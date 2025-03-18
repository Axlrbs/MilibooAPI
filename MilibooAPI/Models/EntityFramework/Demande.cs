using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_demande_dmd", Schema = "miliboo")]
    [PrimaryKey(nameof(ProduitId), nameof(ProfessionnelId))]
    public partial class Demande
    {
        [Key]
        [Column("dmd_idproduit")]
        public int ProduitId { get; set; }

        [Key]
        [Column("dmd_idprofessionnel")]
        public int ProfessionnelId { get; set; }

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.Demandes))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProfessionnelId))]
        [InverseProperty(nameof(Professionnel.Demandes))]
        public virtual Professionnel IdProfessionnelNavigation { get; set; } = null!;
    }
}
