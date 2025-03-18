using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_professionnel_prf", Schema = "miliboo")]
    [Index(nameof(ProfessionnelId), Name = "uc_professionnel", IsUnique = true)]
    [PrimaryKey(nameof(ProfessionnelId))]
    public partial class Professionnel
    {
        [Key]
        [Column("prf_id")]
        public int ProfessionnelId { get; set; }

        [Column("prf_nompersonne")]
        [StringLength(20)]
        public string? Nompersonne { get; set; }

        [Column("prf_prenompersonne")]
        [StringLength(20)]
        public string? Prenompersonne { get; set; }

        [Column("prf_telpersonne")]
        [StringLength(10)]
        public string? Telpersonne { get; set; }

        [Column("prf_raisonsociale")]
        [StringLength(50)]
        public string? Raisonsociale { get; set; }

        [Column("prf_telfixe")]
        [StringLength(10)]
        public string? Telfixe { get; set; }

        [InverseProperty(nameof(Demande.IdProfessionnelNavigation))]
        public virtual ICollection<Demande> Demandes { get; set; } = new List<Demande>();

        [InverseProperty(nameof(SeSitue.IdProfessionnelNavigation))]
        public virtual ICollection<SeSitue> SeSitues { get; set; } = new List<SeSitue>();
    }
}
