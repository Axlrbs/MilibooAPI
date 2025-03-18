using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_pays_pys", Schema = "miliboo")]
    [Index(nameof(Libellepays), Name = "idx_pays_libellepays")]
    [PrimaryKey(nameof(PaysId))]
    public partial class Pays
    {
        [Key]
        [Column("pys_idpays")]
        [StringLength(20)]
        public string PaysId { get; set; } = null!;

        [Column("pys_libellepays")]
        [StringLength(100)]
        public string? Libellepays { get; set; }

        [InverseProperty(nameof(Adresse.IdPaysNavigation))]
        public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();
    }
}
