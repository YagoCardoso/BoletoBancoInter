# Boletos no Banco Inter
Consumo de API para emissão de boletos no Banco Inter

# Autenticação
- Para fazer a autenticação na API do Inter precisa de duas coisas, o Número da Conta Corrente (da conta PJ) e um Certificado.
- Para habilitar o uso de API e gerar o certificado logue com a conta PJ no internet baking.
- Depois navegue pelo menu até a página de criação de aplicação para API.
- Siga o passo-a-passo no internet baking para gerar o csr e o private key com o openssl.
- Depois ficará disponível para download o arquivo crt. Baixe ele.
- Agora com o arquivo crt e o private key em mãos, gere o arquivo pfx para usar na autenticação.
- Para gerar o certificado no formato pfx, utilize os arquivos crt e private.key com o comando abaixo:
	> openssl pkcs12 -export -out domain.name.pfx -inkey domain.name.key -in domain.name.crt
	
- [Se estiver no Windows, baixe aqui o openssl](https://code.google.com/archive/p/openssl-for-windows/downloads)


# Consumindo a API
- Instancie a classe passando o numero da conta corrente, o caminho do certificado, e a senha.
```C#
//Métodos assíncronos
var inter = new BancoInter.ServiceAsync(numContaCorrente, caminhoCertificado, password);

//ou

//Métodos síncrono
var inter = new BancoInter.Service(numContaCorrente, caminhoCertificado, password);
```

- Gerar novo boleto
```C#
var boleto = new BancoInter.Model.NovoBoleto();
...
var result = inter.NovoBoleto(boleto).Result;
```

- Dar baixa no boleto
```C#
var baixaBoleto = new BancoInter.Model.BaixaBoleto();
...
var result = inter.BaixaBoleto(nossoNumero, baixaBoleto).Result;
```

- Listar Boletos
```C#
var filtro = new BancoInter.Model.FiltroBoleto();
...
var result = inter.ListaBoletos(filtro).Result;
```

- Detalhes do Boleto
```C#
var result = inter.BoletoDetalhado(nossoNumero).Result;
```

- Boleto em PDF
```C#
var result = inter.BoletoPdf(nossoNumero).Result;
File.WriteAllBytes("C:\boleto.pdf", result);
```
