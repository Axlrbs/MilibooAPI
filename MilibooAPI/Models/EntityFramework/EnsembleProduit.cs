using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_ensemble_produit_esp", Schema = "miliboo")]
    [PrimaryKey(nameof(EnsembleId))]
    public partial class EnsembleProduit
    {
        [Key]
        [Column("esp_idensemble")]
        public int EnsembleId { get; set; }

        [Column("esp_descriptionensemble")]
        [StringLength(100)]
        public string? Descriptionensemble { get; set; }

        [Column("esp_aspecttechnique")]
        [StringLength(100)]
        public string? Aspecttechnique { get; set; }

        [Column("esp_stock")]
        public int? Stock { get; set; }

        [InverseProperty(nameof(Constitue.IdensembleNavigation))]
        public virtual ICollection<Constitue> Constitues { get; set; } = new List<Constitue>();

        [InverseProperty(nameof(EstInclu.IdensembleNavigation))]
        public virtual ICollection<EstInclu> EstInclus { get; set; } = new List<EstInclu>();
    }
}
