using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_avis_client_avc", Schema = "miliboo")]
    [Index(nameof(TitreAvis), IsUnique = false, Name = "uqix_avisclient_titreavis")]
    [PrimaryKey(nameof(AvisId))]
    public partial class AvisClient
    {
        [Key]
        [Column("avc_idavis")]
        public int AvisId { get; set; }

        [Column("avc_idclient")]
        public int ClientId { get; set; }

        [Column("avc_idproduit")]
        public int ProduitId { get; set; }

        [Column("avc_descriptionavis", TypeName = "varchar(300)")]
        public string? DescriptionAvis { get; set; }

        [Column("avc_note")]
        public int? NoteAvis { get; set; }

        [Column("avc_dateavis")]
        public DateTime? DateAvis { get; set; }

        [Column("avc_titreavis", TypeName = "varchar(100)")]
        public string? TitreAvis { get; set; }

        [Column("avc_idavisparent")]
        public int? IdAvisParent { get; set; }


        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.AvisClients))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Produit.AvisClients))]
        public virtual Client IdclientNavigation { get; set; } = null!;

        [InverseProperty(nameof(EstConstitue.IdavisNavigation))]
        public virtual ICollection<EstConstitue> EstConstitues { get; set; } = new List<EstConstitue>();
    }
}
