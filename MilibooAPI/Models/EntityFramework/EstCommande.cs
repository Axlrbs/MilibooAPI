using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estcommande_esc", Schema = "miliboo")]
    [PrimaryKey(nameof(CommandeId), nameof(EstDeCouleurId))]
    public partial class EstCommande
    {
        [Key]
        [Column("esc_idcommande")]
        public int CommandeId { get; set; }

        [Key]
        [Column("esc_idestdecouleur")]
        public int EstDeCouleurId { get; set; }

        [Column("esc_quantite")]
        public decimal Quantite { get; set; }

        [ForeignKey(nameof(CommandeId))]
        [InverseProperty(nameof(Commande.EstCommandes))]
        public virtual Commande IdCommandeNavigation { get; set; } = null!;

        [ForeignKey(nameof(EstDeCouleurId))]
        [InverseProperty(nameof(EstDeCouleur.EstCommandes))]
        public virtual EstDeCouleur IdEstDeCouleurNavigation { get; set; } = null!;
    }
}
