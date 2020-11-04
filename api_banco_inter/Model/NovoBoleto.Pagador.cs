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
        public class Pagador
        {
            #region Properties
            [Required]
            [JsonConverter(typeof(StringEnumConverter))]
            public TipoPessoa tipoPessoa { get; set; }

            [Required]
            [MaxLength(100)]
            public string nome { get; set; }

            [Required]
            [MaxLength(90)]
            public string endereco { get; set; }

            [Required]
            [MaxLength(10)]
            public string numero { get; set; }

            [MaxLength(30)]
            public string complemento { get; set; } = "";

            [Required]
            [MaxLength(60)]
            public string bairro { get; set; }

            [Required]
            [MaxLength(60)]
            public string cidade { get; set; }

            [Required]
            [JsonConverter(typeof(StringEnumConverter))]
            public UF uf { get; set; }

            [Required]
            [MaxLength(8)]
            public string cep { get; set; }

            [Required]
            [MaxLength(15)]
            public string cnpjCpf { get; set; }

            [MaxLength(50)]
            public string email { get; set; } = "";

            [MaxLength(2)]
            public string ddd { get; set; } = "";

            [MaxLength(9)]
            public string telefone { get; set; } = "";
            #endregion

            #region Enumerators
            public enum TipoPessoa
            {
                FISICA = 1,
                JURIDICA
            }

            public enum UF
            {
                AC = 1,
                AL,
                AP,
                AM,
                BA,
                CE,
                DF,
                ES,
                GO,
                MA,
                MT,
                MS,
                MG,
                PA,
                PB,
                PR,
                PE,
                PI,
                RJ,
                RN,
                RS,
                RO,
                RR,
                SC,
                SP,
                SE,
                TO
            }
            #endregion
        }
    }
}
