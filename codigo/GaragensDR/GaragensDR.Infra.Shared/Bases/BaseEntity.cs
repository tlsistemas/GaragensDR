using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace GaragensDR.Infra.Shared.Bases
{
    [DebuggerDisplay("Id={Id}")]
    [Serializable]
    public class BaseEntity
    {
        [System.Xml.Serialization.XmlIgnore]
        [NotMapped]
        public String Key
        {
            get
            {
                if (Id == 0)
                    return "";
                else return Id.ToString().EncryptAES().UrlEncode();
            }
            set
            {
                int id;
                if (!String.IsNullOrEmpty(value) &&
                    int.TryParse(value.UrlDecode().DecryptAES(), out id))
                    Id = id;
                else
                    Id = 0;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; set; }
        public Boolean Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
