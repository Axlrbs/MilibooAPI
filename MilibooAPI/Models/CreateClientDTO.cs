namespace MilibooAPI.Models
{
    public class CreateClientDTO
    {
        public string PrenomPersonne { get; set; }
        public string NomPersonne { get; set; }
        public string TelPersonne { get; set; }
        public string EmailClient { get; set; }
        public string MdpClient { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreateClientDTO dTO &&
                   PrenomPersonne == dTO.PrenomPersonne &&
                   NomPersonne == dTO.NomPersonne &&
                   TelPersonne == dTO.TelPersonne &&
                   EmailClient == dTO.EmailClient &&
                   MdpClient == dTO.MdpClient;
        }
    }
}