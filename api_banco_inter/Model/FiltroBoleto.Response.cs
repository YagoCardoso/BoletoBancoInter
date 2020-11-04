using System.Collections.ObjectModel;

namespace BancoInter.Model
{
    public partial class FiltroBoleto
    {
        public class Response
        {
            public int totalPages { get; set; }
            public int totalElements { get; set; }
            public int numberOfElements { get; set; }
            public bool last { get; set; }
            public bool first { get; set; }
            public int size { get; set; }
            public Summary summary { get; set; }
            public Collection<Content> content { get; set; }

            public class Summary
            {
                public SummaryItem recebidos { get; set; }
                public SummaryItem previstos { get; set; }
                public SummaryItem baixados { get; set; }
                public SummaryItem expirados { get; set; }

                public class SummaryItem
                {
                    public int quantidade { get; set; }
                    public double valor { get; set; }
                }
            }

            public class Content
            {
                public string nossoNumero { get; set; }
                public string seuNumero { get; set; }
                public string cnpjCpfSacado { get; set; }
                public string nomeSacado { get; set; }
                public string codigoBaixa { get; set; }
                public string situacao { get; set; }
                public string dataPagtoBaixa { get; set; }
                public string dataVencimento { get; set; }
                public double valorNominal { get; set; }
                public double valorTotalRecebimento { get; set; }
                public string ddd { get; set; }
                public string telefone { get; set; }
                public string email { get; set; }
                public string dataEmissao { get; set; }
                public string dataLimite { get; set; }
                public string dataDigitavel { get; set; }
                public ContentItem desconto1 { get; set; }
                public ContentItem desconto2 { get; set; }
                public ContentItem desconto3 { get; set; }
                public ContentItem multa { get; set; }
                public ContentItem mora { get; set; }
                public double valorAbatimento { get; set; }

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
}
