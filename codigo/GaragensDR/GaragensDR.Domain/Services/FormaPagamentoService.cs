﻿using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Services
{
    public class FormaPagamentoService : BaseService<FormaPagamento>, IFormaPagamentoService
    {
        public readonly IFormaPagamentoRepository _repository;

        public FormaPagamentoService(IFormaPagamentoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
