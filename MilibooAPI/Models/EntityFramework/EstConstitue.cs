using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estconstitue_esc", Schema = "miliboo")]
    [PrimaryKey(nameof(AvisId), nameof(Codephoto))]
    public partial class EstConstitue
    {
        [Key]
        [Column("esc_idavis")]
        public int AvisId { get; set; }

        [Key]
        [Column("esc_codephoto")]
        [StringLength(10)]
        public string Codephoto { get; set; } = null!;

        [ForeignKey(nameof(Codephoto))]
        [InverseProperty(nameof(Photo.EstConstitues))]
        public virtual Photo CodephotoNavigation { get; set; } = null!;

        [ForeignKey(nameof(AvisId))]
        [InverseProperty(nameof(AvisClient.EstConstitues))]
        public virtual AvisClient IdavisNavigation { get; set; } = null!;
    }
}
