using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_coloris_clr", Schema = "miliboo")]
    [Index(nameof(ColorisId), Name = "clr_id", IsUnique = false)]
    [PrimaryKey(nameof(ColorisId))]
    public partial class Coloris
    {
        [Key]
        [Column("clr_id")]
        public int ColorisId { get; set; }

        [Column("clr_libelle")]
        [StringLength(50)]
        public string? LibelleColoris { get; set; }

        [InverseProperty(nameof(EstAjouteDans.IdcolorisNavigation))]
        public virtual ICollection<EstAjouteDans> EstAjouteDanss { get; set; } = new List<EstAjouteDans>();

        [InverseProperty(nameof(EstDeCouleur.IdcolorisNavigation))]
        public virtual ICollection<EstDeCouleur> EstDeCouleurs { get; set; } = new List<EstDeCouleur>();
    }
}
