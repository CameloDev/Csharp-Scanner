

# Scanner de Cheats em C#

## Visão Geral

Este projeto é um Scanner de Cheats desenvolvido em C# para detectar softwares não autorizados, como trapaças e outras aplicações não permitidas, rodando no sistema de um usuário. Ele utiliza diversas bibliotecas e APIs para monitorar processos, serviços e atividades de arquivos no sistema, identificando comportamentos potencialmente nocivos ou não autorizados.

O projeto inclui funcionalidades como monitoramento de processos do sistema, comunicação via rede e relatórios automatizados usando o Discord Messenger.

---

## Funcionalidades

- **Monitoramento de Processos do Sistema:** Rastreia todos os processos e serviços ativos na máquina do usuário.
- **Comunicação em Rede:** Envia logs e alertas através do Discord Messenger utilizando `HttpClient` para relatórios e monitoramento remoto.
- **Monitoramento do Sistema de Arquivos:** Varre diretórios e arquivos em busca de assinaturas e padrões conhecidos de trapaças.
- **Monitoramento em Tempo Real:** Utiliza `System.Timers` e operações assíncronas para atualizações em tempo real.
- **Inspeção do Registro:** Verifica o Registro do Windows em busca de modificações e irregularidades relacionadas a cheats ou softwares não autorizados.
- **Relatórios Multi-Plataforma:** Envia alertas via webhooks do Discord para monitoramento remoto.
- **Interface Gráfica:** Interface baseada em Windows Forms para facilitar a interação com o usuário.

---

## Bibliotecas Utilizadas

O projeto utiliza diversas bibliotecas e namespaces importantes:

- **`DiscordMessenger`:** Para envio de mensagens e alertas ao Discord via webhooks.
- **`System.Management`:** Para interação com processos e serviços do sistema.
- **`System.Net.Http`:** Para gerenciar requisições HTTP, como o envio de alertas para servidores externos.
- **`System.Diagnostics`:** Para diagnósticos detalhados do sistema e registro de eventos.
- **`Microsoft.Win32`:** Para interagir com o Registro do Windows.
- **`System.Timers`:** Para gerenciar eventos baseados em tempo.
- **`System.Windows.Forms`:** Para a interface gráfica do scanner.
- **`System.IO`:** Para monitoramento de atividades no sistema de arquivos.
- **`System.Text.RegularExpressions`:** Para buscar padrões conhecidos em arquivos e processos.
- **`Siticone.Desktop.UI.Native.WinApi`:** Para interação com a API nativa do Windows.
- **`TheArtOfDev.HtmlRenderer`:** Para renderização de conteúdo HTML no aplicativo.

---

## Instalação e Configuração

1. **Clone o Repositório:**
   ```bash
   git clone https://github.com/CameloDev/Csharp-Scanner.git
   ```

2. **Requisitos:**
   - .NET Framework 4.7 ou superior
   - Visual Studio 2019 ou mais recente

3. **Instale as Dependências:**
   Algumas dependências podem precisar ser instaladas via pacotes NuGet ou outras fontes.

4. **Execute o Projeto:**
   Abra a solução no Visual Studio e execute o projeto clicando em `Iniciar` ou pressionando `F5`.

---

## Como Usar

1. **Executando o Scanner:**
   - Ao iniciar o aplicativo, o sistema começa a escanear os processos ativos e monitorar o sistema de arquivos em tempo real.
   - Qualquer atividade suspeita, incluindo softwares não autorizados ou modificações em arquivos críticos, disparará alertas.

2. **Relatórios via Discord:**
   - Certifique-se de configurar o webhook do Discord nas configurações para receber notificações em tempo real quando cheats forem detectados.

---

## Contribuindo

Se você deseja contribuir, fique à vontade para submeter um pull request. Antes disso, por favor, certifique-se de que seu código segue as diretrizes de estilo do projeto e inclua a documentação relevante.

---

## Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo `LICENSE` para mais informações.

---

## Contato

Para dúvidas ou problemas, entre em contato com o responsável pelo repositório através do GitHub ou Discord.
