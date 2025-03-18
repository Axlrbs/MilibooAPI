using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_panier_pan", Schema = "miliboo")]
    [Index(nameof(ClientId), Name = "idx_panier_idclient")]
    [PrimaryKey(nameof(PanierId))]
    public partial class Panier
    {
        [Key]
        [Column("pan_idpanier")]
        public int PanierId { get; set; }

        [Column("pan_idclient")]
        public int ClientId { get; set; }

        [Column("pan_dateetheure")]
        public DateOnly? Dateetheure { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.Paniers))]
        public virtual Client IdClientNavigation { get; set; } = null!;

        [InverseProperty(nameof(Commande.IdPanierNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

        [InverseProperty(nameof(EstAjouteDans.IdpanierNavigation))]
        public virtual ICollection<EstAjouteDans> EstAjouteDanss { get; set; } = new List<EstAjouteDans>();
    }
}
