using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_regroupement_rgp", Schema = "miliboo")]
    [PrimaryKey(nameof(RegroupementId))]
    public partial class Regroupement
    {
        [Key]
        [Column("rgp_id")]
        public int RegroupementId { get; set; }

        [Column("rgp_libelle")]
        [StringLength(50)]
        public string? Libelleregroupement { get; set; }

        [InverseProperty(nameof(Appartient.IdRegroupementNavigation))]
        public virtual ICollection<Appartient> Appartients { get; set; } = new List<Appartient>();
    }
}
