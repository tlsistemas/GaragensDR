﻿using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Delegates;
using System.Linq.Expressions;

namespace GaragensDR.Application.Parameters
{
    public class FormaPagamentoParams : BaseParams<FormaPagamento>
    {
        public string Key { get; set; } = "";
        public string Codigo { get; set; } = "";
        public bool? Ativo { get; set; } = null;

        public override Expression<Func<FormaPagamento, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<FormaPagamento>();

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var obj = new FormaPagamento { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(obj.Id));
            }

            if (Id > 0)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Codigo))
            {
                predicate = predicate.And(p => p.Codigo.Contains(Codigo));
            }

            if (Ativo.HasValue)
            {
                predicate = predicate.And(p => p.Ativo == Ativo);
            }

            return predicate;
        }
    }
}
