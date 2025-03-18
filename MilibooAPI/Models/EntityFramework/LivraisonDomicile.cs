using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_livraison_domicile_liv", Schema = "miliboo")]
    [PrimaryKey(nameof(LivraisonId))]
    public partial class LivraisonDomicile
    {
        [Column("liv_idtypelivraison")]
        public int TypeLivraisonId { get; set; }

        [Key]
        [Column("liv_idlivraison")]
        public int LivraisonId { get; set; }

        [Column("liv_idadresse")]
        public int Adresseid { get; set; }

        [Column("liv_libelletypelivraison")]
        [StringLength(50)]
        public string? Libelletypelivraison { get; set; }

        [Column("liv_estexpress")]
        public bool? Estexpress { get; set; }

        [ForeignKey(nameof(Adresseid))]
        [InverseProperty(nameof(Adresse.LivraisonDomiciles))]
        public virtual Adresse IdadresseNavigation { get; set; } = null!;

        [InverseProperty(nameof(Commande.IdLivraisonNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
