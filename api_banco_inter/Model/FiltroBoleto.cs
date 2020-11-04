using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BancoInter.Model
{
    public partial class FiltroBoleto
    {
        #region Properties
        /// <summary>
        /// default: TODOS
        /// </summary>
        public FiltrarPor filtrarPor { get; set; } = FiltrarPor.TODOS;

        /// <summary>
        /// default: VENCIMENTO
        /// </summary>
        public FiltrarDataPor filtrarDataPor { get; set; } = FiltrarDataPor.VENCIMENTO;

        [Required]
        public DateTime dataInicial { get; set; }

        [Required]
        public DateTime dataFinal { get; set; }

        /// <summary>
        /// default: NOSSONUMERO
        /// </summary>
        public OrdenarPor ordenarPor { get; set; } = OrdenarPor.NOSSONUMERO;

        /// <summary>
        /// Incial: 0
        /// </summary>
        public int page { get; set; } = 0;

        /// <summary>
        /// Tamanho Máximo: 20
        /// </summary>
        public int size { get; set; } = 20;
        #endregion

        #region Enumerators
        public enum OrdenarPor
        {
            NOSSONUMERO = 1,
            SEUNUMERO,
            DATAVENCIMENTO_ASC,
            DATAVENCIMENTO_DSC,
            NOMESACADO,
            VALOR_ASC,
            VALOR_DSC,
            STATUS_ASC,
            STATUS_DSC,
        }

        public enum FiltrarDataPor
        {
            VENCIMENTO = 1,
            EMISSAO,
            /// <summary>
            /// <para>PAGOS: corresponde a data de pagamento</para>
            /// <para>EXPIRADOS: corresponde a data de expiração</para>
            /// <para>VENCIDOSAVENCER: corresponde a data do boleto EMABERTO ou VENCIDO</para>
            /// <para>TODOSBAIXADOS: corresponde a data de baixa</para>
            /// </summary>
            SITUACAO,
        }

        public enum FiltrarPor
        {
            TODOS = 1,
            VENCIDOSAVENCER,
            EXPIRADOS,
            PAGOS,
            TODOSBAIXADOS
        }
        #endregion
    }
}
