using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoApi.Models
{
    public partial class Processos
    {
        //public Processos()
        //{
        //    ProcessoMovimentacoes = new HashSet<ProcessoMovimentacoes>();
        //}

       [Required]
        public string NumeroProcesso { get; set; }
        [Required]
        public int Grau { get; set; }

        //public virtual ICollection<ProcessoMovimentacoes> ProcessoMovimentacoes { get; set; }
    }
}
