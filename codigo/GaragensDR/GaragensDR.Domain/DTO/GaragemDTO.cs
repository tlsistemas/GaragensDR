using GaragensDR.Infra.Shared.Bases;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GaragensDR.Domain.DTO
{
    public class GaragemDTO : BaseDTO
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("Preco_1aHora")]        
        public string Preco1aHora { get; set; } = string.Empty;
        [JsonPropertyName("Preco_HorasExtra")]
        public string PrecoHorasExtra { get; set; } = string.Empty;
        [JsonPropertyName("Preco_Mensalista")]
        public string PrecoMensalista { get; set; } = string.Empty;

    }
}
