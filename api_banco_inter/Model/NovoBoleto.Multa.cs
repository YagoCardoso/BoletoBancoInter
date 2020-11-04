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
        public class Multa
        {
            #region Properties
            [Required]
            [JsonConverter(typeof(StringEnumConverter))]
            public CodigoMulta codigoMulta { get; set; } = CodigoMulta.NAOTEMMULTA;

            /// <summary>
            /// <para>Obrigatório se informado VALORFIXO ou PERCENTUAL</para>
            /// <para>Deve ser maior que o vencimento e marca a data de
            /// início de cobranaça de multa (incluindo essa data)</para>
            /// </summary>
            [JsonConverter(typeof(JsonDateTimeFormat))]
            [RequiredCustom(new CodigoMulta[] { CodigoMulta.VALORFIXO, CodigoMulta.PERCENTUAL })]
            public DateTime? data { get; set; }

            /// <summary>
            /// Obrigatório se informado PERCENTUAL
            /// </summary>
            [RequiredCustom(new CodigoMulta[] { CodigoMulta.PERCENTUAL })]
            public double taxa { get; set; } = 0;

            /// <summary>
            /// Obrigatório se informado VALORFIXO
            /// </summary>
            [RequiredCustom(new CodigoMulta[] { CodigoMulta.VALORFIXO })]
            public double valor { get; set; } = 0;
            #endregion

            #region Enumerators
            public enum CodigoMulta
            {
                NAOTEMMULTA = 1,
                VALORFIXO,
                PERCENTUAL
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
                CodigoMulta[] _codigos;
                public RequiredCustom(CodigoMulta[] codigos)
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
                        var codigoDesconto = validationContext.ObjectInstance.GetType().GetProperty("codigoMulta");
                        var codigo = (CodigoMulta)codigoDesconto.GetValue(validationContext.ObjectInstance, null);

                        if (_codigos.ToList().Contains(codigo))
                            return new ValidationResult($"O campo Multa.{validationContext.MemberName} é obrigatório.");
                    }

                    return ValidationResult.Success;
                }
            }
            #endregion
        }
    }
}
