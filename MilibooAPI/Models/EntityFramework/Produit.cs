using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MilibooAPI.Models.EntityFramework
{
    [Table("t_e_produit_prd", Schema = "miliboo")]
    [Index(nameof(Reference), Name = "idx_produit_reference")]
    [PrimaryKey(nameof(ProduitId))]
    public partial class Produit
    {
        [Key]
        [Column("prd_id")]
        public int ProduitId { get; set; }

        [Column("prd_carac")]
        [StringLength(1500)]
        public string? Caracteristiquetechnique { get; set; }

        [Column("prd_ref")]
        [StringLength(20)]
        public string? Reference { get; set; }

        [Column("prd_like")]
        public int? Like { get; set; }

        [InverseProperty(nameof(AComme.IdproduitNavigation))]
        public virtual ICollection<AComme> ACommes { get; set; } = new List<AComme>();

        [InverseProperty(nameof(Appartient.IdProduitNavigation))]
        public virtual ICollection<Appartient> Appartients { get; set; } = new List<Appartient>();

        [InverseProperty(nameof(AvisClient.IdproduitNavigation))]
        public virtual ICollection<AvisClient> AvisClients { get; set; } = new List<AvisClient>();

        [InverseProperty(nameof(Demande.IdproduitNavigation))]
        public virtual ICollection<Demande> Demandes { get; set; } = new List<Demande>();

        [InverseProperty(nameof(EstAjouteDans.IdproduitNavigation))]
        public virtual ICollection<EstAjouteDans> EstAjouteDanss { get; set; } = new List<EstAjouteDans>();

        [InverseProperty(nameof(EstDeCouleur.IdproduitNavigation))]
        public virtual ICollection<EstDeCouleur> EstDeCouleurs { get; set; } = new List<EstDeCouleur>();

        [InverseProperty(nameof(Constitue.IdproduitNavigation))]
        public virtual ICollection<Constitue> Constitues { get; set; } = new List<Constitue>();

        [InverseProperty(nameof(Liker.IdproduitNavigation))]
        public virtual ICollection<Liker> Likers { get; set; } = new List<Liker>();

        [InverseProperty(nameof(Recherche.IdproduitNavigation))]
        public virtual ICollection<Recherche> Recherches { get; set; } = new List<Recherche>();

    }
}
