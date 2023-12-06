using Dapper;
using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Contexts;
using System.Data;

namespace GaragensDR.Infra.Data.Repositories
{
    public class PassagemRepository : BaseRepository<Passagem>, IPassagemRepository
    {
        private readonly SqlContext _context;
        private readonly DapperContext _contextDapper;

        public PassagemRepository(SqlContext context, DapperContext contextDapper) : base(context)
        {
            _context = context;
            _contextDapper = contextDapper;
        }

        public async Task<int> AdicionarLista(List<Passagem> lista)
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
                        var sqlEntrada = @"INSERT INTO Passagens (Garagem, CarroPlaca, CarroMarca, 
                                 CarroModelo, DataHoraEntrada, DataHoraSaida, FormaPagamento, PrecoTotal) 
                                 OUTPUT INSERTED.ID 
                                 VALUES (@Garagem, @CarroPlaca, @CarroMarca, @CarroModelo, @DataHoraEntrada, @DataHoraSaida, @FormaPagamento, @PrecoTotal);";
                        foreach (var item in lista)
                        {
                            parametersEntrada.Add("Garagem", item.Garagem, DbType.String);
                            parametersEntrada.Add("CarroPlaca", item.CarroPlaca, DbType.String);
                            parametersEntrada.Add("CarroMarca", item.CarroMarca, DbType.String);
                            parametersEntrada.Add("CarroModelo", item.CarroModelo, DbType.String);
                            parametersEntrada.Add("DataHoraEntrada", item.DataHoraEntrada, DbType.String);
                            parametersEntrada.Add("DataHoraSaida", item.DataHoraSaida, DbType.String);
                            parametersEntrada.Add("FormaPagamento", item.FormaPagamento, DbType.String);
                            parametersEntrada.Add("PrecoTotal", item.PrecoTotal, DbType.String);

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