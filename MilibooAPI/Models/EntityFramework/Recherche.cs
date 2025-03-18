using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_recherche_rch", Schema = "miliboo")]
    [PrimaryKey(nameof(ClientId), nameof(ProduitId))]
    public partial class Recherche
    {
        [Key]
        [Column("rch_idclient")]
        public int ClientId { get; set; }

        [Key]
        [Column("rch_idproduit")]
        public int ProduitId { get; set; }

        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.Recherches))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.Recherches))]
        public virtual Client IdClientNavigation { get; set; } = null!;
    }
}
