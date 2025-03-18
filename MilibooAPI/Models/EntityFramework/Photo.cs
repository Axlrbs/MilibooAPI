using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_photo_pht", Schema = "miliboo")]
    [Index(nameof(Urlphoto), Name = "idx_photo_urlphoto")]
    [PrimaryKey(nameof(CodePhoto))]
    public partial class Photo
    {
        [Key]
        [Column("pht_codephoto")]
        [StringLength(10)]
        public string CodePhoto { get; set; } = null!;

        [Column("pht_urlphoto")]
        [StringLength(500)]
        public string? Urlphoto { get; set; }

        [InverseProperty(nameof(EstConstitue.CodephotoNavigation))]
        public virtual ICollection<EstConstitue> EstConstitues { get; set; } = new List<EstConstitue>();

        [InverseProperty(nameof(EstInscriteDans.CodephotoNavigation))]
        public virtual ICollection<EstInscriteDans> EstIncriteDanss { get; set; } = new List<EstInscriteDans>();
    }
}
