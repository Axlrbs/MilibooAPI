using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_avis_client_avc", Schema = "miliboo")]
    public partial class AvisClient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [JsonIgnore]
        [ForeignKey(nameof(ProduitId))]
        [InverseProperty(nameof(Produit.AvisClients))]
        public virtual Produit IdproduitNavigation { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Produit.AvisClients))]
        public virtual Client IdclientNavigation { get; set; } = null!;

        [InverseProperty(nameof(EstConstitue.IdavisNavigation))]
        public virtual ICollection<EstConstitue> EstConstitues { get; set; } = new List<EstConstitue>();
    }
    public class AvisClientDto
    {
        public int ClientId { get; set; }
        public int ProduitId { get; set; }
        public string? DescriptionAvis { get; set; }
        public int? NoteAvis { get; set; }
        public string? TitreAvis { get; set; }
    }
}