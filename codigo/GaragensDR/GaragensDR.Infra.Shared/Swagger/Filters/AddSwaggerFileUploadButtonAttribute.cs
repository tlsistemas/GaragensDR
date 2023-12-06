using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaragensDR.Infra.Shared.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AddSwaggerFileUploadButtonAttribute : Attribute
    {
        public String ParameterName { get; set; }
    }
}
