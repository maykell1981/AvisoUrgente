using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_scraping
{
    public class Processos
    {
        public string NumeroProcesso { get; set; }
        public int Grau { get; set; }

        public static implicit operator Processos(List<Processos> v)
        {
            throw new NotImplementedException();
        }
    }
}
