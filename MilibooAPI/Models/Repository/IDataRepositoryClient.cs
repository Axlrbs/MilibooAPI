using Microsoft.AspNetCore.Mvc;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryClient<TEntity>
    {
        Task<ActionResult<TEntity>> GetByStringBisAsync(string str);
    }
}
