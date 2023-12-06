using System.Net;

namespace GaragensDR.Infra.Shared.Bases
{
    public class BasePaginationResponse<TData> : BaseResponse<TData>
    {
        public BasePaginationResponse()
        {

        }

        public BasePaginationResponse(TData data) : base(data)
        {

        }

        public BasePaginationResponse(TData data, HttpStatusCode Status) : base(data, Status)
        {
        }

        public int Count { get; set; }
        public int CountFull { get; set; }
        public string JsonAux { get; set; }
    }
}
