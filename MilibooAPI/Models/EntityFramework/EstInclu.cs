using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_j_estinclu_esi", Schema = "miliboo")]
    [PrimaryKey(nameof(CategorieId), nameof(EnsembleId))]
    public partial class EstInclu
    {
        [Key]
        [Column("esi_idcategorie")]
        public int CategorieId { get; set; }

        [Key]
        [Column("esi_idensemble")]
        public int EnsembleId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.EstInclus))]
        public virtual Categorie IdcategorieNavigation { get; set; } = null!;

        [ForeignKey(nameof(EnsembleId))]
        [InverseProperty(nameof(EnsembleProduit.EstInclus))]
        public virtual EnsembleProduit IdensembleNavigation { get; set; } = null!;

    }
}
