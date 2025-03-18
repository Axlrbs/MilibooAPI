using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_paypal_pyp", Schema = "miliboo")]
    [PrimaryKey(nameof(PaypalId))]
    public partial class Paypal
    {
        [Column("pyp_idtypepaiement")]
        public int TypePaiementId { get; set; }

        [Key]
        [Column("pyp_idpaypal")]
        public int PaypalId { get; set; }

        [Column("pyp_libelletypepaiement")]
        [StringLength(50)]
        public string? Libelletypepaiement { get; set; }

        [ForeignKey(nameof(TypePaiementId))]
        [InverseProperty(nameof(TypePaiement.Paypals))]
        public virtual TypePaiement IdPaypalNavigation { get; set; } = null!;

        [InverseProperty(nameof(Commande.IdPaypalNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
