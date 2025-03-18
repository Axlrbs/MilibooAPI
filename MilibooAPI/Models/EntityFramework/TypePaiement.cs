using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_typepaiement_typ", Schema = "miliboo")]
    [PrimaryKey(nameof(TypePaiementId))]
    public partial class TypePaiement
    {
        [Key]
        [Column("typ_id")]
        public int TypePaiementId { get; set; }

        [Column("typ_libelle")]
        [StringLength(50)]
        public string? Libelletypepaiement { get; set; }

        [InverseProperty(nameof(VirementBancaire.IdVirementBancaireNavigation))]
        public virtual ICollection<VirementBancaire> VirementBancaires { get; set; } = new List<VirementBancaire>();

        [InverseProperty(nameof(Paypal.IdPaypalNavigation))]
        public virtual ICollection<Paypal> Paypals { get; set; } = new List<Paypal>();
    }
}
