using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_scraping.Models
{
    public class ProcessoMovimentacoes
    {
        public string NumeroProcesso { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
