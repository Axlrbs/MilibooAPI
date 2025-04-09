using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models
{
    public class CreateCommandeDTO
    {
        public int PanierId { get; set; }
        public int ClientId { get; set; }
        public int LivraisonId { get; set; }
        public int CarteBancaireId { get; set; }
        public decimal MontantCommande {  get; set; }
        public int NbPointFidelite { get; set; }
        public string Statut { get; set; } = "en attente de validation";


        public override bool Equals(object? obj)
        {
            return obj is CreateCommandeDTO dTO &&
                   PanierId == dTO.PanierId &&
                   ClientId == dTO.ClientId &&
                   LivraisonId == dTO.LivraisonId &&
                   CarteBancaireId == dTO.CarteBancaireId &&
                   MontantCommande == dTO.MontantCommande &&
                   NbPointFidelite == dTO.NbPointFidelite &&
                   Statut == dTO.Statut;
        }
    }
}
