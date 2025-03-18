using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_liker_lik", Schema = "miliboo")]
    [PrimaryKey(nameof(ProduitId), nameof(ClientId))]
    public partial class Liker
    {
        [Key]
        [Column("lik_idproduit")]
        public int ProduitId { get; set; }

        [Key]
        [Column("lik_idclient")]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.Likers))]
        public virtual Client IdClientNavigation { get; set; } = null!;

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.Likers))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;
    }
}
