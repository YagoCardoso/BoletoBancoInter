using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BancoInter.Model
{
    public partial class BaixaBoleto
    {
        #region Properties
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public CodigoBaixa codigoBaixa { get; set; }
        #endregion

        #region Enumerators
        public enum CodigoBaixa
        {
            ACERTOS = 1,
            PROTESTADO,
            DEVOLUCAO,
            PROTESTOAPOSBAIXA,
            PAGODIRETOAOCLIENTE,
            SUBSTITUICAO,
            FALTADESOLUCAO,
            APEDIDODOCLIENTE
        }
        #endregion
    }
}
