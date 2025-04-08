namespace MilibooAPI.Models
{
    public class CreateAdresseDTO
    {
        public string NumeroInsee { get; set; }
        public string PaysId { get; set; }
        public string Rue { get; set; }
        public int CodePostal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreateAdresseDTO dTO &&
                   NumeroInsee == dTO.NumeroInsee &&
                   PaysId == dTO.PaysId &&
                   Rue == dTO.Rue &&
                   CodePostal == dTO.CodePostal;
        }
    }
}
