using Dapper;
using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Contexts;
using System.Data;

namespace GaragensDR.Infra.Data.Repositories
{
    public class GaragemRepository : BaseRepository<Garagem>, IGaragemRepository
    {
        private readonly SqlContext _context;
        private readonly DapperContext _contextDapper;

        public GaragemRepository(SqlContext context, DapperContext contextDapper) : base(context)
        {
            _context = context;
            _contextDapper = contextDapper;
        }

        public async Task<int> AdicionarLista(List<Garagem> lista)
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
                        var sqlEntrada = @"INSERT INTO Garagens (Codigo, Nome, Preco1aHora, 
                                 PrecoHorasExtra, PrecoMensalista) 
                                 OUTPUT INSERTED.ID 
                                 VALUES (@Codigo, @Nome, @Preco1aHora, @PrecoHorasExtra, @PrecoMensalista);";
                        foreach (var item in lista)
                        {
                            parametersEntrada.Add("Codigo", item.Codigo, DbType.String);
                            parametersEntrada.Add("Nome", item.Nome, DbType.String);
                            parametersEntrada.Add("Preco1aHora", item.Preco1aHora, DbType.String);
                            parametersEntrada.Add("PrecoHorasExtra", item.PrecoHorasExtra, DbType.String);
                            parametersEntrada.Add("PrecoMensalista", item.PrecoMensalista, DbType.String);

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