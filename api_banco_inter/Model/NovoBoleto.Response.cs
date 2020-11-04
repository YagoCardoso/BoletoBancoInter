using BancoInter.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BancoInter.Model
{
    public partial class NovoBoleto
    {
        public class Response
        {
            public string seuNumero { get; set; }
            public string nossoNumero { get; set; }
            public string codigoBarras { get; set; }
            public string linhaDigitavel { get; set; }
        }
    }
}
