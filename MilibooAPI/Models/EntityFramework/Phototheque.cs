using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_phototheque_ptt", Schema = "miliboo")]
    [PrimaryKey(nameof(Codephototheque))]
    public partial class Phototheque
    {
        [Key]
        [Column("ptt_codephototheque")]
        public int Codephototheque { get; set; }

        [InverseProperty(nameof(EstInscriteDans.CodephotothequeNavigation))]
        public virtual ICollection<EstInscriteDans> EstIncriteDanss { get; set; } = new List<EstInscriteDans>();
    }
}
