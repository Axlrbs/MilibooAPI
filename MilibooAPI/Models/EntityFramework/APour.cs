using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_a_pour_apr", Schema = "miliboo")]
    [PrimaryKey(nameof(AdresseId), nameof(ClientId))]
    public partial class APour
    {
        [Key]
        [Column("apr_idadresse")]
        public int AdresseId { get; set; }

        [Key]
        [Column("apr_idclient")]
        public int ClientId { get; set; }

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.APours))]
        public virtual Adresse IdAdresseNavigation { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Client.APours))]
        public virtual Client IdClientNavigation { get; set; } = null!;
    }
}
