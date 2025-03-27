using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilibooAPI.Models.EntityFramework;
using MilibooAPI.Models.Repository;

namespace MilibooAPI.Models.DataManager
{
    public class ClientManager : IDataRepositoryClient
    {
        readonly MilibooDBContext? milibooContext;
        public ClientManager() { }
        public ClientManager(MilibooDBContext context)
        {
            milibooContext = context;
        }
        public async Task<ActionResult<IEnumerable<Client>>> GetAllAsync()
        {
            return await milibooContext.Clients.ToListAsync();
        }
        public async Task<ActionResult<Client>> GetByIdAsync(int id)
        {
            return await milibooContext.Clients.FirstOrDefaultAsync(u => u.ClientId == id);
        }
        public async Task<ActionResult<Client>> GetByStringAsync(string nom)
        {
            return await milibooContext.Clients.FirstOrDefaultAsync(u => u.NomPersonne.ToUpper() == nom.ToUpper());
        }
        public async Task<ActionResult<Client>> GetByStringBisAsync(string email)
        {
            return await milibooContext.Clients.FirstOrDefaultAsync(u => u.EmailClient.ToUpper() == email.ToUpper());
        }

        public async Task AddAsync(Client entity)
        {
            await milibooContext.Clients.AddAsync(entity);
            await milibooContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Client client, Client entity)
        {
            milibooContext.Entry(client).State = EntityState.Modified;
            client.ClientId = entity.ClientId;
            client.NomPersonne = entity.NomPersonne;
            client.PrenomPersonne = entity.PrenomPersonne;
            client.TelPersonne = entity.TelPersonne;
            client.EmailClient = entity.EmailClient;
            client.MdpClient = entity.MdpClient;
            client.NbTotalPointsFidelite = entity.NbTotalPointsFidelite;
            client.MoyenneAvis = entity.MoyenneAvis;
            client.NombreAvisDepose = entity.NombreAvisDepose;
            client.IsVerified = entity.IsVerified;
            client.DateDerniereUtilisation = entity.DateDerniereUtilisation;
            client.DateAnonymisation = entity.DateAnonymisation;
            await milibooContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Client client)
        {
            milibooContext.Clients.Remove(client);
            await milibooContext.SaveChangesAsync();
        }


    }
}