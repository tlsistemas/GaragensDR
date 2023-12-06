using Dapper;
using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Contexts;
using System.Data;

namespace GaragensDR.Infra.Data.Repositories
{
    public class FormaPagamentoRepository : BaseRepository<FormaPagamento>, IFormaPagamentoRepository
    {
        private readonly SqlContext _context;
        private readonly DapperContext _contextDapper;

        public FormaPagamentoRepository(SqlContext context, DapperContext contextDapper) : base(context)
        {
            _context = context;
            _contextDapper = contextDapper;
        }

        public async Task<int> AdicionarLista(List<FormaPagamento> lista)
        {
            int resp = 0;

            try
            {
                using (var connection = _contextDapper.CreateConnection())
                {
                    connection.Open();

                    using (var tran = connection.BeginTransaction())
                    {
                        var parametersEntrada = new DynamicParameters();
                        Int32 idEntrada = 0;
                        var sqlEntrada = @"INSERT INTO FormasPagamento (Codigo, Descricao) 
                                 OUTPUT INSERTED.ID 
                                 VALUES (@Codigo, @Descricao);";
                        foreach (var item in lista)
                        {
                            parametersEntrada.Add("Codigo", item.Codigo, DbType.String);
                            parametersEntrada.Add("Descricao", item.Descricao, DbType.String);

                            idEntrada = await connection.QuerySingleAsync<int>(sqlEntrada, parametersEntrada, tran);

                        }

                        resp = idEntrada >= 1 ? idEntrada : 0;

                        if (resp > 0)
                        {
                            tran.Commit(); //Or rollback
                            resp = idEntrada;
                        }
                        else
                            tran.Rollback();
                    }
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            return resp;
        }
    }
}