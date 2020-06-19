using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoApi.Models
{
    public partial class ProcessoMovimentacoes
    {
        public int ProcessoMovimentacaoId { get; set; }
        [Required]
        public string NumeroProcesso { get; set; }
        [Required]
        public string Data { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
