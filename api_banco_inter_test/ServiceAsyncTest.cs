using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BancoInter.Model;
using BancoInter;
using Newtonsoft.Json;
using System.IO;

namespace BancoInterTest
{
    [TestClass]
    public class ServiceAsyncTest
    {
        [TestMethod]
        public void Post_NovoBoleto()
        {
            var boleto = new NovoBoleto();
            boleto.seuNumero = StaticParams._gerarSeuNumero();
            boleto.cnpjCPFBeneficiario = "00516998000181";
            boleto.valorNominal = 10.00;
            boleto.dataEmissao = DateTime.Now;
            boleto.dataVencimento = DateTime.Now.AddDays(10);
            boleto.numDiasAgenda = NovoBoleto.NumDiasAgenda.TRINTA;
            boleto.pagador.tipoPessoa = NovoBoleto.Pagador.TipoPessoa.FISICA;
            boleto.pagador.nome = "João da Silva";
            boleto.pagador.endereco = "Rua Nove de Julho";
            boleto.pagador.numero = "123";
            boleto.pagador.bairro = "Centro";
            boleto.pagador.cidade = "São Paulo";
            boleto.pagador.uf = NovoBoleto.Pagador.UF.SP;
            boleto.pagador.cep = "04739010";
            boleto.pagador.cnpjCpf = "35965221029";
            boleto.mensagem.linha1 = "linha 1 da mensagem teste";
            boleto.mensagem.linha2 = "linha 2 da mensagem teste";
            boleto.mensagem.linha3 = "linha 3 da mensagem teste";
            boleto.mensagem.linha4 = "linha 4 da mensagem teste";
            boleto.mensagem.linha5 = "linha 5 da mensagem teste";

            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);
            var resp = inter.NovoBoleto(boleto);

            var send = resp.Result != null;

            Assert.IsTrue(send);

            var jsonResult = JsonConvert.SerializeObject(resp.Result);
            Console.WriteLine(jsonResult);
        }

        [TestMethod]
        public void Post_BaixaBoleto()
        {
            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);

            var resp = inter.BaixaBoleto(StaticParams._nossoNumero, new BancoInter.Model.BaixaBoleto()
            {
                codigoBaixa = BancoInter.Model.BaixaBoleto.CodigoBaixa.FALTADESOLUCAO
            });

            var send = resp.Result == null;

            Assert.IsTrue(send);
        }

        [TestMethod]
        public void CamposObrigatorios()
        {
            var boleto = new NovoBoleto();
            boleto.desconto1.codigoDesconto = NovoBoleto.Desconto.CodigoDesconto.PERCENTUALDATAINFORMADA;
            boleto.desconto2.codigoDesconto = NovoBoleto.Desconto.CodigoDesconto.VALORANTECIPADODIACORRIDO;
            boleto.desconto3.codigoDesconto = NovoBoleto.Desconto.CodigoDesconto.VALORFIXODATAINFORMADA;
            boleto.multa.codigoMulta = NovoBoleto.Multa.CodigoMulta.PERCENTUAL;
            boleto.mora.codigoMora = NovoBoleto.Mora.CodigoMora.TAXAMENSAL;

            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);
            var resp = inter.NovoBoleto(boleto);

            bool send = false;

            Assert.ThrowsException<AggregateException>(delegate { send = resp.Result != null; });
            Assert.IsFalse(send);
        }

        [TestMethod]
        public void Get_ListaBoletos()
        {
            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);

            var filtro = new FiltroBoleto();
            filtro.dataInicial = new DateTime(2020, 11, 1, 0, 0, 0);
            filtro.dataFinal = new DateTime(2020, 12, 1, 23, 59, 59);

            var resp = inter.ListaBoletos(filtro);

            var send = resp.Result != null;

            Assert.IsTrue(send);

            var jsonResult = JsonConvert.SerializeObject(resp.Result);
            Console.WriteLine(jsonResult);
        }

        [TestMethod]
        public void Get_BoletoPDF()
        {
            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);

            var resp = inter.BoletoPdf(StaticParams._nossoNumero);

            var send = resp.Result != null;

            if (send)
            {
                if (File.Exists(StaticParams._caminhoPDF))
                    File.Delete(StaticParams._caminhoPDF);
                File.WriteAllBytes(StaticParams._caminhoPDF, resp.Result);
            }

            Assert.IsTrue(send);
        }

        [TestMethod]
        public void Get_BoletoDetalhado()
        {
            var inter = new BancoInter.ServiceAsync(StaticParams._numContaCorrente, StaticParams._caminhoCertificado, StaticParams._password);

            var resp = inter.BoletoDetalhado(StaticParams._nossoNumero);

            var send = resp.Result != null;

            Assert.IsTrue(send);

            var jsonResult = JsonConvert.SerializeObject(resp.Result);
            Console.WriteLine(jsonResult);
        }
    }
}
