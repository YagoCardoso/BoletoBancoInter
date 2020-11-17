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
        public class Mensagem
        {
            readonly int tamanhoMaximoPorLinha = 78;

            public Mensagem() { }

            public Mensagem(string mensagem)
            {
                int maxLenght = tamanhoMaximoPorLinha * 5;

                if (mensagem.Length > maxLenght)
                    throw new Exception($"Mensagem com mais de {maxLenght} caracteres");

                try
                {
                    linha1 = mensagem.Substring(0, tamanhoMaximoPorLinha);//0
                    linha2 = mensagem.Substring(tamanhoMaximoPorLinha);//78
                    linha3 = mensagem.Substring(tamanhoMaximoPorLinha * 2);//156
                    linha4 = mensagem.Substring(tamanhoMaximoPorLinha * 3);//234
                    linha5 = mensagem.Substring(tamanhoMaximoPorLinha * 4);//312
                }
                catch { }
                finally
                {
                    if (linha1.Length > tamanhoMaximoPorLinha) linha1 = mensagem.Substring(0, tamanhoMaximoPorLinha);
                    if (linha2.Length > tamanhoMaximoPorLinha) linha2 = mensagem.Substring(0, tamanhoMaximoPorLinha);
                    if (linha3.Length > tamanhoMaximoPorLinha) linha3 = mensagem.Substring(0, tamanhoMaximoPorLinha);
                    if (linha4.Length > tamanhoMaximoPorLinha) linha4 = mensagem.Substring(0, tamanhoMaximoPorLinha);
                    if (linha5.Length > tamanhoMaximoPorLinha) linha5 = mensagem.Substring(0, tamanhoMaximoPorLinha);
                }
            }

            #region Properties
            [MaxLength(78)]
            public string linha1 { get; set; } = "";

            [MaxLength(78)]
            public string linha2 { get; set; } = "";

            [MaxLength(78)]
            public string linha3 { get; set; } = "";

            [MaxLength(78)]
            public string linha4 { get; set; } = "";

            [MaxLength(78)]
            public string linha5 { get; set; } = "";
            #endregion
        }
    }
}
