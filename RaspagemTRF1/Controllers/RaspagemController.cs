using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_scraping.Models;
//using web_scraping.Models;

namespace web_scraping.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RaspagemController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<Raspagem>> GetAsync()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("http://localhost/AvisoUrgente/swagger");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Processos");

                if (response.IsSuccessStatusCode)
                {
                    List<Processos> listaProcesso = await response.Content.ReadAsAsync<List<Processos>>();

                    foreach (var item in listaProcesso)
                    {
                        var url = "https://processual.trf1.jus.br/consultaProcessual/processo.php?proc=" +
                            item.NumeroProcesso + "&secao=TRF1";
                        var httpclient = new HttpClient();
                        var html = await httpclient.GetStringAsync(url);

                        var htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(html);

                        var ProcessosHtml = htmlDocument.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("id", "")
                                .Equals("aba-movimentacao")).ToList();

                        if (ProcessosHtml.Count > 0)
                        {
                            List<List<string>> ListaProcessos = ProcessosHtml[0]
                                .Descendants("tr")
                                .Skip(1)
                                .Where(tr => tr.Elements("td").Count() > 1)
                                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                .ToList();

                            List<Raspagem> List = new List<Raspagem>();
                            for (int i = 0; i < ListaProcessos.Count; i++)
                            {
                                DateTime dataMovimentacao = Convert.ToDateTime(ListaProcessos[i][0].Replace("&nbsp;", ""));

                                HttpResponseMessage verificaMovimentacao = await client
                                    .GetAsync("api/ProcessoMovimentacoes/BuscaProcessoMovimentcoesAplicacao/"
                                + item.NumeroProcesso
                                + "/"
                                + dataMovimentacao.ToString("dd/MM/yyyy").Replace("/", "-")
                                + "/"
                                + ListaProcessos[i][2]);

                                if (verificaMovimentacao.StatusCode == HttpStatusCode.OK)
                                {
                                    var processoMovimentacao = new ProcessoMovimentacoes()
                                    {
                                        Data = dataMovimentacao.ToString("dd/MM/yyyy"),
                                        Descricao = ListaProcessos[i][2],
                                        NumeroProcesso = item.NumeroProcesso
                                    };

                                    response = await client.PostAsJsonAsync("api/ProcessoMovimentacoes", processoMovimentacao);


                                }


                            }

                        }

                    }

                }
                else
                {

                    return BadRequest("Servidor da API não está rodando. Rodar o ProjetoApi no modo do IIS!");
                }

            }
            return null;
        }
    }
}
