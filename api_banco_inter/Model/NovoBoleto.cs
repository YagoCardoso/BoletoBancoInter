using BancoInter.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;

namespace BancoInter.Model
{
    public partial class NovoBoleto
    {
        #region Properties
        [Required]
        [MaxLength(15)]
        public string seuNumero { get; set; }

        [Required]
        [MaxLength(15)]
        public string cnpjCPFBeneficiario { get; set; }

        [Required]
        public double valorNominal { get; set; }

        public double valorAbatimento { get; set; } = 0;

        [Required]
        [JsonConverter(typeof(JsonDateTimeFormat))]
        public DateTime dataEmissao { get; set; }

        [Required]
        [JsonConverter(typeof(JsonDateTimeFormat))]
        public DateTime dataVencimento { get; set; }

        /// <summary>
        /// Número de dias corridso após o vencimento para baixa efetiva automática do boleto.
        /// </summary>
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public NumDiasAgenda numDiasAgenda { get; set; }

        [Required]
        public Pagador pagador { get; set; } = new Pagador();

        public Mensagem mensagem { get; set; } = new Mensagem();

        [Required]
        public Desconto desconto1 { get; set; } = new Desconto();

        [Required]
        public Desconto desconto2 { get; set; } = new Desconto();

        [Required]
        public Desconto desconto3 { get; set; } = new Desconto();

        [Required]
        public Multa multa { get; set; } = new Multa();

        [Required]
        public Mora mora { get; set; } = new Mora();
        #endregion

        #region Enumerators
        public enum NumDiasAgenda
        {
            TRINTA = 30,
            SESSENTA = 60
        }
        #endregion

        internal void isValid()
        {
            if (Validation.Results == null) Validation.Results = new List<ValidationResult>();
            Validation.Results.Clear();

            var validate = new List<bool>();

            var vc = new ValidationContext(this);
            validate.Add(Validator.TryValidateObject(this, vc, Validation.Results, true));

            if (desconto1 != null)
                validate.Add(desconto1.isValid());

            if (desconto2 != null)
                validate.Add(desconto2.isValid());

            if (desconto3 != null)
                validate.Add(desconto3.isValid());

            if (multa != null)
            {
                validate.Add(multa.isValid());

                if (multa.data.HasValue && multa.data <= dataVencimento)
                {
                    Validation.Results.Add(new ValidationResult("Data Multa inferior ao vencimento"));
                }
            }

            if (mora != null)
            {
                validate.Add(mora.isValid());

                if (mora.data.HasValue && mora.data <= dataVencimento)
                {
                    Validation.Results.Add(new ValidationResult("Data Mora inferior ao vencimento"));
                }
            }

            if (validate.Contains(false))
                throw new Exception(string.Join("\r\n", Validation.Results.Select(a => a.ErrorMessage)));
        }
    }
}
