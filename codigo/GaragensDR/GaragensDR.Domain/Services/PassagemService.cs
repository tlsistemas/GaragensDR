﻿using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Services
{
    public class PassagemService : BaseService<Passagem>, IPassagemService
    {
        public readonly IPassagemRepository _repository;

        public PassagemService(IPassagemRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> AdicionarLista(List<Passagem> lista)
        {
            return await _repository.AdicionarLista(lista);
        }
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
