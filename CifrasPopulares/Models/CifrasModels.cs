using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CifrasPopulares.Models
{
    public class Artista
    {
        public int ArtistaID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Musica> Musicas { get; set; }
    }

    public class Musica
    {
        public int MusicaID { get; set; }
        public string Nome { get; set; }
        public virtual Artista Artista { get; set; }
        public int ArtistaID { get; set; }
        public virtual ICollection<RankingMusica> RankingMusicas { get; set; }
    }

    public class Ranking
    {
        public int RankingID { get; set; }
        public DateTime data { get; set; }
        public virtual ICollection<RankingMusica> RankingMusicas { get; set; }
    }

    public class RankingMusica
    {
        public int RankingMusicaID { get; set; }
        public int PosicaoMusica { get; set; }
        public virtual Musica Musica { get; set; }
        public int MusicaID { get; set; }
        public virtual Ranking Ranking { get; set; }
        public int RankingID { get; set; }
    }
}
