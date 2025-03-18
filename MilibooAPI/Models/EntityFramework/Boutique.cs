using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_boutique_btq", Schema = "miliboo")]
    [PrimaryKey(nameof(BoutiqueId))]
    public partial class Boutique
    {
        [Column("btq_idtypelivraison")]
        public int TypeLivraisonId { get; set; }

        [Key]
        [Column("btq_idboutique")]
        public int BoutiqueId { get; set; }

        [Column("btq_idadresse")]
        public int AdresseId { get; set; }

        [Column("btq_mailboutique", TypeName = "varchar(50)")]
        public string MailBoutique { get; set; }

        [Column("btq_telboutique", TypeName = "varchar(50)")]
        public string TelBoutique { get; set; }

        [Column("btq_accesboutique", TypeName = "varchar(100)")]
        public string AccesBoutique { get; set; }

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.Boutiques))]
        public virtual Adresse IdadresseNavigation { get; set; } = null!;

        [InverseProperty(nameof(Commande.IdBoutiqueNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
