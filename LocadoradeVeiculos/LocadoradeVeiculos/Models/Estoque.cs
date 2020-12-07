using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoradeVeiculos.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstoque { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public string AnoModelo { get; set; }
        public string AnoFabricacao { get; set; }
        public virtual ICollection<Locacao> Locacaos { get; set; }
    }
}
