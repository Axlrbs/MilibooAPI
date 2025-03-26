using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryClient : IDataRepository<Client>
    {
        Task<ActionResult<Client>> GetByStringBisAsync(string str);
    }
}
