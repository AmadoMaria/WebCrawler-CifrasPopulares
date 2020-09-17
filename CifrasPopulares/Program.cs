using System;
using System.Text;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using CifrasPopulares.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CifrasPopulares
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vamos Conferir as Cifras do momento!");
            startCrawlerasync();
            Console.WriteLine("CrawlerFinalizado");

        }

        private static void InsereMusica(Musica musica)
        {
            using (var context = new CifrasContext.CifrasDbContext())
            {
                context.Database.EnsureCreated();

                try
                {

                    context.Musicas.Add(musica);
                    context.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("Algum erro ocorreu!");
                }
            }
        }

        private static void InsereRanking(Ranking ranking)
        {
            using (var context = new CifrasContext.CifrasDbContext())
            {
                context.Database.EnsureCreated();

                try
                {
                    context.Rankings.Add(ranking);
                    context.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("Algum erro ocorreu!");
                }
            }
        }

        private static void InsereRankingMusicas(RankingMusica rankingMusica)
        {
            using (var context = new CifrasContext.CifrasDbContext())
            {
                context.Database.EnsureCreated();

                try
                {
                    context.RankingMusicas.Add(rankingMusica);
                    context.SaveChanges();
                }
                catch
                {
                    Console.WriteLine("Algum erro ocorreu!");
                }
            }
        }

        private static async Task startCrawlerasync()
        {
            int cont = 1;
            var ranking = new Ranking();
            var rankingMusicas = new RankingMusica();
            ranking.data = DateTime.Now;

            var url = "https://www.cifraclub.com.br/";
            var client = new WebClient();
            string pagina = client.DownloadString(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pagina);
            var spans =
                htmlDocument.DocumentNode.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "").Equals("top-number")).ToList();

            foreach (var span in spans)
            {
                var artista = new Artista();
                var musica = new Musica();

                musica.Nome = (span.Descendants("strong")
                    .Where(node => node.GetAttributeValue("class", "").Equals("top-txt_primary")))
                    .FirstOrDefault().InnerText;

                artista.Nome = (span.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Equals("top-txt_secondary")))
                    .FirstOrDefault().InnerText;

                musica.Artista = artista;

                rankingMusicas.Musica = musica;
                rankingMusicas.PosicaoMusica = cont;
                rankingMusicas.Ranking = ranking;

                InsereMusica(musica);
                InsereRanking(ranking);
                InsereRankingMusicas(rankingMusicas);

                cont ++;
            }
        }
    }
}
