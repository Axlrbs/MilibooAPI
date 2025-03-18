using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_carte_bancaire_crtban", Schema = "miliboo")]
    [PrimaryKey(nameof(CarteBancaireId))]
    public partial class CarteBancaire
    {
        [Key]
        [Column("crtban_idcartebancaire")]
        public int CarteBancaireId { get; set; }

        [Column("crtban_libelletypepaiement")]
        [StringLength(50)]
        public string? Libelletypepaiement { get; set; }

        [Column("crtban_nomcarte", TypeName = "varchar(20)")]
        public string NomCarte { get; set; }

        [Column("crtban_numcarte", TypeName = "varchar(16)")]
        public string NumCarte { get; set; }

        [Column("crtban_dateexpiration", TypeName = "varchar(10)")]
        public string DateExpiration { get; set; }

        [Column("crtban_cvvcarte", TypeName = "varchar(3)")]
        public string Cvvcarte { get; set; }

        [InverseProperty(nameof(Commande.IdCarteNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
