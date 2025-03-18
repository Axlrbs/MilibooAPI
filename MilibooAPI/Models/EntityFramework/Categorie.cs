using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_categorie_cat", Schema = "miliboo")]
    [PrimaryKey(nameof(CategorieId))]
    public partial class Categorie
    {
        [Key]
        [Column("cat_idcategorie")]
        public int CategorieId { get; set; }

        [Column("cat_cat_idcategorie")]
        public int? CatIdCategorie { get; set; }

        [Column("cat_nomcategorie", TypeName = "varchar(100)")]
        public string? NomCategorie { get; set; }

        [ForeignKey(nameof(CatIdCategorie))]
        [InverseProperty(nameof(InverseCatIdcategorieNavigation))]
        public virtual Categorie? CatIdcategorieNavigation { get; set; }

        [InverseProperty(nameof(CatIdcategorieNavigation))]
        public virtual ICollection<Categorie> InverseCatIdcategorieNavigation { get; set; } = new List<Categorie>();

        [InverseProperty(nameof(AComme.IdcategorieNavigation))]
        public virtual ICollection<AComme> ACommes { get; set; } = new List<AComme>();

        [InverseProperty(nameof(EstDans.IdcategorieNavigation))]
        public virtual ICollection<EstDans> EstDanss { get; set; } = new List<EstDans>();

        [InverseProperty(nameof(EstInclu.IdcategorieNavigation))]
        public virtual ICollection<EstInclu> EstInclus { get; set; } = new List<EstInclu>();
    }
}
