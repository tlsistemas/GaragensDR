using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaragensDR.Infra.Shared.Bases
{
    public class BaseDTO
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
		[System.Xml.Serialization.XmlIgnore]
		[Newtonsoft.Json.JsonIgnore]
		public DateTime DataCriacao { get; set; }
		[System.Xml.Serialization.XmlIgnore]
		[Newtonsoft.Json.JsonIgnore]
		public DateTime? DataAtualizacao { get; set; }
		[System.Xml.Serialization.XmlIgnore]
		[Newtonsoft.Json.JsonIgnore]
		public string? CriadoPor { get; set; }
		[System.Xml.Serialization.XmlIgnore]
		[Newtonsoft.Json.JsonIgnore]
		public string? AtualizadoPor { get; set; }
		[System.Xml.Serialization.XmlIgnore]
		[Newtonsoft.Json.JsonIgnore]
		public bool Ativo { get; set; }
    }
}
