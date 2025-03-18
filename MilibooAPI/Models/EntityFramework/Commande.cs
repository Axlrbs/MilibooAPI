using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_commande_cmd", Schema = "miliboo")]
    [PrimaryKey(nameof(CommandeId))]
    public partial class Commande
    {
        [Key]
        [Column("cmd_idcommande")]
        public int CommandeId { get; set; }

        [Column("cmd_idpanier")]
        public int PanierId { get; set; }

        [Column("cmd_idvirement")]
        public int? VirementId { get; set; }

        [Column("cmd_idclient")]
        public int ClientId { get; set; }

        [Column("cmd_idlivraison")]
        public int? LivraisonId { get; set; }

        [Column("cmd_idboutique")]
        public int? BoutiqueId { get; set; }

        [Column("cmd_idpaypal")]
        public int? PaypalId { get; set; }

        [Column("cmd_idcarte")]
        public int? CarteBancaireId { get; set; }

        [Column("cmd_montantcommande", TypeName = "numeric")]
        public decimal? MontantCommande { get; set; }

        [Column("cmd_datefacture")]
        public DateTime? DateFacture { get; set; }

        [Column("cmd_nbpointfidelite")]
        public int? NbPointFidelite { get; set; }

        [Column("cmd_statut", TypeName = "varchar(50)")]
        public string? Statut { get; set; }

        
        [ForeignKey(nameof(PanierId))]
        [InverseProperty(nameof(Panier.Commandes))]
        public virtual Panier IdPanierNavigation { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.Commandes))]
        public virtual Client IdClientNavigation { get; set; } = null!;

        [ForeignKey(nameof(VirementId))]
        [InverseProperty(nameof(VirementBancaire.Commandes))]
        public virtual VirementBancaire? IdVirementNavigation { get; set; }

        [ForeignKey(nameof(LivraisonId))]
        [InverseProperty(nameof(LivraisonDomicile.Commandes))]
        public virtual LivraisonDomicile? IdLivraisonNavigation { get; set; }

        [ForeignKey(nameof(BoutiqueId))]
        [InverseProperty(nameof(Boutique.Commandes))]
        public virtual Boutique? IdBoutiqueNavigation { get; set; }

        [ForeignKey(nameof(PaypalId))]
        [InverseProperty(nameof(Paypal.Commandes))]
        public virtual Paypal? IdPaypalNavigation { get; set; }

        [ForeignKey(nameof(CarteBancaireId))]
        [InverseProperty(nameof(CarteBancaire.Commandes))]
        public virtual CarteBancaire? IdCarteNavigation { get; set; }

        [InverseProperty(nameof(EstCommande.IdCommandeNavigation))]
        public virtual ICollection<EstCommande> EstCommandes { get; set; } = new List<EstCommande>();
    }
}
