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
        public class Desconto
        {
            #region Properties
            [Required]
            [JsonConverter(typeof(StringEnumConverter))]
            public CodigoDesconto codigoDesconto { get; set; } = CodigoDesconto.NAOTEMDESCONTO;

            /// <summary>
            /// <para>Obrigatório para códigos de desconto VALORFIXODATAINFORMADA, e PERCENTUALDATAINFORMADA.</para>
            /// <para>Não informar para os demais.</para>
            /// </summary>
            [JsonConverter(typeof(JsonDateTimeFormat))]
            [RequiredCustom(new CodigoDesconto[] { CodigoDesconto.VALORFIXODATAINFORMADA, CodigoDesconto.PERCENTUALDATAINFORMADA })]
            public DateTime? data { get; set; }

            /// <summary>
            /// <para>Obrigatório para códigos de desconto </para>
            /// <para>PERCENTUALDATAINFORMADA, </para>
            /// <para>PERCENTUALVALORNOMINALDIACORRIDO, e </para>
            /// <para>PERCENTUALVALORNOMINALDIAUTIL</para>
            /// </summary>
            [RequiredCustom(new CodigoDesconto[] { CodigoDesconto.PERCENTUALDATAINFORMADA, CodigoDesconto.PERCENTUALVALORNOMINALDIACORRIDO, CodigoDesconto.PERCENTUALVALORNOMINALDIAUTIL })]
            public double taxa { get; set; } = 0;

            /// <summary>
            /// <para>Obrigatório para códigos de desconto </para>
            /// <para>VALORFIXODATAINFORMADA, </para>
            /// <para>VALORANTECIPADODIACORRIDO, e </para>
            /// <para>VALORANTECIPADODIAUTIL</para>
            /// </summary>
            [RequiredCustom(new CodigoDesconto[] { CodigoDesconto.VALORFIXODATAINFORMADA, CodigoDesconto.VALORANTECIPADODIACORRIDO, CodigoDesconto.VALORANTECIPADODIAUTIL })]
            public double valor { get; set; } = 0;
            #endregion

            #region Enumerators
            public enum CodigoDesconto
            {
                NAOTEMDESCONTO = 1,
                VALORFIXODATAINFORMADA,
                PERCENTUALDATAINFORMADA,
                VALORANTECIPADODIACORRIDO,
                VALORANTECIPADODIAUTIL,
                PERCENTUALVALORNOMINALDIACORRIDO,
                PERCENTUALVALORNOMINALDIAUTIL
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
                CodigoDesconto[] _codigos;
                public RequiredCustom(CodigoDesconto[] codigos)
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
                        var codigoDesconto = validationContext.ObjectInstance.GetType().GetProperty("codigoDesconto");
                        var codigo = (CodigoDesconto)codigoDesconto.GetValue(validationContext.ObjectInstance, null);

                        if (_codigos.ToList().Contains(codigo))
                            return new ValidationResult($"O campo Desconto.{validationContext.MemberName} é obrigatório.");
                    }

                    return ValidationResult.Success;
                }
            }
            #endregion
        }
    }
}
