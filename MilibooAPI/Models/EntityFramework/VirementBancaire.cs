using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_virementbancaire_vba", Schema = "miliboo")]
    [PrimaryKey(nameof(VirementId))]
    public partial class VirementBancaire
    {
        [Column("vba_idtypepaiement")]
        public int TypePaiementId { get; set; }

        [Key]
        [Column("vba_id")]
        public int VirementId { get; set; }

        [Column("vba_iban")]
        [StringLength(34)]
        public string? Iban { get; set; }

        [InverseProperty(nameof(Commande.IdVirementNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

        [ForeignKey(nameof(TypePaiementId))]
        [InverseProperty(nameof(TypePaiement.VirementBancaires))]
        public virtual TypePaiement IdVirementBancaireNavigation { get; set; } = null!;
    }
}
