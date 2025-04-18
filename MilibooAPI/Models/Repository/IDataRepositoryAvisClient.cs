﻿using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;

namespace MilibooAPI.Models.Repository
{
    public interface IDataRepositoryAvisClient : IDataRepository<AvisClient>
    {
        Task<ActionResult<IEnumerable<AvisClientDto>>> GetAllByProduitIdAsync(int produitId);
    }
}