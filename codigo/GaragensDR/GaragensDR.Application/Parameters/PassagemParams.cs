using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Delegates;
using System.Linq.Expressions;

namespace GaragensDR.Application.Parameters
{
    public class PassagemParams : BaseParams<Passagem>
    {
        public string Key { get; set; } = "";
        public string Garagem { get; set; } = "";
        public string CarroPlaca { get; set; } = "";
        public string CarroMarca { get; set; } = "";
        public string CarroModelo { get; set; } = "";
        public string DataHoraEntrada { get; set; } = "";
        public string DataHoraSaida { get; set; } = "";
        public string FormaPagamento { get; set; } = "";
        public string PrecoTotal { get; set; } = "";
        public bool? Ativo { get; set; } = null;

        public override Expression<Func<Passagem, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Passagem>();

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var obj = new Passagem { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(obj.Id));
            }

            if (Id > 0)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Garagem))
            {
                predicate = predicate.And(p => p.Garagem.Contains(Garagem));
            }

            if (!string.IsNullOrWhiteSpace(CarroPlaca))
            {
                predicate = predicate.And(p => p.CarroPlaca.Contains(CarroPlaca));
            }

            if (!string.IsNullOrWhiteSpace(CarroMarca))
            {
                predicate = predicate.And(p => p.CarroMarca.Contains(CarroMarca));
            }

            if (!string.IsNullOrWhiteSpace(CarroModelo))
            {
                predicate = predicate.And(p => p.CarroModelo.Contains(CarroModelo));
            }

            if (!string.IsNullOrWhiteSpace(DataHoraEntrada))
            {
                predicate = predicate.And(p => p.DataHoraEntrada.Contains(DataHoraEntrada));
            }

            if (!string.IsNullOrWhiteSpace(DataHoraSaida))
            {
                predicate = predicate.And(p => p.DataHoraSaida.Contains(DataHoraSaida));
            }

            if (!string.IsNullOrWhiteSpace(FormaPagamento))
            {
                predicate = predicate.And(p => p.FormaPagamento.Contains(FormaPagamento));
            }

            if (!string.IsNullOrWhiteSpace(PrecoTotal))
            {
                predicate = predicate.And(p => p.PrecoTotal.Contains(PrecoTotal));
            }

            if (Ativo.HasValue)
            {
                predicate = predicate.And(p => p.Ativo == Ativo);
            }

            return predicate;
        }
    }
}
