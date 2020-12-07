using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoradeVeiculos.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CPF { get; set; }
        public virtual ICollection<Locacao> Locacaos { get; set; }
    }
}