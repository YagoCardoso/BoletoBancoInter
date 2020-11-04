using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoInter.Model
{
    public partial class BoletoDetalhado
    {
        public class Response
        {
            public string nomeBeneficiario { get; set; }
            public string cnpjCpfBeneficiario { get; set; }
            public string tipoPessoaBeneficiario { get; set; }
            public string dataHoraSituacao { get; set; }
            public string codigoBarras { get; set; }
            public string linhaDigitavel { get; set; }
            public string dataVencimento { get; set; }
            public string dataEmissao { get; set; }
            public string descricao { get; set; }
            public string seuNumero { get; set; }
            public double valorNominal { get; set; }
            public string nomePagador { get; set; }
            public string emailPagador { get; set; }
            public string telefonePagador { get; set; }
            public string tipoPessoaPagador { get; set; }
            public string cnpjCpfPagador { get; set; }
            public string dataLimitePagamento { get; set; }
            public double valorAbatimento { get; set; }
            public string situacaoPagamento { get; set; }
            public ContentItem desconto1 { get; set; }
            public ContentItem desconto2 { get; set; }
            public ContentItem desconto3 { get; set; }
            public ContentItem multa { get; set; }
            public ContentItem mora { get; set; }

            public class ContentItem
            {
                public string codigo { get; set; }
                public string data { get; set; }
                public double taxa { get; set; }
                public double valor { get; set; }
            }


        }
    }
}
