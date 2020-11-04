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
        public class Mora
        {
            #region Properties
            [Required]
            [JsonConverter(typeof(StringEnumConverter))]
            public CodigoMora codigoMora { get; set; } = CodigoMora.ISENTO;

            /// <summary>
            /// <para>Obrigatório se informado VALORDIA ou TAXAMENSAL</para>
            /// <para>Deve ser maior que o vencimento e marca a data de 
            /// início de cobranaça de mora (incluindo essa data)</para>
            /// </summary>
            [JsonConverter(typeof(JsonDateTimeFormat))]
            [RequiredCustom(new CodigoMora[] { CodigoMora.VALORDIA, CodigoMora.TAXAMENSAL })]
            public DateTime? data { get; set; }

            /// <summary>
            /// Obrigatório se informado TAXAMENSAL
            /// </summary>
            [RequiredCustom(new CodigoMora[] { CodigoMora.TAXAMENSAL })]
            public double taxa { get; set; } = 0;

            /// <summary>
            /// Obrigatório se informado VALORDIA
            /// </summary>
            [RequiredCustom(new CodigoMora[] { CodigoMora.VALORDIA })]
            public double valor { get; set; } = 0;
            #endregion

            #region Enumerators
            public enum CodigoMora
            {
                VALORDIA = 1,
                TAXAMENSAL,
                ISENTO
            }
            #endregion

            internal bool isValid()
            {
                var validate = new List<bool>();

                var vc = new ValidationContext(this);
                validate.Add(Validator.TryValidateObject(this, vc, Validation.Results, true));

                return !validate.Contains(false);
            }

            #region JsonValidation
            public class RequiredCustom : ValidationAttribute
            {
                CodigoMora[] _codigos;
                public RequiredCustom(CodigoMora[] codigos)
                {
                    _codigos = codigos;
                }

                public override bool IsValid(object value)
                {
                    return base.IsValid(value);
                }

                protected override ValidationResult IsValid(object value, ValidationContext validationContext)
                {
                    if (value == null)
                    {
                        var codigoDesconto = validationContext.ObjectInstance.GetType().GetProperty("codigoMora");
                        var codigo = (CodigoMora)codigoDesconto.GetValue(validationContext.ObjectInstance, null);

                        if (_codigos.ToList().Contains(codigo))
                            return new ValidationResult($"O campo Mora.{validationContext.MemberName} é obrigatório.");
                    }

                    return ValidationResult.Success;
                }
            }
            #endregion
        }
    }
}
