using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_adresse_adr", Schema = "miliboo")]
    [Index(nameof(Rue), IsUnique = false, Name = "uqix_adresse_rue")]
    [PrimaryKey(nameof(AdresseId))]
    public partial class Adresse
    {
        [Key]
        [Column("adr_idadresse")]
        public int AdresseId { get; set; }

        [Column("adr_numeroinsee", TypeName = "varchar(15)")]
        public string NumeroInsee { get; set; }

        [Column("adr_idpays", TypeName = "varchar(20)")]
        public string PaysId { get; set; }

        [Column("adr_rue", TypeName = "varchar(50)")]
        public string? Rue { get; set; }

        [Column("adr_codepostal", TypeName = "numeric(5,0)")]
        public int? CodePostal { get; set; }

        [ForeignKey(nameof(PaysId))]
        [InverseProperty(nameof(Pays.Adresses))]
        public virtual Pays IdPaysNavigation { get; set; } = null!;

        [ForeignKey(nameof(NumeroInsee))]
        [InverseProperty(nameof(Ville.Adresses))]
        public virtual Ville IdVilleNavigation { get; set; } = null!;

        [InverseProperty(nameof(APour.IdAdresseNavigation))]
        public virtual ICollection<APour> APours { get; set; } = new List<APour>();

        [InverseProperty(nameof(Boutique.IdadresseNavigation))]
        public virtual ICollection<Boutique> Boutiques { get; set; } = new List<Boutique>();

        [InverseProperty(nameof(LivraisonDomicile.IdadresseNavigation))]
        public virtual ICollection<LivraisonDomicile> LivraisonDomiciles { get; set; } = new List<LivraisonDomicile>();

        [InverseProperty(nameof(SeSitue.IdadresseNavigation))]
        public virtual ICollection<SeSitue> SeSitues { get; set; } = new List<SeSitue>();

        
    }
}
