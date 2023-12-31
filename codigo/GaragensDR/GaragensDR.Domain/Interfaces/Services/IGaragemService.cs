﻿using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases.Interface;

namespace GaragensDR.Domain.Interfaces.Services
{
    public interface IGaragemService : IBaseService<Garagem>
    {
        Task<int> AdicionarLista(List<Garagem> lista);
    }
}
