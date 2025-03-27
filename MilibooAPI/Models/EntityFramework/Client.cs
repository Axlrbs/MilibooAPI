using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_client_clt", Schema = "miliboo")]
    [PrimaryKey(nameof(ClientId))]
    public partial class Client
    {
        [Key]
        [Column("clt_idclient")]
        public int ClientId { get; set; }

        [Column("clt_nompersonne", TypeName = "varchar(20)")]
        public string? NomPersonne { get; set; }

        [Column("clt_prenompersonne", TypeName = "varchar(20)")]
        public string? PrenomPersonne { get; set; }

        [Column("clt_telpersonne", TypeName = "varchar(10)")]
        public string? TelPersonne { get; set; }

        [Column("clt_emailclient", TypeName = "varchar(100)")]
        public string? EmailClient { get; set; }

        [Column("clt_mdpclient", TypeName = "varchar(300)")]
        public string? MdpClient { get; set; }

        [Column("clt_nbtotalpointsfidelite")]
        public int? NbTotalPointsFidelite { get; set; }

        [Column("clt_moyenneavis", TypeName = "numeric")]
        public decimal? MoyenneAvis { get; set; }

        [Column("clt_nombreavisdepose")]
        public int? NombreAvisDepose { get; set; }

        [Column("clt_is_verified")]
        public bool IsVerified { get; set; } = false;

        [Column("clt_datederniereutilisation")]
        public DateTime DateDerniereUtilisation { get; set; } = DateTime.UtcNow;

        [Column("clt_dateanonymisation")]
        public DateTime? DateAnonymisation { get; set; } = DateTime.UtcNow;

        [InverseProperty(nameof(APour.IdClientNavigation))]
        public virtual ICollection<APour> APours { get; set; } = new List<APour>();

        [InverseProperty(nameof(AvisClient.IdclientNavigation))]
        public virtual ICollection<AvisClient> AvisClients { get; set; } = new List<AvisClient>();

        [InverseProperty(nameof(Commande.IdClientNavigation))]
        public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

        [InverseProperty(nameof(Liker.IdClientNavigation))]
        public virtual ICollection<Liker> Likers { get; set; } = new List<Liker>();

        [InverseProperty(nameof(Panier.IdClientNavigation))]
        public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();

        [InverseProperty(nameof(Recherche.IdClientNavigation))]
        public virtual ICollection<Recherche> Recherches { get; set; } = new List<Recherche>();
    }

}
