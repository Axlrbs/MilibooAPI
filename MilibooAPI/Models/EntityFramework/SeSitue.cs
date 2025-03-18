using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_sesitue_sst", Schema = "miliboo")]
    [PrimaryKey(nameof(AdresseId), nameof(ProfessionnelId))]
    public partial class SeSitue
    {
        [Key]
        [Column("sst_idadresse")]
        public int AdresseId { get; set; }

        [Key]
        [Column("sst_idprofessionnel")]
        public int ProfessionnelId { get; set; }

        [ForeignKey(nameof(ProfessionnelId))]
        [InverseProperty(nameof(Professionnel.SeSitues))]
        public virtual Professionnel IdProfessionnelNavigation { get; set; } = null!;

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.SeSitues))]
        public virtual Adresse IdadresseNavigation { get; set; } = null!;
    }
}
