using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estinscritedans_eid", Schema = "miliboo")]
    [PrimaryKey(nameof(Codephoto), nameof(Codephototheque))]
    public partial class EstInscriteDans
    {
        [Key]
        [Column("eid_codephoto")]
        [StringLength(10)]
        public string Codephoto { get; set; } = null!;

        [Key]
        [Column("eid_codephototheque")]
        public int Codephototheque { get; set; }

        [ForeignKey(nameof(Codephoto))]
        [InverseProperty(nameof(Photo.EstIncriteDanss))]
        public virtual Photo CodephotoNavigation { get; set; } = null!;

        [ForeignKey(nameof(Codephototheque))]
        [InverseProperty(nameof(Phototheque.EstIncriteDanss))]
        public virtual Phototheque CodephotothequeNavigation { get; set; } = null!;
    }
}
