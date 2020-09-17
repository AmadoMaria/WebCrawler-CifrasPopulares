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
        private static void Main(string[] args)
        {
            Console.WriteLine("Vamos Conferir as Cifras do momento!");
            StartCrawlerasync();
            Console.WriteLine("CrawlerFinalizado");

        }

        private static Musica InsereMusica(Musica musica)
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
                return musica;
            }
        }

        private static Ranking InsereRanking(Ranking ranking)
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
                return ranking;
            }
        }

        private static void InsereRankingMusicas(RankingMusica rankingMusica)
        {
            using var context = new CifrasContext.CifrasDbContext();
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

        private static async Task StartCrawlerasync()
        {
            int cont = 1;
            Ranking ranking = new Ranking();
            ranking.data = DateTime.Now;
            ranking = InsereRanking(ranking);

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
                var rankingMusicas = new RankingMusica();

                musica.Nome = (span.Descendants("strong")
                    .Where(node => node.GetAttributeValue("class", "").Equals("top-txt_primary")))
                    .FirstOrDefault().InnerText;

                artista.Nome = (span.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "").Equals("top-txt_secondary")))
                    .FirstOrDefault().InnerText;

                musica.Artista = artista;

                musica = InsereMusica(musica);

                rankingMusicas.MusicaID = musica.MusicaID;
                rankingMusicas.PosicaoMusica = cont;
                rankingMusicas.RankingID = ranking.RankingID;

                InsereRankingMusicas(rankingMusicas);

                cont ++;
            }
        }
    }
}
