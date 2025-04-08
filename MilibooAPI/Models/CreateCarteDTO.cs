namespace MilibooAPI.Models
{
    public class CreateCarteDTO
    {
        public string NomCarte { get; set; }
        public string NumCarte { get; set; }
        public string DateExpiration { get; set; }
        public string Cvvcarte { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreateCarteDTO dTO &&
                   NomCarte == dTO.NomCarte &&
                   NumCarte == dTO.NumCarte &&
                   DateExpiration == dTO.DateExpiration &&
                   Cvvcarte == dTO.Cvvcarte;
        }
    }
}