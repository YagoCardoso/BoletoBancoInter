using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BancoInterTest
{
    internal static class StaticParams
    {
        internal static string _numContaCorrente = ""; //TODO: Numero da Conta Corrente

        internal static string _caminhoCertificado = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"Debug\Certificado\certificadoopenssl.pfx"); //TODO: Certificado PFX
        internal static string _password = ""; //TODO: Senha do Certificado

        internal static string _caminhoPDF = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"Debug\boleto.pdf");
        internal static string _nossoNumero = ""; //TODO: Nosso Número gerado pelo Banco Inter

        internal static string _gerarSeuNumero()
        {
            return Guid.NewGuid()
                .ToString()
                .Replace("-", string.Empty)
                .Substring(0, 15);
        }
    }
}
