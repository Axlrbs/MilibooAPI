using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_ville_vil", Schema = "miliboo")]
    [Index(nameof(NumeroInsee), Name = "idx_ville_libelleville")]
    [PrimaryKey(nameof(NumeroInsee))]
    public partial class Ville
    {
        [Key]
        [Column("vil_numeroinsee")]
        [StringLength(15)]
        public string NumeroInsee { get; set; } = null!;

        [Column("vil_libelle")]
        [StringLength(50)]
        public string? Libelleville { get; set; }

        [InverseProperty(nameof(Adresse.IdVilleNavigation))]
        public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();
    }
}
