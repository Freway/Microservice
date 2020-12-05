using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoradeVeiculos.Models
{
    [Table("Locacao")]
    public class Locacao
    {
        [Key]
        public int IdLocacao { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataReservaInicio { get; set; }
        public DateTime DataReservaFim { get; set; }
        //public int IdEstoque { get; set; }
        public virtual Estoque Estoque { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
